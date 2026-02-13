using EF.PoliceMod.Core;
using EF.PoliceMod.Gameplay;
using GTA;
using GTA.Math;
using GTA.Native;
using System;

public class SuspectOnFootExecutor
{
    private readonly SuspectController _controller;
    private readonly SuspectStateHub _stateHub;

    // Escorting tether（每帧维护）
    private int _lastFollowReissueMs = 0;

    private const float ReissueFollowDistance = 3.8f;   // 超过该距离就会尝试重下 follow
    private const int ReissueFollowCooldownMs = 800;


    // “拽着押送”的关键：双方切换押送/被拷步态（clipset）
    private const string CLIPSET_PRISONER_CUFFED = "move_m@prisoner_cuffed";
    private const string CLIPSET_PRISON_GUARD = "move_m@prison_guard";
    private bool _escortClipsetApplied = false;
    private int _lastClipsetRequestMs = 0;
    private const int CLIPSET_REQUEST_COOLDOWN_MS = 1500;
    private int _lastClipsetApplyMs = 0;
    private const int CLIPSET_REAPPLY_COOLDOWN_MS = 650;

    private int _lastCuffedUpperBodyPoseMs = 0;
    private const int CUFFED_UPPERBODY_COOLDOWN_MS = 850;

    // 实时拖拽（CuffAndLead）：将嫌疑人轻量 attach 到玩家手部附近，达到“拽着走”的持续效果。
    private bool _dragAttached = false;
    private int _draggedSuspectHandle = -1;
    private const float DragAttachDistance = 1.25f;
    private const float DragDetachDistance = 2.20f;

    private EF.PoliceMod.Core.ArrestActionStyle GetStyle()
    {
        try { return _controller != null ? _controller.CurrentArrestStyle : EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead; }
        catch { return EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead; }
    }

    public SuspectOnFootExecutor(
        SuspectController controller,
        SuspectStateHub stateHub)
    {
        _controller = controller;
        _stateHub = stateHub;

        _stateHub.OnStateChanged += OnStateChanged;

        EventBus.Subscribe<SuspectFollowRequestEvent>(OnFollowRequested);
    }

    private bool IsBusyState(SuspectState s)
    {
        // 哪些状态我们认为“占用”嫌疑人（不应在此期间有其它行为）
        return s == SuspectState.Escorting
            || s == SuspectState.EnteringVehicle
            || s == SuspectState.InVehicle
            || s == SuspectState.ExitingVehicle
            || s == SuspectState.Resisting
            || s == SuspectState.Restrained;
    }

    public void TickUpdate()
    {
        try
        {
            if (!_stateHub.Is(SuspectState.Escorting)) return;

            var suspect = _controller.GetCurrentSuspect();
            var player = Game.Player.Character;

            if (suspect == null || !suspect.Exists()) return;
            if (player == null || !player.Exists()) return;
            if (suspect.IsDead || suspect.Health <= 0) return;

            // 只在步行押送时做 tether（避免车内/过渡态抢任务）
            if (suspect.IsInVehicle() || player.IsInVehicle()) return;
            if (_stateHub.Is(SuspectState.EnteringVehicle) || _stateHub.Is(SuspectState.ExitingVehicle) || _stateHub.Is(SuspectState.InVehicle)) return;

            var style = GetStyle();

            // 修复“嫌疑人被玩家拿在手里”：任何时候都不允许 attach（并清理历史残留）
            DetachDragIfNeeded();

            // 只有“拷起牵走”才应用被拷步态；抱头跟随不改变走路姿态
            if (style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead)
                EnsureEscortClipsets(suspect, player);
            else if (_escortClipsetApplied)
                ResetEscortClipsets(suspect, player);

            EnsureEscortConstraints(suspect, style);

            // ★兜底：上拷/同步场景/任务切换后偶发“嫌疑人成为非实体(可穿模/无重力/冻结)”
            // 这里在步行押送阶段持续恢复实体属性，优先保障“不会穿模、G/E 后续链路正常”。
            if (style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead)
            {
                try { EnsureSuspectIsSolid(suspect, player); } catch { }
            }

            MaintainCuffedUpperBodyPoseIfNeeded(suspect, player, style);

            float dist = 0f;
            try { dist = suspect.Position.DistanceTo(player.Position); } catch { dist = 0f; }

            // 被撞倒/摔倒：等其自然站起（避免持续重下任务造成抖动）

            try
            {
                if (suspect.IsRagdoll || suspect.IsFalling)
                    return;
            }
            catch { }

            // 轻度偏离：定期重下 follow（防止被碰撞/任务抢占打断）
            if (dist > ReissueFollowDistance)
            {
                int now = Game.GameTime;
                if (now - _lastFollowReissueMs > ReissueFollowCooldownMs)
                {
                    _lastFollowReissueMs = now;
                    ReissueFollowTask(suspect, player, clearTasks: false);
                }
            }
        }
        catch (Exception ex)
        {
            ModLog.Error("[SuspectOnFootExecutor] TickUpdate crashed: " + ex);
        }
    }

