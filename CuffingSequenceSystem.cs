using EF.PoliceMod.Core;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Linq;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 上拷演出（最小可用版）：
    /// - 触发：成功拘捕后（仅 CuffAndLead 风格）
    /// - 行为：短暂禁用玩家控制 -> 玩家走到嫌疑人背后 -> 播放上拷动画（best-effort）-> 保持嫌疑人手铐
    /// 
    /// 目标：
    /// 1) 修复“动画执行完仍无法操控”的卡控问题（避免把玩家一直留在 task 状态）
    /// 2) 缩短禁控窗口：只在走位/对位阶段禁控，动画开始后尽快恢复玩家控制，手感更自然
    /// 3) 通过临时无碰撞 + 走位对齐，减少穿模/抖动
    /// </summary>
    public class CuffingSequenceSystem
    {
        private bool _active;
        private int _suspectHandle = -1;
        private int _startedAtMs;
        private int _stage;
        private bool _noCollision;

        // 走位/对位参数（减少瞬移穿模 + 缩短禁控）
        private Vector3 _targetBehind;
        private int _moveStartedAtMs;
        private const int MOVE_TIMEOUT_MS = 1600;
        private const float BEHIND_OFFSET_Y = -0.90f;
        private const float BEHIND_OFFSET_X = 0.12f;

        private void EndSequence(bool forceRestoreControl)
        {
            // 先恢复碰撞（避免后续流程“穿人”）
            try
            {
                if (_noCollision)
                {
                    try
                    {
                        var suspect = ResolveSuspect();
                        var player = Game.Player.Character;
                        if (suspect != null && suspect.Exists() && player != null && player.Exists())
                        {
                            Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, player.Handle, suspect.Handle, false);
                            Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, suspect.Handle, player.Handle, false);
                        }
                    }
                    catch { }
                    _noCollision = false;
                }
            }
            catch { }

            // 无论如何都尝试解控（就算前面某阶段异常退出）
            try { LockPlayerControl(false); } catch { }

            // 关键修复：同步场景/动画结束后，玩家可能仍残留在 task/anim 状态，表现为“动画完了还不能移动”。
            // 这里做一次温和 stop anim + 清 secondary task（必要时再清 tasks）。
            try
            {
                var player = Game.Player.Character;
                if (player != null && player.Exists())
                {
                    // 温和停止可能的动画（不依赖是否真的在播）
                    try { Function.Call(Hash.STOP_ANIM_TASK, player.Handle, "mp_arrest_paired", "cop_p2_back_left", -4.0f); } catch { }
                    try { Function.Call(Hash.STOP_ANIM_TASK, player.Handle, "mp_arresting", "a_uncuff", -4.0f); } catch { }

                    // secondary task 残留最常见，优先清掉
                    try { Function.Call(Hash.CLEAR_PED_SECONDARY_TASK, player.Handle); } catch { }

                    // 强解：直接清任务（立即版更“硬”，但你这边是“必现卡住”，必须用它兜底）
                    try { Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, player.Handle); } catch { }
                }
            }
            catch { }

            _active = false;
        }

        public CuffingSequenceSystem()
        {
            EventBus.Subscribe<CuffingSequenceRequestedEvent>(OnRequested);
        }

        private void OnRequested(CuffingSequenceRequestedEvent e)
        {
            try
            {
                if (e.SuspectHandle <= 0) return;

                _active = true;
                _suspectHandle = e.SuspectHandle;
                _startedAtMs = Game.GameTime;
                _stage = 0;

                ModLog.Info("[CuffSeq] Start (suspect=" + _suspectHandle + ")");
            }
            catch (Exception ex)
            {
                ModLog.Error("[CuffSeq] OnRequested error: " + ex);
            }
        }

        private Ped ResolveSuspect()
        {
            try
            {
                return World.GetAllPeds().FirstOrDefault(p => p != null && p.Exists() && p.Handle == _suspectHandle);
            }
            catch
            {
                return null;
            }
        }

        private void LockPlayerControl(bool lockControl)
        {
            try
            {
                Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, !lockControl, 0);
            }
            catch { }
        }

        public void Tick()
        {
            if (!_active) return;

            var player = Game.Player.Character;
            if (player == null || !player.Exists()) { EndSequence(true); return; }

            var suspect = ResolveSuspect();
            if (suspect == null || !suspect.Exists() || suspect.IsDead)
            {
                EndSequence(true);
                return;
            }

            // 0) 初始化：禁控 + 稳定嫌疑人 + 计算“身后站位点”（先走过去，减少瞬移穿模）
            if (_stage == 0)
            {
                try
                {
                    // 忙碌态兜底：确保不会被“busy”挡住后续抱头/押送
                    try { EFCore.Instance?.GetSuspectController()?.UnmarkBusy(suspect.Handle); } catch { }

                    // 上拷演出只支持玩家步行
                    try
                    {
                        if (player.IsInVehicle())
                        {
                            Notification.Show("~y~请下车后再上拷");
                            EndSequence(true);
                            return;
                        }
                    }
                    catch { }

                    // 只在走位/对位期间禁控
                    LockPlayerControl(true);

                    // 临时禁用双方碰撞，避免走位/贴身动画互相挤压导致穿模/抖动
                    try
                    {
                        Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, player.Handle, suspect.Handle, true);
                        Function.Call(Hash.SET_ENTITY_NO_COLLISION_ENTITY, suspect.Handle, player.Handle, true);
                        _noCollision = true;
                    }
                    catch { }

                    // 让嫌疑人稳定站住（别乱转/乱走）
                    try { Function.Call(Hash.TASK_STAND_STILL, suspect.Handle, 2500); } catch { }

                    // 计算嫌疑人身后的点（offset 更稳）
                    try { _targetBehind = Function.Call<Vector3>(Hash.GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS, suspect.Handle, BEHIND_OFFSET_X, BEHIND_OFFSET_Y, 0.0f); }
                    catch { _targetBehind = suspect.Position - suspect.ForwardVector * Math.Abs(BEHIND_OFFSET_Y); }
                    _targetBehind.Z = suspect.Position.Z;

                    // 尽量找地面 Z
                    try
                    {
                        var outZ = new OutputArgument();
                        if (Function.Call<bool>(Hash.GET_GROUND_Z_FOR_3D_COORD, _targetBehind.X, _targetBehind.Y, _targetBehind.Z + 50.0f, outZ, false))
                        {
                            _targetBehind.Z = outZ.GetResult<float>();
                        }
                    }
                    catch { }

                    try { Function.Call(Hash.REQUEST_COLLISION_AT_COORD, _targetBehind.X, _targetBehind.Y, _targetBehind.Z); } catch { }

                    // 自动走位到点（比瞬移自然，也更少穿模）
                    try
                    {
                        Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, player.Handle, _targetBehind.X, _targetBehind.Y, _targetBehind.Z, 1.15f, MOVE_TIMEOUT_MS, suspect.Heading, 0.05f);
                    }
                    catch { }

                    _moveStartedAtMs = Game.GameTime;
                    _stage = 1;
                    return;
                }
                catch (Exception ex)
                {
                    ModLog.Error("[CuffSeq] stage0 error: " + ex);
                    EndSequence(true);
                    return;
                }
            }

            // 1) 等玩家走位到身后 + 最终对位（轻微“吸附”一次即可）
            if (_stage == 1)
            {
                try
                {
                    bool timeout = Game.GameTime - _moveStartedAtMs > MOVE_TIMEOUT_MS;
                    float dist = 999f;
                    try { dist = player.Position.DistanceTo(_targetBehind); } catch { }

                    if (dist < 0.55f || timeout)
                    {
                        // 最终对位：不再瞬移玩家（会出现“卡一下”的视觉断层），只做朝向调整
                        try { Function.Call(Hash.SET_ENTITY_HEADING, player.Handle, suspect.Heading); } catch { }

                        // 也把嫌疑人贴地一下，减少两人 Z 不一致导致的“叠模/卡模”
                        try { Function.Call(Hash.PLACE_OBJECT_ON_GROUND_PROPERLY, suspect.Handle); } catch { }
                        try { Function.Call(Hash.PLACE_OBJECT_ON_GROUND_PROPERLY, player.Handle); } catch { }

                        // 面向（让配对动画更贴合）
                        try { Function.Call(Hash.TASK_TURN_PED_TO_FACE_ENTITY, suspect.Handle, player.Handle, 450); } catch { }
                        try { Function.Call(Hash.TASK_TURN_PED_TO_FACE_ENTITY, player.Handle, suspect.Handle, 450); } catch { }

                        _stage = 2;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ModLog.Error("[CuffSeq] stage1 error: " + ex);
                    EndSequence(true);
                    return;
                }
            }

            // 2) 播放上拷动画（best-effort），并给嫌疑人上手铐
            if (_stage == 2)
            {
                try
                {
                    // 嫌疑人手铐 + 禁 ragdoll
                    try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
                    try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, suspect.Handle, false); } catch { }
                    try { suspect.BlockPermanentEvents = true; } catch { }
                    try { suspect.AlwaysKeepTask = true; } catch { }

                    bool played = false;

                    // 优先：同步场景 + 配对动画（玩家+嫌疑人都有动作）
                    try
                    {
                        Function.Call(Hash.REQUEST_ANIM_DICT, "mp_arrest_paired");
                        int t = Game.GameTime + 900;
                        while (Game.GameTime < t)
                        {
                            if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_arrest_paired")) break;
                            Script.Wait(0);
                        }

                        if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_arrest_paired"))
                        {
                            var pos = suspect.Position;
                            float heading = suspect.Heading;
                            int scene = Function.Call<int>(Hash.CREATE_SYNCHRONIZED_SCENE, pos.X, pos.Y, pos.Z, 0f, 0f, heading, 2);
                            Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, player.Handle, scene, "mp_arrest_paired", "cop_p2_back_left", 4.0f, -4.0f, 0, 0, 0f, 0);
                            Function.Call(Hash.TASK_SYNCHRONIZED_SCENE, suspect.Handle, scene, "mp_arrest_paired", "crook_p2_back_left", 4.0f, -4.0f, 0, 0, 0f, 0);
                            Function.Call(Hash.SET_SYNCHRONIZED_SCENE_LOOPED, scene, false);
                            played = true;
                        }
                    }
                    catch { played = false; }

                    if (!played)
                    {
                        // 回退：仅玩家做 a_uncuff，嫌疑人保持站立/被铐 idle
                        try
                        {
                            Function.Call(Hash.REQUEST_ANIM_DICT, "mp_arresting");
                            int t2 = Game.GameTime + 800;
                            while (Game.GameTime < t2)
                            {
                                if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_arresting")) break;
                                Script.Wait(0);
                            }

                            if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "mp_arresting"))
                            {
                                // 用 native 版本，避免 duration 导致的“硬禁控”，参考函数大全 TASK_PLAY_ANIM flags=32 可启用玩家控制
                                try
                                {
                                    Function.Call(Hash.TASK_PLAY_ANIM, player.Handle, "mp_arresting", "a_uncuff", 4.0f, -4.0f, 1100, 32, 0.0f, false, false, false);
                                }
                                catch
                                {
                                    player.Task.PlayAnimation("mp_arresting", "a_uncuff", 4.0f, 1100, AnimationFlags.None);
                                }

                                try { suspect.Task.PlayAnimation("mp_arresting", "idle", 4.0f, -1, AnimationFlags.Loop); } catch { }
                                played = true;
                            }
                        }
                        catch { }
                    }

                    // 动画已开始：立刻解控（禁控窗口到此结束）
                    try { LockPlayerControl(false); } catch { }

                    _stage = 3;
                    return;
                }
                catch (Exception ex)
                {
                    ModLog.Error("[CuffSeq] stage2 error: " + ex);
                    EndSequence(true);
                    return;
                }
            }

            // 3) 收尾：让动画跑完一小段后只做清理（不强行清任务），避免“结束瞬间跳回去很假”
            if (_stage == 3)
            {
                try
                {
                    // 给动画一点时间（更自然），但不再整段都禁控
                    if (Game.GameTime - _startedAtMs > 1550)
                    {
                        EndSequence(false);
                        ModLog.Info("[CuffSeq] Done");
                        return;
                    }

                    // 兜底超时：防止动画/同步场景异常导致残留状态
                    if (Game.GameTime - _startedAtMs > 3600)
                    {
                        try { player.Task.ClearAll(); } catch { }
                        EndSequence(true);
                        ModLog.Info("[CuffSeq] Done (timeout)");
                        return;
                    }
                }
                catch { }
            }
        }
    }
}