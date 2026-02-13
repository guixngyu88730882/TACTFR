using EF.PoliceMod.Core;
using GTA;
using GTA.Native;
using System;

namespace EF.PoliceMod.Gameplay
{
    public enum SuspectBehavior
    {
        Arrested,
        Escorting
    }

    public class BehaviorKeeper
    {
        private const string CUFF_DICT = "mp_arresting";
        private const string CUFF_IDLE = "idle";

        private int _lastAnimRequestMs = 0;

        private Ped _owner;
        private SuspectBehavior? _behavior;
        private string _debugOwnerTag;

        public bool TryAcquire(Ped ped, SuspectBehavior behavior)
        {
            if (ped == null || !ped.Exists())
            {
                ModLog.Warn($"[Keeper] Acquire failed: ped invalid ({behavior})");
                return false;
            }

            if (_behavior.HasValue)
            {
                ModLog.Warn($"[Keeper] Acquire denied: already holding {_behavior}");
                return false;
            }

            _owner = ped;
            _behavior = behavior;
            _debugOwnerTag = ped.Handle.ToString();

            ModLog.Info($"[Keeper] Acquire {_behavior} owner={_debugOwnerTag}");
            return true;
        }


        public void Release(Ped ped)
        {
            if (_owner != ped)
                return;

            ModLog.Info($"[Keeper] Release {_behavior} owner={_debugOwnerTag}");

            _owner = null;
            _behavior = null;
            _debugOwnerTag = null;
        }

        public void ForceClear(string reason = "")
        {
            try
            {
                if (_behavior.HasValue)
                {
                    ModLog.Info($"[Keeper] ForceClear behavior={_behavior} owner={_debugOwnerTag} reason={reason}");
                }
            }
            catch { }

            _owner = null;
            _behavior = null;
            _debugOwnerTag = null;
        }


        public void Update()
        {
            // 关键修复：GTA 实体 handle 可能复用。
            // 如果 owner 已经不存在，不清理的话，后续新刷出的 ped 可能“继承”到旧行为。
            if (_behavior.HasValue && (_owner == null || !_owner.Exists()))
            {
                ModLog.Info($"[Keeper] Owner invalid -> auto release behavior={_behavior} owner={_debugOwnerTag}");
                _owner = null;
                _behavior = null;
                _debugOwnerTag = null;
                return;
            }

            if (!_behavior.HasValue || _owner == null || !_owner.Exists())
                return;

            switch (_behavior)
            {
                case SuspectBehavior.Arrested:
                    EnforceArrested(_owner);
                    break;

                case SuspectBehavior.Escorting:
                    EnforceEscorting(_owner);
                    break;

            }
        }
        public bool IsHolding(Ped ped, SuspectBehavior behavior)
        {
            if (!_behavior.HasValue)
                return false;

            if (_owner != ped)
                return false;

            return _behavior.Value == behavior;
        }

        private void EnforceArrested(Ped ped)
        {
            try
            {
                if (ped == null || !ped.Exists())
                    return;

                if (ped.IsDead)
                    return;

                // “拘捕=戴手铐(双手在后)”：不要每帧 ClearAll/HandsUp，否则会把手铐 idle 与后续跟随任务冲掉。
                try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, ped.Handle, true); } catch { }
                try { Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, ped.Handle, false); } catch { }
                try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_ANIMS, ped.Handle, false); } catch { }
                try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS, ped.Handle, false); } catch { }
                try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, ped.Handle, false); } catch { }

                ped.BlockPermanentEvents = true;
                ped.AlwaysKeepTask = true;

                EnsureHandcuffedIdle(ped);
            }
            catch (Exception ex)
            {
                ModLog.Error("[BehaviorKeeper] EnforceArrested native call failed: " + ex);
            }
        }


        private void EnforceEscorting(Ped ped)
        {
            if (ped == null || !ped.Exists())
                return;

            if (ped.IsDead)
                return;

            // Escort 行为的“强控制语义” + 维持手铐姿势（走路 clipset 在 OnFootExecutor 里做，这里只兜底“手铐+idle”）。
            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, ped.Handle, true); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_SWITCH_WEAPON, ped.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_ANIMS, ped.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS, ped.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, ped.Handle, false); } catch { }

            ped.BlockPermanentEvents = true;
            ped.AlwaysKeepTask = true;

            EnsureHandcuffedIdle(ped);
        }

        private void EnsureHandcuffedIdle(Ped ped)
        {
            if (ped == null || !ped.Exists()) return;
            if (ped.IsDead) return;

            // 确保嫌疑人仍是“实体可碰撞”的（防止某些流程把碰撞关掉后未恢复，出现玩家穿模）。
            try { Function.Call(Hash.SET_ENTITY_COLLISION, ped.Handle, true, true); } catch { }
            try
            {
                var player = Game.Player.Character;
                if (player != null && player.Exists())
                {
                    // 兜底：把双方 no-collision 解除（只要没在上拷演出阶段，就不应长期 no-collision）。
                    try { Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, player.Handle, ped.Handle, false); } catch { }
                    try { Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, ped.Handle, player.Handle, false); } catch { }
                }
            }
            catch { }

            try
            {
                int now = Game.GameTime;
                if (now - _lastAnimRequestMs > 1500)
                {
                    _lastAnimRequestMs = now;
                    try { Function.Call(Hash.REQUEST_ANIM_DICT, CUFF_DICT); } catch { }
                }

                bool loaded = false;
                try { loaded = Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, CUFF_DICT); } catch { loaded = false; }
                if (!loaded) return;

                // 跟随/押送在移动时，强行播 idle 会把走路动作打断，看起来像“手铐姿势丢/卡住”。
                // 所以：只有在嫌疑人基本静止时才补 idle；移动时如果正在播 idle 则停掉。
                float speed = 0f;
                try { speed = Function.Call<float>(Hash.GET_ENTITY_SPEED, ped.Handle); } catch { speed = 0f; }

                bool isPlaying = false;
                try { isPlaying = Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, ped.Handle, CUFF_DICT, CUFF_IDLE, 3); } catch { isPlaying = false; }

                if (speed > 0.20f)
                {
                    if (isPlaying)
                    {
                        try { Function.Call(Hash.STOP_ANIM_TASK, ped.Handle, CUFF_DICT, CUFF_IDLE, -4.0f); } catch { }
                    }
                    return;
                }

                if (isPlaying) return;

                // 只在未播放时补一次，避免每帧强行重播导致抖动/卡任务。
                try
                {
                    // flags=49 (Loop + OnlyAnimateUpperBody + AllowPlayerControl[对 ped 无影响])：更稳一点
                    Function.Call(Hash.TASK_PLAY_ANIM, ped.Handle, CUFF_DICT, CUFF_IDLE, 4.0f, -4.0f, -1, 49, 0.0f, false, false, false);
                }
                catch
                {
                    try { ped.Task.PlayAnimation(CUFF_DICT, CUFF_IDLE, 4.0f, -1, AnimationFlags.Loop); } catch { }
                }
            }
            catch { }
        }
    }
}