    private void OnFollowRequested(SuspectFollowRequestEvent e)
    {
        try
        {
            if (!_stateHub.Is(SuspectState.Escorting))
                return;

            var suspect = _controller.GetCurrentSuspect();
            var player = Game.Player.Character;
            if (suspect == null || !suspect.Exists()) return;
            if (player == null || !player.Exists()) return;
            if (suspect.IsDead || suspect.Health <= 0) return;
            if (suspect.IsInVehicle() || player.IsInVehicle()) return;

            var style = GetStyle();
            try { EnsureEscortConstraints(suspect, style); } catch { }

            if (style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead)
            {
                try { EnsureEscortClipsets(suspect, player); } catch { }
            }

            try
            {
                float offY = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? -0.9f : -1.35f;
                float speed = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? 1.2f : 1.0f;
                float stopRange = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? 0.9f : 1.35f;
                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, suspect.Handle, player.Handle, 0f, offY, 0f, speed, -1, stopRange, true);
            }
            catch { }
        }
        catch { }
    }

    private void MaintainCuffedUpperBodyPoseIfNeeded(Ped suspect, Ped player, EF.PoliceMod.Core.ArrestActionStyle style)
    {
        if (style != EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead) return;
        if (suspect == null || !suspect.Exists()) return;
        if (player == null || !player.Exists()) return;

        try
        {
            int now = Game.GameTime;
            if (now - _lastCuffedUpperBodyPoseMs < CUFFED_UPPERBODY_COOLDOWN_MS) return;
            _lastCuffedUpperBodyPoseMs = now;

            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }

            bool playing = false;
            try { playing = Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, suspect.Handle, "mp_arresting", "idle", 3); } catch { playing = false; }
            if (playing) return;

            try { Function.Call(Hash.REQUEST_ANIM_DICT, "mp_arresting"); } catch { }
            try
            {
                if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_arresting"))
                    return;
            }
            catch { return; }

            try
            {
                // flags=49: Loop + OnlyAnimateUpperBody（让下半身仍可走路，同时上半身维持背手姿态）
                Function.Call(Hash.TASK_PLAY_ANIM, suspect.Handle, "mp_arresting", "idle", 4.0f, -4.0f, -1, 49, 0.0f, false, false, false);
            }
            catch { }
        }
        catch { }
    }

    private void EnsureEscortClipsets(Ped suspect, Ped player)
    {
        if (suspect == null || !suspect.Exists()) return;
        if (player == null || !player.Exists()) return;

        try
        {
            int now = Game.GameTime;
            if (now - _lastClipsetRequestMs > CLIPSET_REQUEST_COOLDOWN_MS)
            {
                _lastClipsetRequestMs = now;
                try { Function.Call(Hash.REQUEST_ANIM_SET, CLIPSET_PRISONER_CUFFED); } catch { }
                try { Function.Call(Hash.REQUEST_ANIM_SET, CLIPSET_PRISON_GUARD); } catch { }
            }

            bool prisonerLoaded = false;
            bool guardLoaded = false;
            try { prisonerLoaded = Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, CLIPSET_PRISONER_CUFFED); } catch { }
            try { guardLoaded = Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, CLIPSET_PRISON_GUARD); } catch { }

            if (!prisonerLoaded || !guardLoaded) return;

            // 周期性重施加，避免 follow/其他任务把 clipset 覆盖掉
            if (_escortClipsetApplied && now - _lastClipsetApplyMs < CLIPSET_REAPPLY_COOLDOWN_MS) return;

            // 嫌疑人：被拷步态（更像“被扯着走”）
            try { Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, suspect.Handle, CLIPSET_PRISONER_CUFFED, 0.25f); } catch { }

            // 玩家：押送步态（让整体观感更像押送而不是两个路人贴着走）
            try { Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, player.Handle, CLIPSET_PRISON_GUARD, 0.25f); } catch { }

            _escortClipsetApplied = true;
            _lastClipsetApplyMs = now;
        }
        catch { }
    }

    private void ResetEscortClipsets(Ped suspect, Ped player)
    {
        try
        {
            if (!_escortClipsetApplied) return;

            if (suspect != null && suspect.Exists())
            {
                try { Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, suspect.Handle, 0.25f); } catch { }
            }

            if (player != null && player.Exists())
            {
                try { Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, player.Handle, 0.25f); } catch { }
            }
        }
        catch { }
        finally
        {
            _escortClipsetApplied = false;
        }

        // best-effort: 释放 anim set（不影响功能）
        try { Function.Call(Hash.REMOVE_ANIM_SET, CLIPSET_PRISONER_CUFFED); } catch { }
        try { Function.Call(Hash.REMOVE_ANIM_SET, CLIPSET_PRISON_GUARD); } catch { }
    }

    private void EnsureEscortConstraints(Ped suspect, EF.PoliceMod.Core.ArrestActionStyle style)
    {
        if (suspect == null || !suspect.Exists()) return;
        if (style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead)
        {
            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_ANIMS, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_ENTITY_COLLISION, suspect.Handle, true, true); } catch { }
            try { suspect.BlockPermanentEvents = true; } catch { }
            try { suspect.AlwaysKeepTask = true; } catch { }
        }
        else
        {
            // 抱头：不强制手铐，不改步态，保持更自然
            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_ANIMS, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_ENTITY_COLLISION, suspect.Handle, true, true); } catch { }
            try { suspect.BlockPermanentEvents = true; } catch { }
            try { suspect.AlwaysKeepTask = true; } catch { }
        }
    }

    private void EnsureSuspectIsSolid(Ped suspect, Ped player)
    {
        if (suspect == null || !suspect.Exists()) return;
        if (suspect.IsDead) return;

        // 解除冻结/恢复动力学
        try { Function.Call(Hash.FREEZE_ENTITY_POSITION, suspect.Handle, false); } catch { }
        try { Function.Call(Hash.SET_ENTITY_COMPLETELY_DISABLE_COLLISION, suspect.Handle, false, false); } catch { }
        try { Function.Call(Hash.SET_ENTITY_COLLISION, suspect.Handle, true, true); } catch { }
        try { Function.Call(Hash.SET_ENTITY_DYNAMIC, suspect.Handle, true); } catch { }
        try { Function.Call(Hash.SET_ENTITY_HAS_GRAVITY, suspect.Handle, true); } catch { }
        try { Function.Call(Hash.ACTIVATE_PHYSICS, suspect.Handle); } catch { }

        // 防止同步场景残留的“彼此无碰撞”导致贴身穿模
        try
        {
            if (player != null && player.Exists())
            {
                Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, player.Handle, suspect.Handle, false);
                Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, suspect.Handle, player.Handle, false);
            }
        }
        catch { }

        // 恢复可物理交互能力（不改变手铐约束，由 EnsureEscortConstraints 控制）
        try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, suspect.Handle, true); } catch { }
        try { Function.Call(Hash.SET_PED_CAN_RAGDOLL_FROM_PLAYER_IMPACT, suspect.Handle, true); } catch { }
    }

    private void MaintainDragIfNeeded(Ped suspect, Ped player, float dist)
    {
        try
        {
            // 禁用：attach 拖拽会导致“嫌疑人被玩家拿在手里”的滑稽表现
            DetachDragIfNeeded();
            return;
        }
        catch { }
    }

    private void AttachDrag(Ped suspect, Ped player)
    {
        // deprecated / disabled
        DetachDragIfNeeded();
    }

    private void DetachDragIfNeeded()
    {
        if (!_dragAttached) return;

        try
        {
            var suspect = _controller.GetCurrentSuspect();
            var player = Game.Player.Character;

            if (suspect != null && suspect.Exists())
            {
                try { Function.Call(Hash.DETACH_ENTITY, suspect.Handle, true, true); } catch { }
            }

            // 恢复双方碰撞关系
            try
            {
                if (player != null && player.Exists() && suspect != null && suspect.Exists())
                {
                    try { Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, player.Handle, suspect.Handle, false); } catch { }
                    try { Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, suspect.Handle, player.Handle, false); } catch { }
                }
            }
            catch { }
        }
        catch { }
        finally
        {
            _dragAttached = false;
            _draggedSuspectHandle = -1;
        }
    }

    private void ReissueFollowTask(Ped suspect, Ped player, bool clearTasks)
    {
        if (suspect == null || !suspect.Exists()) return;
        if (player == null || !player.Exists()) return;

        try
        {
            if (clearTasks)
            {
                try { Function.Call(Hash.CLEAR_PED_TASKS, suspect.Handle); } catch { }
            }

            var style = GetStyle();

            // 更稳定：用 native 而不是 wrapper 的 FollowToOffsetFromEntity（后者更易被抢占）
            try
            {
                // 拷起：跟得更近；抱头：更自然一点（不要贴脸）
                float offY = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? -0.9f : -1.35f;
                float speed = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? 1.2f : 1.0f;
                float stopRange = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? 0.9f : 1.35f;
                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, suspect.Handle, player.Handle, 0f, offY, 0f, speed, -1, stopRange, true);
            }
            catch { }
        }
        catch { }
    }



    private void OnStateChanged(SuspectState from, SuspectState to)
    {
        var suspect = _controller.GetCurrentSuspect();
        var player = Game.Player.Character;

        // 离开押送/状态切换时，确保不残留 attach
        try { DetachDragIfNeeded(); } catch { }

        // 离开押送（上车/下车/反抗/束缚等）时，恢复默认步态，避免“全程押送走路太假”
        try
        {
            if (from == SuspectState.Escorting && to != SuspectState.Escorting)
            {
                ResetEscortClipsets(suspect, player);
            }
        }
        catch { }

        if (suspect == null || !suspect.Exists())
            return;

        int handle = suspect.Handle;

        // 如果之前状态是 busy 状态，先尝试解除 busy（防止遗留标记）
        try
        {
            if (IsBusyState(from))
            {
                _controller.UnmarkBusy(handle);
                ModLog.Info($"[SuspectOnFootExecutor] Unmarked busy for ped={handle} (from {from})");
            }
        }
        catch (Exception ex)
        {
            ModLog.Error("[SuspectOnFootExecutor] Error while unmarking busy: " + ex);
        }

        // 然后处理状态动作（保持原有行为触发）
        switch (to)
        {
            case SuspectState.Escorting:
                StartFollow(suspect);
                break;

            case SuspectState.Restrained:
                StartArrest(suspect);
                break;

            case SuspectState.Resisting:
                StartResist(suspect);
                break;
        }
    }

    // StartFollow
    private void StartFollow(Ped suspect)
    {
        if (suspect == null || !suspect.Exists()) return;
        if (suspect.IsDead || suspect.Health <= 0)
        {
            ModLog.Info($"[SuspectOnFootExecutor] StartFollow aborted: suspect {suspect.Handle} dead/invalid");
            return;
        }

        try
        {
            // 不再使用 busy/MarkBusy 阻塞押送：只要状态切到 Escorting，就强制下发 follow 任务。

            // 在调用任何以 player 为目标的 native Task 之前，务必确认 player 实例有效
            var player = Game.Player.Character;
            if (player == null || !player.Exists())
            {
                ModLog.Info($"[SuspectOnFootExecutor] StartFollow aborted: player invalid when issuing FollowToOffset for suspect {suspect.Handle}");
                return;
            }

            var style = GetStyle();

            // 开始跟随前，先确保约束/步态正确
            try { EnsureEscortConstraints(suspect, style); } catch { }
            try
            {
                if (style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead)
                    EnsureEscortClipsets(suspect, player);
                else if (_escortClipsetApplied)
                    ResetEscortClipsets(suspect, player);
            }
            catch { }

            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead); } catch { }

            try { Function.Call(Hash.CLEAR_PED_TASKS, suspect.Handle); } catch { }
            try
            {
                // 使用 TASK.md 中的 TASK_FOLLOW_TO_OFFSET_OF_ENTITY（更稳定，不容易打断被拷步态）
                float offY = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? -0.9f : -1.35f;
                float speed = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? 1.2f : 1.0f;
                float stopRange = style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead ? 0.9f : 1.35f;
                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, suspect.Handle, player.Handle, 0f, offY, 0f, speed, -1, stopRange, true);
                ModLog.Info($"[SuspectOnFootExecutor] Suspect {suspect.Handle} FollowToOffset issued.");
            }
            catch (Exception exTask)
            {
                ModLog.Error("[SuspectOnFootExecutor] StartFollow Task call failed: " + exTask);
                try { _controller.UnmarkBusy(suspect.Handle); } catch { }
                return;
            }
        }
        catch (Exception ex)
        {
            ModLog.Error("[SuspectOnFootExecutor] StartFollow crashed: " + ex);
            try { _controller.UnmarkBusy(suspect.Handle); } catch { }
        }
    }

    // StartArrest
    private void StartArrest(Ped suspect)
    {
        if (suspect == null || !suspect.Exists()) return;
        if (suspect.IsDead || suspect.Health <= 0)
        {
            ModLog.Info($"[SuspectOnFootExecutor] StartArrest aborted: suspect {suspect?.Handle} dead/invalid");
            return;
        }

        try
        {
            if (!suspect.Exists() || suspect.IsDead)
                return;

            var style = GetStyle();

            // 拘捕：进入“受控/束缚”表现（不要 FightAgainst）
            try { suspect.Task.ClearAll(); } catch { }

            try
            {
                suspect.BlockPermanentEvents = true;
                suspect.AlwaysKeepTask = true;

                // 拘捕后必须保证实体碰撞恢复（避免出现玩家穿模 + 嫌疑人像“幽灵”一样站住不动）
                try { Function.Call(Hash.SET_ENTITY_COLLISION, suspect.Handle, true, true); } catch { }
                try
                {
                    var player = Game.Player.Character;
                    if (player != null && player.Exists())
                    {
                        try { Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, player.Handle, suspect.Handle, false); } catch { }
                        try { Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, suspect.Handle, player.Handle, false); } catch { }
                    }
                }
                catch { }

                // 上拷 / 抱头：按风格设置
                try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead); } catch { }
                try { Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, suspect.Handle, false); } catch { }
                try { Function.Call(Hash.SET_PED_AS_ENEMY, suspect.Handle, false); } catch { }

                bool animPlayed = false;
                if (style == EF.PoliceMod.Core.ArrestActionStyle.CuffAndLead)
                {
                    try
                    {
                        Function.Call(Hash.REQUEST_ANIM_DICT, "mp_arresting");
                        int t = Game.GameTime + 800;
                        while (Game.GameTime < t)
                        {
                            try { if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_arresting")) break; } catch { break; }
                            Script.Wait(0);
                        }

                        if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_arresting"))
                        {
                            // 对讲机/确认音（best-effort，多重兜底）
                            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONFIRM_BEEP", "HUD_MINI_GAME_SOUNDSET", false); } catch { }
                            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "TIMER_STOP", "HUD_MINI_GAME_SOUNDSET", false); } catch { }
                            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "MP_AWARD", "HUD_AWARDS", false); } catch { }

                            suspect.Task.PlayAnimation("mp_arresting", "idle", 4.0f, -1, AnimationFlags.Loop);
                            animPlayed = true;
                        }
                    }
                    catch { animPlayed = false; }
                }
                else
                {
                    // 抱头动画（best-effort）
                    try
                    {
                        Function.Call(Hash.REQUEST_ANIM_DICT, "random@arrests");
                        int t = Game.GameTime + 800;
                        while (Game.GameTime < t)
                        {
                            try { if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "random@arrests")) break; } catch { break; }
                            Script.Wait(0);
                        }

                        if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "random@arrests"))
                        {
                            // 使用跪地抱头 idle，并保持末帧，避免循环“跪下-抬头”动作
                            suspect.Task.PlayAnimation("random@arrests", "kneeling_arrest_idle", 4.0f, -1, AnimationFlags.StayInEndFrame);
                            animPlayed = true;
                        }
                    }
                    catch { animPlayed = false; }
                }

                if (!animPlayed)
                {
                    try { suspect.Task.HandsUp(-1); } catch { }
                }
            }
            catch { }
        }
        catch (Exception ex)
        {
            ModLog.Error("[SuspectOnFootExecutor] StartArrest crashed: " + ex);
        }
    }

    // StartResist
    private void StartResist(Ped suspect)
    {
        if (suspect == null || !suspect.Exists()) return;
        if (suspect.IsDead || suspect.Health <= 0) return;

        var player = Game.Player.Character;
        if (player == null || !player.Exists()) return;

        try
        {
            // 从“抱头/押送/束缚”等状态切到反抗时，必须解除约束；
            // 否则会出现 UI 提示反抗但 Ped 实际只走路/不攻击（常见原因：禁止切武器/BlockPermanentEvents）。
            try { suspect.BlockPermanentEvents = false; } catch { }
            try { suspect.AlwaysKeepTask = true; } catch { }

            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_PED_AS_ENEMY, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_ANIMS, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, suspect.Handle, true); } catch { }

            // 清任务后用 Combat 任务驱动：有枪会开枪、没枪会近战，更贴合“武装反抗/暴力反抗”的提示
            try { Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, suspect.Handle); } catch { }
            try { Function.Call(Hash.TASK_COMBAT_PED, suspect.Handle, player.Handle, 0, 16); } catch { }
        }
        catch (Exception ex)
        {
            ModLog.Error("[SuspectOnFootExecutor] StartResist crashed (native or managed): " + ex);
        }
    }
}