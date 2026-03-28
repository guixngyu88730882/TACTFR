using EF.PoliceMod;
using EF.PoliceMod.Core;
using EF.PoliceMod.Gameplay;
using EF.PoliceMod.Suspects;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EF.PoliceMod.Executors
{
    /// <summary>
    /// 嫌疑人车辆押送执行器
    /// - 不持有私有状态
    /// - 不决定状态迁移
    /// - 只监听 SuspectStateHub 并执行行为
    /// </summary>
    public class SuspectVehicleEscortExecutor
    {
        private static readonly Random _rand = new Random();

        private readonly SuspectController _suspectController;
        private readonly SuspectStateHub _stateHub;
        private readonly SuspectStyleRegistry _styleRegistry;
        private readonly EF.PoliceMod.Suspects.SuspectContextRegistry _ctxRegistry;
        private readonly EF.PoliceMod.Suspects.StateHubRouter _hubRouter;

        // 车门动作节流：避免每帧重复 Open/Close 导致车辆物理抖动/倾斜甚至翻车
        private const int DOOR_ACTION_COOLDOWN_MS = 650;
        private int _lastDoorActionAtMs = 0;
        private int _lastDoorActionVehicleHandle = -1;
        private int _lastDoorActionDoorIndex = -1;
        private bool TryBeginDoorAction(int nowMs, Vehicle veh, int doorIndex)
        {
            if (veh == null || !veh.Exists()) return false;
            int vh = veh.Handle;
            if (_lastDoorActionVehicleHandle == vh
                && _lastDoorActionDoorIndex == doorIndex
                && nowMs - _lastDoorActionAtMs < DOOR_ACTION_COOLDOWN_MS)
                return false;
            _lastDoorActionVehicleHandle = vh;
            _lastDoorActionDoorIndex = doorIndex;
            _lastDoorActionAtMs = nowMs;
            return true;
        }

        // I 逼停线旁路（抱头线专用）：允许免 L 锁定 + H 拘捕 直接 G/E 操作。

        private const int PULLOVER_BYPASS_TTL_MS = 90 * 1000;
        private readonly PullOverEscortBypassState _pullOverBypass = new PullOverEscortBypassState(PULLOVER_BYPASS_TTL_MS);

        // --- 跟随控制状态（用于强制：必须先按 G 使嫌疑人跟随，才允许按 E 上车） ---
        // 去重/状态记录：防止重复发布 boarded 事件
        private int _lastBoardedSuspectHandle = -1;
        private int _lastBoardedAtMs = 0; // Game.GameTime ms
                                          // 配置：是否强制先按 G 让嫌疑人跟随，才能按 E 让其上车
                                          // 强制：必须先按 G 让嫌疑人进入"跟随/押送"状态，才允许按 E 让其上车。
        private bool _requireFollowBeforeBoard = true;
        private int _lastVehicleInteractRejectAtMs = 0;

        // 玩家上下车边沿检测：实现"玩家上车后嫌疑人自动上车 / 玩家下车后嫌疑人自动下车"
        private bool _wasPlayerInVehicle = false;

        // EnteringVehicle 超时保护
        private int _enteringVehicleStartMs = 0;
        private const int ENTERING_VEHICLE_TIMEOUT_MS = 8000; // 8秒超时
        private const int BOARDING_STAGGER_MS = 450;
        private const int BOARDING_RETRY_DELAY_MS = 900;
        private const int BOARDING_APPROACH_DELAY_MS = 450;
        private const int MAX_BOARDING_RETRIES = 3;
        private const float BOARDING_BLOCK_RADIUS = 1.2f;

        // ExitingVehicle 超时保护
        private int _exitingVehicleStartMs = 0;
        private const int EXITING_VEHICLE_TIMEOUT_MS = 6000; // 6秒超时

        private readonly CuffedVehicleDoorFlow _cuffedDoorFlow = new CuffedVehicleDoorFlow();

        private bool _handlingStateChange = false;
        private SuspectStateHub _subscribedHub = null;
        private readonly System.Collections.Generic.HashSet<int> _subscribedHubHandles = new System.Collections.Generic.HashSet<int>();
        // 被拷嫌疑人步态/背手姿势的"时间戳状态"（具体 native 行为已抽离到 CuffedPoseOps）


        private int _lastClipsetRequestMs = 0;
        private int _lastClipsetApplyMs = 0;
        private const int CLIPSET_REQUEST_COOLDOWN_MS = 1500;
        private const int CLIPSET_REAPPLY_COOLDOWN_MS = 650;

        private int _lastCuffedUpperBodyPoseMs = 0;
        private const int CUFFED_UPPERBODY_COOLDOWN_MS = 650;

        private void EnsureCuffedClipset(Ped suspect)
        {
            if (suspect == null || !suspect.Exists()) return;
            if (suspect.IsDead) return;
            if (!VehicleEscortLine.IsCuffed(GetStyleFor(suspect.Handle))) return;

            CuffedPoseOps.EnsureClipset(
                suspect,
                ref _lastClipsetRequestMs,
                ref _lastClipsetApplyMs,
                CLIPSET_REQUEST_COOLDOWN_MS,
                CLIPSET_REAPPLY_COOLDOWN_MS
            );
        }

        private void EnsureCuffedUpperBodyPose(Ped suspect)
        {
            if (suspect == null || !suspect.Exists()) return;
            if (suspect.IsDead) return;
            if (!VehicleEscortLine.IsCuffed(GetStyleFor(suspect.Handle))) return;

            CuffedPoseOps.EnsureUpperBodyIdle(
                suspect,
                ref _lastCuffedUpperBodyPoseMs,
                CUFFED_UPPERBODY_COOLDOWN_MS
            );
        }





        // === 合并：CuffedVehicleEscortFlow / CuffedVehicleEscortTickFlow ===



        private void StartCuffedExitVehicle(Ped suspect, ArrestActionStyle style)
        {
            if (_suspectController == null) return;
            if (suspect == null || !suspect.Exists() || suspect.IsDead) return;
            if (!suspect.IsInVehicle()) return;

            try
            {
                if (_suspectController.IsBusy(suspect)) return;
            }
            catch { }

            try
            {
                try { _suspectController.MarkBusy(suspect); } catch { }

                // Keep handcuffs enabled BEFORE opening door / issuing exit task
                if (IsCuffed(style))
                {
                    try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
                    try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
                }

                if (ShouldAutoDoors(style))
                {
                    var veh = suspect.CurrentVehicle;
                    if (veh != null && veh.Exists())
                    {
                        int doorIndex = GetRearDoorIndexForSuspect(veh, suspect);
                        doorIndex = NormalizeDoorIndex(veh, doorIndex);
                        try { _cuffedDoorFlow.RecordExitDoor(veh.Handle, doorIndex); } catch { }
                        try
                        {
                            if (TryBeginDoorAction(Game.GameTime, veh, doorIndex))
                                VehicleDoorOps.OpenDoor(veh, doorIndex);
                        }
                        catch { }
                    }
                }

                try { suspect.Task.ClearAll(); } catch { }
                try
                {
                    var veh2 = suspect.CurrentVehicle;
                    if (veh2 != null && veh2.Exists())
                        Function.Call(Hash.TASK_LEAVE_VEHICLE, suspect.Handle, veh2.Handle, 256);
                }
                catch { }
            }
            catch
            {
                try { _suspectController.ClearBusy(suspect); } catch { }
            }
        }

        private void OnCuffedEnteredVehicle(Ped suspect, ArrestActionStyle style, int nowMs)
        {
            if (_suspectController == null) return;
            if (suspect == null || !suspect.Exists()) return;
            if (!suspect.IsInVehicle()) return;

            int handle = suspect.Handle;
            if (_lastBoardedSuspectHandle == handle && nowMs - _lastBoardedAtMs < 2000) return;

            if (_requireFollowBeforeBoard && !IsSuspectFollowing(handle)) return;
            _lastBoardedSuspectHandle = handle;
            _lastBoardedAtMs = nowMs;
            try { EventBus.Publish(new EF.PoliceMod.Core.SuspectBoardedVehicleEvent(handle)); } catch { }
            try
            {
                var veh = suspect.CurrentVehicle;
                if (veh != null && veh.Exists())
                {
                    int doorIndex = GetRearDoorIndexForSuspect(veh, suspect);
                    doorIndex = NormalizeDoorIndex(veh, doorIndex);
                    if (ShouldAutoDoors(style) && TryBeginDoorAction(nowMs, veh, doorIndex))
                        VehicleDoorOps.ShutDoor(veh, doorIndex);
                }
            }
            catch { }

            // Add at the end of OnCuffedEnteredVehicle, after the existing door close:
            try
            {
                var veh = suspect.CurrentVehicle;
                if (veh != null && veh.Exists() && ShouldAutoDoors(style))
                {
                    for (int di = 0; di <= 3; di++)
                    {
                        try
                        {
                            bool isOpen = Function.Call<float>(
                                Hash.GET_VEHICLE_DOOR_ANGLE_RATIO, veh.Handle, di) > 0.1f;
                            if (isOpen)
                            {
                                VehicleDoorOps.ShutDoor(veh, di);
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }

            try { _suspectController.ClearBusy(suspect); } catch { }

            ResetBoardingReservation(handle);
            SetSuspectFollowing(handle, false);
        }

        private bool TickCuffedVehicleEscort(
            Ped suspect,
            Ped player,
            ArrestActionStyle style,
            int nowMs)
        {
            if (_stateHub == null) return false;
            if (suspect == null || !suspect.Exists()) return false;
            if (player == null || !player.Exists()) return false;

            bool playerInVehicle = false;
            try { playerInVehicle = player.IsInVehicle(); } catch { playerInVehicle = false; }

            // 过渡期保持手铐/姿势
            try
            {
                if (ShouldAutoVehicleSync(style) && IsState(SuspectState.InVehicle))
                {
                    try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
                    try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
                }
            }
            catch { }

            // 玩家上车边沿：嫌疑人自动上车（仅被拷线）
            if (playerInVehicle && !_wasPlayerInVehicle)
            {
                try
                {
                    if (ShouldAutoVehicleSync(style)
                        && IsState(SuspectState.Escorting)
                        && !suspect.IsInVehicle()
                        && IsSuspectFollowing(suspect.Handle)
                        && player.CurrentVehicle != null
                        && player.CurrentVehicle.Exists())
                    {
                        bool gettingIn = false;
                        try { gettingIn = Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, suspect.Handle); } catch { gettingIn = false; }

                        float suspectToVeh = suspect.Position.DistanceTo(player.CurrentVehicle.Position);
                        if (!gettingIn && suspectToVeh < 12.0f)
                        {
                            _enteringVehicleStartMs = 0;
                            ChangeState(SuspectState.EnteringVehicle);
                            _wasPlayerInVehicle = playerInVehicle;
                            ModLog.Info($"[Escort][Vehicle] Auto-board triggered (suspectToVeh={suspectToVeh:F1}m, handle={suspect.Handle})");
                            return true;
                        }
                        else
                        {
                            ModLog.Info($"[Escort][Vehicle] Auto-board skipped: dist={suspectToVeh:F1}m, gettingIn={gettingIn}");
                        }
                    }
                }
                catch { }
            }

            // 玩家下车边沿：嫌疑人自动下车（仅被拷线）
            if (!playerInVehicle && _wasPlayerInVehicle)
            {
                try
                {
                    if (ShouldAutoVehicleSync(style)
                        && IsState(SuspectState.InVehicle)
                        && suspect.IsInVehicle())
                    {
                        bool gettingIn = false;
                        try { gettingIn = Function.Call<bool>(Hash.IS_PED_GETTING_INTO_A_VEHICLE, suspect.Handle); } catch { gettingIn = false; }
                        if (!gettingIn)
                        {
                            ChangeState(SuspectState.ExitingVehicle);
                            _wasPlayerInVehicle = playerInVehicle;
                            return true;
                        }
                    }
                }
                catch { }
            }

            _wasPlayerInVehicle = playerInVehicle;

            // EnteringVehicle -> InVehicle 或超时回退
            if (IsState(SuspectState.EnteringVehicle))
            {
                if (suspect.IsInVehicle())
                {
                    _enteringVehicleStartMs = 0;
                    if (IsCuffed(style))
                    {
                        RestoreCuffConstraints(suspect);
                        ModLog.Info($"[Escort][Vehicle] Cuff constraints restored after vehicle entry (handle={suspect.Handle})");
                    }
                    ChangeState(SuspectState.InVehicle);
                    try { OnCuffedEnteredVehicle(suspect, style, nowMs); } catch { }
                    return true;
                }

                if (_enteringVehicleStartMs == 0)
                {
                    _enteringVehicleStartMs = nowMs;
                }
                else if (TryHandleBoardingRecovery(suspect.Handle, suspect, style, nowMs))
                {
                    return true;
                }
            }


            // ExitingVehicle -> Escorting 或超时回退
            if (IsState(SuspectState.ExitingVehicle))
            {
                if (!suspect.IsInVehicle())
                {
                    _exitingVehicleStartMs = 0;
                    ChangeState(SuspectState.Escorting);
                    try { OnSuspectExitVehicle(); } catch { }
                    try
                    {
                        _cuffedDoorFlow.TryShutDoorAfterExit(
                            style,
                            (h) => FindVehicleByHandle(h),
                            (v, d) => NormalizeDoorIndex(v, d)
                        );
                    }
                    catch { }
                    try
                    {
                        if (ShouldAutoDoors(style))
                        {
                            var veh = player != null && player.Exists() ? player.CurrentVehicle : null;
                            if (veh == null || !veh.Exists()) veh = World.GetNearbyVehicles(suspect, 10.0f).FirstOrDefault(v => v != null && v.Exists());
                            if (veh != null && veh.Exists())
                            {
                                try { VehicleDoorOps.ShutDoor(veh, NormalizeDoorIndex(veh, 1)); } catch { }
                                try { VehicleDoorOps.ShutDoor(veh, NormalizeDoorIndex(veh, 3)); } catch { }
                            }
                        }
                    }
                    catch { }
                    return true;
                }

                if (_exitingVehicleStartMs == 0)
                {
                    _exitingVehicleStartMs = nowMs;
                }
                else if (nowMs - _exitingVehicleStartMs > EXITING_VEHICLE_TIMEOUT_MS)
                {
                    ModLog.Warn($"[Escort][Vehicle] ExitingVehicle timeout ({EXITING_VEHICLE_TIMEOUT_MS}ms) -> force warp out");
                    _exitingVehicleStartMs = 0;

                    try
                    {
                        var veh = suspect.CurrentVehicle;
                        suspect.Task.WarpOutOfVehicle(veh);
                    }
                    catch { }

                    ChangeState(SuspectState.Escorting);
                    return true;
                }
            }

            return false;
        }

        private static VehicleSeat FindAvailableSeatForSuspect(Vehicle vehicle)
        {
            if (vehicle == null || !vehicle.Exists()) return VehicleSeat.None;

            try
            {
                if (vehicle.IsSeatFree(VehicleSeat.RightRear)) return VehicleSeat.RightRear;
                if (vehicle.IsSeatFree(VehicleSeat.LeftRear)) return VehicleSeat.LeftRear;
            }
            catch { }

            try { if (vehicle.IsSeatFree(VehicleSeat.Passenger)) return VehicleSeat.Passenger; } catch { }
            return VehicleSeat.None;
        }

        private static int GetDoorIndexForSeat(VehicleSeat seat)
        {
            if (seat == VehicleSeat.RightRear) return 3;
            if (seat == VehicleSeat.LeftRear) return 1;
            if (seat == VehicleSeat.Passenger) return 2;
            return 2;
        }

        private static int NormalizeDoorIndex(Vehicle vehicle, int desiredDoorId)
        {
            if (vehicle == null || !vehicle.Exists()) return desiredDoorId;
            try
            {
                bool valid = Function.Call<bool>(Hash.GET_IS_DOOR_VALID, vehicle.Handle, desiredDoorId);
                if (valid) return desiredDoorId;
            }
            catch { }

            if (desiredDoorId == 1) return 2;
            if (desiredDoorId == 3) return 2;
            return desiredDoorId;
        }
        // (removed) duplicated GetRearDoorIndexForSuspect(Vehicle, Ped)
        // NOTE：保持对旧调用点（VehicleEscortLine.*）的兼容，但真实规则以本类为准。
        // 后续会把外部引用逐步切到 SuspectVehicleEscortExecutor 内部静态类，再删除独立文件。

        public static class VehicleEscortLine
        {
            internal const float DEFAULT_MAX_E_INTERACT_DISTANCE = 9.5f;

            internal const float PLAYER_SUSPECT_E_INTERACT_DISTANCE = 6.5f;
            internal static bool IsCuffed(ArrestActionStyle style) => style == ArrestActionStyle.CuffAndLead;

            internal static bool ShouldAutoDoors(ArrestActionStyle style) => IsCuffed(style);
            internal static bool ShouldAutoVehicleSync(ArrestActionStyle style) => IsCuffed(style);

            internal static float MaxEInteractDistance(ArrestActionStyle style)
            {
                // 单人被拷线适当放宽，减少"明明在押送但 E 提示太远"的误判。
                if (IsCuffed(style)) return 11.0f;
                return DEFAULT_MAX_E_INTERACT_DISTANCE;
            }
        }

        internal static class VehicleSeatDoorOps
        {
            internal static int GetSeatIndexForDoorId(int doorId)
            {
                // 兼容：仅覆盖常见门 id；未知门 id fallback passenger
                // GTA V 常见：                // 0=FL, 1=FR, 2=RL, 3=RR（不同 API/脚本会有差异，这里按本项目既有使用方式兜底）

                if (doorId == 3) return (int)VehicleSeat.RightRear;
                if (doorId == 2) return (int)VehicleSeat.Passenger;
                if (doorId == 1) return (int)VehicleSeat.LeftRear;
                return (int)VehicleSeat.Passenger;
            }
        }
        

        private static bool IsCuffed(ArrestActionStyle style) => VehicleEscortLine.IsCuffed(style);
        private static bool ShouldAutoDoors(ArrestActionStyle style) => VehicleEscortLine.ShouldAutoDoors(style);
        private static bool ShouldAutoVehicleSync(ArrestActionStyle style) => VehicleEscortLine.ShouldAutoVehicleSync(style);




        // 兜底：部分"同步上拷场景/任务切换"可能导致嫌疑人变成非实体（无碰撞/冻结/不动态）
        // 这里每帧做一次 best-effort 修复，避免出现你说的"能穿模过去、嫌疑人一动不动"。
        private void EnsureSuspectIsSolid(Ped suspect)
        {
            if (suspect == null || !suspect.Exists()) return;
            if (suspect.IsDead) return;

            // ENTITY
            try { Function.Call(Hash.FREEZE_ENTITY_POSITION, suspect.Handle, false); } catch { }
            // 有些情况下仅 SET_ENTITY_COLLISION=true 仍然无法恢复（实体被"完全禁用碰撞"）
            try { Function.Call(Hash.SET_ENTITY_COMPLETELY_DISABLE_COLLISION, suspect.Handle, false, false); } catch { }
            try { Function.Call(Hash.SET_ENTITY_COLLISION, suspect.Handle, true, true); } catch { }
            try { Function.Call(Hash.SET_ENTITY_DYNAMIC, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_ENTITY_HAS_GRAVITY, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.ACTIVATE_PHYSICS, suspect.Handle); } catch { }

            // PED：被同步场景/手铐状态搞乱时，强制恢复可物理交互的标志（不触发 ragdoll，仅恢复能力）
            try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_RAGDOLL_FROM_PLAYER_IMPACT, suspect.Handle, true); } catch { }
        }

        private void SuspendCuffConstraints(Ped suspect)
        {
            if (suspect == null || !suspect.Exists()) return;
            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, suspect.Handle, 0.25f); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, suspect.Handle, true); } catch { }
        }

        private void RestoreCuffConstraints(Ped suspect)
        {
            if (suspect == null || !suspect.Exists()) return;
            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
        }


        public SuspectVehicleEscortExecutor(
            SuspectController suspectController,
            SuspectStateHub stateHub,
            SuspectStyleRegistry styleRegistry,
            EF.PoliceMod.Suspects.SuspectContextRegistry ctxRegistry,
            EF.PoliceMod.Suspects.StateHubRouter hubRouter)
        {
            _suspectController = suspectController;
            _stateHub = stateHub;
            _styleRegistry = styleRegistry;
            _ctxRegistry = ctxRegistry;
            _hubRouter = hubRouter;

            // 输入 = 意图
            EventBus.Subscribe<EscortVehicleInteractEvent>(OnVehicleInteract);
            // 订阅：G 按键（跟随 / 下车）发布的事件
            EventBus.Subscribe<SuspectFollowRequestEvent>(OnFollowRequest);
            // 订阅：I 逼停自动下车后激活"免锁定/免拘捕"的 E 旁路（仅抱头线）
            EventBus.Subscribe<PullOverEscortBypassActivatedEvent>(OnPullOverBypassActivated);
            // 订阅：案件结束事件，重置跟随状态
            EventBus.Subscribe<CaseEndedEvent>(OnCaseEnded);

            // 状态 = 法律
        _stateHub.OnStateChanged += OnSuspectStateChanged;
        _subscribedHub = _stateHub;


            ModLog.Info("[Escort][Vehicle] Executor initialized (StateHub driven)");
        }

        private SuspectStateHub GetActiveHub()
        {
            if (_subscribedHub != null) return _subscribedHub;
            try
            {
                var suspect = _suspectController?.GetCurrentSuspect();
                if (suspect != null && suspect.Exists() && _hubRouter != null)
                {
                    return _hubRouter.GetHubFor(suspect.Handle);
                }
            }
            catch { }
            return _stateHub;
        }

        private SuspectStateHub GetActiveHubFor(int handle)
        {
            try
            {
                if (handle > 0 && _hubRouter != null)
                {
                    return _hubRouter.GetHubFor(handle);
                }
            }
            catch { }
            return _stateHub;
        }

        private bool IsState(SuspectState state)
        {
            try
            {
                var suspect = _suspectController?.GetCurrentSuspect();
                if (suspect != null && suspect.Exists())
                    return IsStateFor(suspect.Handle, state);
            }
            catch { }
            return GetActiveHub().Is(state);
        }

        private bool IsStateFor(int handle, SuspectState state)
        {
            return GetActiveHubFor(handle).Is(state);
        }

        private void ChangeState(SuspectState newState)
        {
            try
            {
                var suspect = _suspectController?.GetCurrentSuspect();
                int h = suspect != null && suspect.Exists() ? suspect.Handle : -1;
                if (_hubRouter != null)
                {
                    var hub = _hubRouter.GetWriterHubFor(h, newState);
                    hub.ChangeState(newState);
                    return;
                }
            }
            catch { }
            _stateHub.ChangeState(newState);
        }

        public void SubscribeToPerHandleHub(SuspectStateHub perHandleHub)
        {
            if (perHandleHub == null) return;
            int hubHandle = perHandleHub.SuspectHandle;

            if (_subscribedHubHandles.Contains(hubHandle)) return;
            _subscribedHubHandles.Add(hubHandle);

            perHandleHub.OnStateChanged += (oldS, newS) =>
            {
                OnSuspectStateChangedForHandle(hubHandle, oldS, newS);
            };

            _subscribedHub = perHandleHub;
            ModLog.Info($"[Escort][Vehicle] Subscribed to per-handle hub (handle={hubHandle})");
        }

        private ArrestActionStyle GetStyle()
        {
            try
            {
                var suspect = _suspectController?.GetCurrentSuspect();
                if (suspect != null && suspect.Exists())
                    return ArrestStyleResolver.GetForHandle(suspect.Handle, _suspectController, _styleRegistry, _ctxRegistry);
            }
            catch { }

            try { return _suspectController != null ? _suspectController.CurrentArrestStyle : ArrestActionStyle.CuffAndLead; }
            catch { return ArrestActionStyle.CuffAndLead; }
        }

        private ArrestActionStyle GetStyleFor(int suspectHandle)
        {
            return ArrestStyleResolver.GetForHandle(suspectHandle, _suspectController, _styleRegistry, _ctxRegistry);
        }

        private bool IsSuspectFollowing(int handle)
        {
            try
            {
                if (handle > 0 && _ctxRegistry != null)
                    return _ctxRegistry.GetFollowRequested(handle);
            }
            catch { }

            return false;
        }

        private void SetSuspectFollowing(int handle, bool following)
        {
            try
            {
                if (_ctxRegistry == null)
                    return;

                if (handle > 0)
                {
                    _ctxRegistry.SetFollowRequested(handle, following);
                }
                else if (!following)
                {
                    _ctxRegistry.ClearFollowRequestedAll();
                }
            }
            catch { }
        }

        private int GetSeatIndexForDoorId(int doorId) => VehicleSeatDoorOps.GetSeatIndexForDoorId(doorId);

        private Ped FindPedByHandle(int handle)
        {
            if (handle <= 0) return null;
            try
            {
                if (!Function.Call<bool>(Hash.DOES_ENTITY_EXIST, handle))
                    return null;
                return Entity.FromHandle(handle) as Ped;
            }
            catch { return null; }
        }

        private Vehicle FindVehicleByHandle(int handle)
        {
            if (handle <= 0) return null;
            try
            {
                if (!Function.Call<bool>(Hash.DOES_ENTITY_EXIST, handle))
                    return null;
                return Entity.FromHandle(handle) as Vehicle;
            }
            catch { return null; }
        }

        private void ApplyActionToOtherCompliantCaseSuspects(int currentHandle, Action<Ped> action)
        {
            if (action == null) return;
            try
            {
                var mgr = EFCore.Instance?.GetCaseManager();
                var handles = mgr?.SuspectHandles;
                if (handles == null) return;

                foreach (var h in handles)
                {
                    if (h <= 0 || h == currentHandle) continue;
                    try
                    {
                        if (_suspectController == null || !_suspectController.IsHandleCompliant(h)) continue;
                        var ped = FindPedByHandle(h);
                        if (ped == null || !ped.Exists() || ped.IsDead) continue;
                        action(ped);
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void TryMakeSecondaryFollow(Ped suspect)
        {
            if (suspect == null || !suspect.Exists() || suspect.IsDead) return;
            if (suspect.IsInVehicle()) return;

            try
            {
                var style = GetStyleFor(suspect.Handle);
                if (style == ArrestActionStyle.CuffAndLead)
                {
                    try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
                    try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
                }
                SuspectFollowOps.StartFollow(_suspectController, suspect, style);
            }
            catch { }

            // Transition secondary's hub to Escorting so boarding/exit checks work
            try
            {
                if (_hubRouter != null)
                {
                    var hub = _hubRouter.GetWriterHubFor(
                        suspect.Handle, SuspectState.Escorting);
                    if (hub != null && !hub.Is(SuspectState.Escorting))
                    {
                        // Restrained -> Escorting is a valid transition
                        hub.ChangeState(SuspectState.Escorting);
                        ModLog.Info($"[Escort] Secondary {suspect.Handle} hub -> Escorting");
                    }
                }
            }
            catch { }
        }

        private void TryMakeSecondaryBoard(Ped suspect, Ped player)
        {
            if (suspect == null || !suspect.Exists() || suspect.IsDead) return;
            if (player == null || !player.Exists()) return;
            if (suspect.IsInVehicle()) return;

            Vehicle targetVeh = null;
            try
            {
                if (player.IsInVehicle()) targetVeh = player.CurrentVehicle;
                else targetVeh = World.GetNearbyVehicles(player, 6.0f)
                    .FirstOrDefault(v => v != null && v.Exists());
            }
            catch { targetVeh = null; }
            if (targetVeh == null || !targetVeh.Exists()) return;

            // Find a seat that isn't being entered by the primary suspect
            VehicleSeat seat = VehicleSeat.None;
            try
            {
                // Try LeftRear first (primary usually takes RightRear)
                if (targetVeh.IsSeatFree(VehicleSeat.LeftRear)) 
                    seat = VehicleSeat.LeftRear;
                else if (targetVeh.IsSeatFree(VehicleSeat.RightRear)) 
                    seat = VehicleSeat.RightRear;
                else if (targetVeh.IsSeatFree(VehicleSeat.Passenger)) 
                    seat = VehicleSeat.Passenger;
            }
            catch { seat = VehicleSeat.None; }

            if (seat == VehicleSeat.None)
            {
                ModLog.Info($"[Escort] TryMakeSecondaryBoard: no seat for {suspect.Handle}");
                return;
            }

            var style = GetStyleFor(suspect.Handle);

            try
            {
                if (ShouldAutoDoors(style))
                {
                    int doorIndex = NormalizeDoorIndex(targetVeh, GetDoorIndexForSeat(seat));
                    try { VehicleDoorOps.OpenDoor(targetVeh, doorIndex); } catch { }
                    try { _cuffedDoorFlow.ArmPendingShutDoor(targetVeh.Handle, doorIndex, suspect.Handle, Game.GameTime); } catch { }
                }
            }
            catch { }

            try 
            { 
                suspect.Task.ClearAll();
                if (IsCuffed(style))
                {
                    SuspendCuffConstraints(suspect);
                }
                suspect.Task.EnterVehicle(targetVeh, seat); 
            }
            catch (Exception ex)
            {
                ModLog.Error($"[Escort] TryMakeSecondaryBoard task failed: {ex}");
                return;
            }

            if (IsCuffed(style))
            {
                try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
                try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
            }

            try
            {
                if (_hubRouter != null)
                {
                    var hub = _hubRouter.GetWriterHubFor(
                        suspect.Handle, SuspectState.EnteringVehicle);
                    if (hub != null && (hub.Is(SuspectState.Escorting) || hub.Is(SuspectState.Restrained)))
                    {
                        _enteringVehicleStartMs = 0;
                        hub.ChangeState(SuspectState.EnteringVehicle);
                    }
                }
            }
            catch { }
            ModLog.Info($"[Escort] Secondary {suspect.Handle} warped into {seat}");
        }

        private void TryMakeSecondaryExit(Ped suspect)
        {
            if (suspect == null || !suspect.Exists() || suspect.IsDead) return;
            if (!suspect.IsInVehicle()) return;

            var style = GetStyleFor(suspect.Handle);
            Vehicle veh = null;
            try { veh = suspect.CurrentVehicle; } catch { }
            try
            {
                if (ShouldAutoDoors(style) && veh != null && veh.Exists())
                {
                    int doorIndex = GetRearDoorIndexForSuspect(veh, suspect);
                    doorIndex = NormalizeDoorIndex(veh, doorIndex);
                    try { VehicleDoorOps.OpenDoor(veh, doorIndex); } catch { }
                    try { _cuffedDoorFlow.RecordExitDoor(veh.Handle, doorIndex); } catch { }
                }
            }
            catch { }
            try
            {
                if (veh != null && veh.Exists())
                {
                    suspect.Task.ClearAll();
                    Function.Call(Hash.TASK_LEAVE_VEHICLE, suspect.Handle, veh.Handle, 256); // 256 = normal exit
                }
                else
                {
                    suspect.Task.WarpOutOfVehicle(veh);
                }
            }
            catch
            {
                try { suspect.Task.WarpOutOfVehicle(veh); } catch { }
            }
            if (IsCuffed(style))
            {
                try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
                try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
            }
            try
            {
                if (_hubRouter != null)
                {
                    var hub = _hubRouter.GetWriterHubFor(
                        suspect.Handle, SuspectState.ExitingVehicle);
                    if (hub != null && (hub.Is(SuspectState.InVehicle) || hub.Is(SuspectState.EnteringVehicle)))
                    {
                        hub.ChangeState(SuspectState.ExitingVehicle);
                        ModLog.Info($"[Escort] Secondary {suspect.Handle} -> ExitingVehicle (warp)");
                    }
                }
            }
            catch { }
        }

        // 让嫌疑人跟随玩家（调用 native 任务）
        private void MakeSuspectFollow(Ped suspect)
        {
            SuspectFollowOps.StartFollow(_suspectController, suspect, GetStyleFor(suspect.Handle));
            ;
        }

        private void StopSuspectFollow(Ped suspect)
        {
            SuspectFollowOps.StopFollow(_suspectController, suspect);
        }

        private bool IsVehicleBoardingContextNear(Ped suspect, Ped player, Vehicle vehicle, float maxDist)
        {
            try
            {
                if (vehicle == null || !vehicle.Exists() || suspect == null || !suspect.Exists() || player == null || !player.Exists()) return false;

                // 玩家在车门附近 + 嫌疑人在车辆附近即可触发上车，不再死卡玩家-嫌疑人点位。
                float playerToVeh = player.Position.DistanceTo(vehicle.Position);
                float suspectToVeh = suspect.Position.DistanceTo(vehicle.Position);
                if (playerToVeh <= 7.0f && suspectToVeh <= maxDist) return true;
            }
            catch { }
            return false;
        }

        private void NotifyVehicleInteractReject(string message, int debounceMs = 800)
        {
            int now = Game.GameTime;
            if ((now - _lastVehicleInteractRejectAtMs) < debounceMs) return;
            _lastVehicleInteractRejectAtMs = now;
            Notification.Show(message);
        }

        private SuspectRuntimeContext GetRuntimeContext(int handle)
        {
            try
            {
                if (_ctxRegistry == null || handle <= 0) return null;
                return _ctxRegistry.GetOrCreate(handle);
            }
            catch { }
            return null;
        }

        private void ResetBoardingReservation(int handle)
        {
            try
            {
                if (_ctxRegistry == null || handle <= 0) return;
                if (_ctxRegistry.TryGet(handle, out var ctx) && ctx != null)
                {
                    ctx.Busy = false;
                    ctx.ResetBoardingReservation();
                }
            }
            catch { }
        }

        private Vehicle ResolveBoardingVehicle(Ped player)
        {
            if (player == null || !player.Exists()) return null;
            try
            {
                if (player.IsInVehicle() && player.CurrentVehicle != null && player.CurrentVehicle.Exists())
                    return player.CurrentVehicle;
            }
            catch { }

            try
            {
                return World.GetNearbyVehicles(player, 10.0f)
                    .OrderBy(v => v.Position.DistanceTo(player.Position))
                    .FirstOrDefault(v => v != null && v.Exists());
            }
            catch { }

            return null;
        }

        private List<Ped> CollectBoardingParticipants(Ped primarySuspect)
        {
            var participants = new List<Ped>();
            if (primarySuspect == null || !primarySuspect.Exists() || primarySuspect.IsDead) return participants;
            participants.Add(primarySuspect);

            try
            {
                var mgr = EFCore.Instance?.GetCaseManager();
                var handles = mgr?.SuspectHandles;
                if (handles == null) return participants;

                foreach (var handle in handles)
                {
                    if (handle <= 0 || handle == primarySuspect.Handle) continue;
                    if (_suspectController == null || !_suspectController.IsHandleCompliant(handle)) continue;

                    var ped = FindPedByHandle(handle);
                    if (ped == null || !ped.Exists() || ped.IsDead || ped.IsInVehicle()) continue;
                    participants.Add(ped);
                }
            }
            catch { }

            return participants;
        }

        private List<VehicleSeat> GetBoardingSeatPool(Vehicle vehicle)
        {
            var seats = new List<VehicleSeat>();
            if (vehicle == null || !vehicle.Exists()) return seats;

            try { if (vehicle.IsSeatFree(VehicleSeat.RightRear)) seats.Add(VehicleSeat.RightRear); } catch { }
            try { if (vehicle.IsSeatFree(VehicleSeat.LeftRear)) seats.Add(VehicleSeat.LeftRear); } catch { }
            try { if (vehicle.IsSeatFree(VehicleSeat.Passenger)) seats.Add(VehicleSeat.Passenger); } catch { }

            return seats;
        }

        private VehicleSeat AssignBoardingSeat(List<VehicleSeat> freeSeats, int sequence)
        {
            if (freeSeats == null || freeSeats.Count == 0) return VehicleSeat.None;

            VehicleSeat[] preferredOrder =
                sequence <= 0
                    ? new[] { VehicleSeat.RightRear, VehicleSeat.LeftRear, VehicleSeat.Passenger }
                    : sequence == 1
                        ? new[] { VehicleSeat.LeftRear, VehicleSeat.RightRear, VehicleSeat.Passenger }
                        : new[] { VehicleSeat.Passenger, VehicleSeat.LeftRear, VehicleSeat.RightRear };

            foreach (var seat in preferredOrder)
            {
                if (!freeSeats.Contains(seat)) continue;
                freeSeats.Remove(seat);
                return seat;
            }

            return VehicleSeat.None;
        }

        private bool TryBeginCoordinatedBoarding(Ped primarySuspect, Ped player, Vehicle vehicle)
        {
            if (primarySuspect == null || !primarySuspect.Exists() || primarySuspect.IsDead) return false;
            if (player == null || !player.Exists()) return false;
            if (vehicle == null || !vehicle.Exists()) return false;

            var participants = CollectBoardingParticipants(primarySuspect);
            if (participants.Count <= 0) return false;

            var freeSeats = GetBoardingSeatPool(vehicle);
            if (freeSeats.Count < participants.Count)
            {
                ModLog.Warn($"[Escort][Vehicle] Boarding aborted: vehicle={vehicle.Handle}, freeSeats={freeSeats.Count}, suspects={participants.Count}");
                Notification.Show("~y~车辆座位不足，无法同时押送所有嫌疑人");
                return false;
            }

            int now = Game.GameTime;
            for (int i = 0; i < participants.Count; i++)
            {
                var ped = participants[i];
                var seat = AssignBoardingSeat(freeSeats, i);
                if (seat == VehicleSeat.None)
                {
                    foreach (var item in participants)
                    {
                        ResetBoardingReservation(item.Handle);
                    }

                    ModLog.Warn($"[Escort][Vehicle] Boarding seat assignment failed: vehicle={vehicle.Handle}, suspect={ped.Handle}");
                    Notification.Show("~y~车辆座位分配失败，请调整车辆位置后重试");
                    return false;
                }

                var ctx = GetRuntimeContext(ped.Handle);
                if (ctx == null) continue;

                ctx.ReservedVehicleHandle = vehicle.Handle;
                ctx.ReservedSeat = seat;
                ctx.ReservedDoorIndex = NormalizeDoorIndex(vehicle, GetDoorIndexForSeat(seat));
                ctx.BoardingSequence = i;
                ctx.BoardingAttemptCount = 0;
                ctx.LastCommandAtMs = 0;
                ctx.NextBoardingRetryAtMs = now + (i * BOARDING_STAGGER_MS);
                ctx.Busy = true;

                ModLog.Info($"[Escort][Vehicle] Boarding plan reserved: suspect={ped.Handle}, vehicle={vehicle.Handle}, seat={seat}, door={ctx.ReservedDoorIndex}, sequence={i}");
            }

            foreach (var ped in participants)
            {
                try
                {
                    var hub = _hubRouter != null
                        ? _hubRouter.GetWriterHubFor(ped.Handle, SuspectState.EnteringVehicle)
                        : GetActiveHubFor(ped.Handle);
                    if (hub != null && !hub.Is(SuspectState.EnteringVehicle))
                        hub.ChangeState(SuspectState.EnteringVehicle);
                }
                catch (Exception ex)
                {
                    ModLog.Error($"[Escort][Vehicle] Boarding state dispatch failed: suspect={ped.Handle}, error={ex}");
                }
            }

            _enteringVehicleStartMs = 0;
            ModLog.Info($"[Escort][Vehicle] Coordinated boarding armed: vehicle={vehicle.Handle}, suspects={participants.Count}");
            return true;
        }

        private Vector3 GetBoardingApproachPosition(Vehicle vehicle, int doorIndex, int sequence)
        {
            if (vehicle == null || !vehicle.Exists()) return Vector3.Zero;

            float offsetX = 1.25f;
            float offsetY = -1.75f;

            if (doorIndex == 1)
            {
                offsetX = -1.25f;
                offsetY = -1.85f;
            }
            else if (doorIndex == 3)
            {
                offsetX = 1.25f;
                offsetY = -1.85f;
            }
            else if (doorIndex == 2)
            {
                offsetX = 1.15f;
                offsetY = 0.1f;
            }

            offsetY -= Math.Min(sequence, 2) * 0.55f;

            try
            {
                return Function.Call<Vector3>(
                    Hash.GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS,
                    vehicle.Handle,
                    offsetX,
                    offsetY,
                    0.0f);
            }
            catch { }

            return vehicle.Position;
        }

        private bool IsBoardingPathBlocked(int handle, Ped suspect, int vehicleHandle)
        {
            if (suspect == null || !suspect.Exists()) return false;
            if (vehicleHandle <= 0) return false;

            try
            {
                var mgr = EFCore.Instance?.GetCaseManager();
                var handles = mgr?.SuspectHandles;
                if (handles == null) return false;

                foreach (var otherHandle in handles)
                {
                    if (otherHandle <= 0 || otherHandle == handle) continue;
                    if (_ctxRegistry == null || !_ctxRegistry.TryGet(otherHandle, out var otherCtx) || otherCtx == null) continue;
                    if (otherCtx.ReservedVehicleHandle != vehicleHandle) continue;

                    var otherPed = FindPedByHandle(otherHandle);
                    if (otherPed == null || !otherPed.Exists() || otherPed.IsDead || otherPed.IsInVehicle()) continue;
                    if (otherPed.Position.DistanceTo(suspect.Position) <= BOARDING_BLOCK_RADIUS)
                        return true;
                }
            }
            catch { }

            return false;
        }

        private bool TryHandleBoardingApproach(int handle, Ped suspect, SuspectRuntimeContext ctx, Vehicle vehicle, int nowMs)
        {
            if (ctx == null || suspect == null || !suspect.Exists() || vehicle == null || !vehicle.Exists()) return false;

            bool blocked = IsBoardingPathBlocked(handle, suspect, ctx.ReservedVehicleHandle);
            if (!blocked && ctx.BoardingSequence <= 0) return false;

            var approach = GetBoardingApproachPosition(vehicle, ctx.ReservedDoorIndex, ctx.BoardingSequence);
            if (approach == Vector3.Zero) return false;
            if (suspect.Position.DistanceTo(approach) <= 0.9f) return false;

            try { suspect.Task.ClearAll(); } catch { }
            try
            {
                Function.Call(
                    Hash.TASK_GO_TO_COORD_ANY_MEANS,
                    suspect.Handle,
                    approach.X,
                    approach.Y,
                    approach.Z,
                    1.1f,
                    0,
                    false,
                    786603,
                    0.0f);
            }
            catch { }

            ctx.LastCommandAtMs = 0;
            ctx.NextBoardingRetryAtMs = nowMs + BOARDING_APPROACH_DELAY_MS;
            ModLog.Info($"[Escort][Vehicle] Boarding approach adjust: suspect={handle}, vehicle={vehicle.Handle}, door={ctx.ReservedDoorIndex}, blocked={blocked}, sequence={ctx.BoardingSequence}");
            return true;
        }

        private Vehicle ResolveReservedBoardingVehicle(SuspectRuntimeContext ctx, Ped suspect, Ped player)
        {
            Vehicle vehicle = null;
            try
            {
                if (ctx != null && ctx.ReservedVehicleHandle > 0)
                    vehicle = FindVehicleByHandle(ctx.ReservedVehicleHandle);
            }
            catch { vehicle = null; }

            if (vehicle != null && vehicle.Exists()) return vehicle;
            if (player != null && player.Exists()) return ResolveBoardingVehicle(player);

            try
            {
                return suspect != null && suspect.Exists()
                    ? World.GetNearbyVehicles(suspect, 10.0f).FirstOrDefault(v => v != null && v.Exists())
                    : null;
            }
            catch { }

            return null;
        }

        private VehicleSeat ResolveReservedSeat(int handle, Vehicle vehicle, SuspectRuntimeContext ctx)
        {
            if (vehicle == null || !vehicle.Exists()) return VehicleSeat.None;

            if (ctx != null && ctx.ReservedSeat != VehicleSeat.None)
            {
                try
                {
                    if (vehicle.IsSeatFree(ctx.ReservedSeat))
                        return ctx.ReservedSeat;
                }
                catch { }

                ModLog.Warn($"[Escort][Vehicle] Reserved seat unavailable: suspect={handle}, vehicle={vehicle.Handle}, seat={ctx.ReservedSeat}");
                return VehicleSeat.None;
            }

            return FindAvailableSeatForSuspect(vehicle);
        }

        private bool TryForceBoardIntoVehicle(int handle, Ped suspect, ArrestActionStyle style)
        {
            var ctx = GetRuntimeContext(handle);
            var player = Game.Player.Character;
            var vehicle = ResolveReservedBoardingVehicle(ctx, suspect, player);
            if (vehicle == null || !vehicle.Exists()) return false;

            var seat = ResolveReservedSeat(handle, vehicle, ctx);
            if (seat == VehicleSeat.None) return false;

            try
            {
                suspect.SetIntoVehicle(vehicle, seat);
                if (IsCuffed(style)) RestoreCuffConstraints(suspect);
                ModLog.Warn($"[Escort][Vehicle] Boarding force-seat applied: suspect={handle}, vehicle={vehicle.Handle}, seat={seat}");
                return true;
            }
            catch (Exception ex)
            {
                ModLog.Error($"[Escort][Vehicle] Boarding force-seat failed: suspect={handle}, error={ex}");
            }

            return false;
        }

        private bool TryHandleBoardingRecovery(int handle, Ped suspect, ArrestActionStyle style, int nowMs)
        {
            var ctx = GetRuntimeContext(handle);
            if (ctx == null || suspect == null || !suspect.Exists()) return false;

            if (ctx.LastCommandAtMs <= 0)
            {
                if (ctx.NextBoardingRetryAtMs > 0 && nowMs >= ctx.NextBoardingRetryAtMs)
                {
                    StartEnterVehicle(handle);
                    return true;
                }

                return false;
            }

            if (nowMs - ctx.LastCommandAtMs <= ENTERING_VEHICLE_TIMEOUT_MS)
                return false;

            if (ctx.BoardingAttemptCount < MAX_BOARDING_RETRIES)
            {
                ctx.LastCommandAtMs = 0;
                ctx.NextBoardingRetryAtMs = nowMs + BOARDING_RETRY_DELAY_MS;
                ModLog.Warn($"[Escort][Vehicle] Boarding retry scheduled: suspect={handle}, attempt={ctx.BoardingAttemptCount}, vehicle={ctx.ReservedVehicleHandle}, seat={ctx.ReservedSeat}");
                try { suspect.Task.ClearAll(); } catch { }
                if (IsCuffed(style)) RestoreCuffConstraints(suspect);
                return true;
            }

            if (TryForceBoardIntoVehicle(handle, suspect, style))
            {
                try
                {
                    var hub = _hubRouter != null
                        ? _hubRouter.GetWriterHubFor(handle, SuspectState.InVehicle)
                        : GetActiveHubFor(handle);
                    hub?.ChangeState(SuspectState.InVehicle);
                }
                catch { }

                return true;
            }

            ModLog.Warn($"[Escort][Vehicle] Boarding failed after retries: suspect={handle}, vehicle={ctx.ReservedVehicleHandle}, seat={ctx.ReservedSeat}");
            ResetBoardingReservation(handle);
            if (IsCuffed(style)) RestoreCuffConstraints(suspect);
            try { suspect.Task.ClearAll(); } catch { }
            try { _suspectController.ClearBusy(suspect); } catch { }

            try
            {
                var hub = _hubRouter != null
                    ? _hubRouter.GetWriterHubFor(handle, SuspectState.Escorting)
                    : GetActiveHubFor(handle);
                hub?.ChangeState(SuspectState.Escorting);
            }
            catch { }

            return true;
        }

        private void TryIssuePendingBoardingCommands()
        {
            try
            {
                var mgr = EFCore.Instance?.GetCaseManager();
                var handles = mgr?.SuspectHandles;
                if (handles == null) return;

                int now = Game.GameTime;
                foreach (var handle in handles)
                {
                    if (handle <= 0) continue;
                    if (_ctxRegistry == null || !_ctxRegistry.TryGet(handle, out var ctx) || ctx == null) continue;
                    if (ctx.ReservedVehicleHandle <= 0) continue;

                    var hub = _hubRouter != null ? _hubRouter.GetHubFor(handle) : GetActiveHubFor(handle);
                    if (hub == null || !hub.Is(SuspectState.EnteringVehicle)) continue;

                    var ped = FindPedByHandle(handle);
                    if (ped == null || !ped.Exists() || ped.IsDead || ped.IsInVehicle()) continue;
                    if (ctx.LastCommandAtMs > 0) continue;
                    if (ctx.NextBoardingRetryAtMs > now) continue;

                    StartEnterVehicle(handle);
                }
            }
            catch { }
        }



        /// <summary>
        /// 玩家与车辆交互（只发意图，不执行行为）
        /// </summary>
        private void OnVehicleInteract(EscortVehicleInteractEvent e)
        {
            var suspect = _suspectController.GetCurrentSuspect();
            var player = Game.Player.Character;

            try { suspect = TryResolveInteractSuspect(suspect, player); } catch { }

            // 基本有效性检查
            if (suspect == null || !suspect.Exists())
            {
                ModLog.Warn("[Escort][Vehicle] E pressed but no current suspect");
                Notification.Show("~y~当前无可交互嫌疑人（请靠近嫌疑人(1)/(2)）");
                return;
            }

            if (suspect.IsDead || (suspect.IsRagdoll && !IsState(SuspectState.InVehicle)))
            {
                ModLog.Warn("[Escort][Vehicle] E pressed but suspect not controllable");
                return;
            }

            // 防止在中间态重复触发（去抖）
            if (IsState(SuspectState.EnteringVehicle) || IsState(SuspectState.ExitingVehicle))
            {
                ModLog.Info("[Escort][Vehicle] E pressed but suspect is transitioning");
                return;
            }

            // 先验证 player 是否有效（避免直接访问 player 时发生异常）
            if (player == null || !player.Exists())
            {
                ModLog.Warn("[Escort][Vehicle] E pressed but player invalid");
                return;
            }

            // E 门禁：按线路分流（被拷线 vs 抱头线）
            bool pullOverBypass = false;
            ArrestActionStyle interactStyle = GetStyleFor(suspect.Handle);
            try { pullOverBypass = _pullOverBypass.IsActive(interactStyle, suspect.Handle, Game.GameTime); } catch { pullOverBypass = false; }

            if (!VehicleEscortInteractGate.EnsureAllowed(interactStyle, suspect, pullOverBypass))
                return;

            // 规则：E 只负责"上下车"；前置必须先按 G 进入押送。
            // Restrained 状态下按 E 只提示，不做任何自动跟随/自动押送。
            if (IsState(SuspectState.Restrained))
            {
                Notification.Show("~y~请先按 G 让嫌疑人跟随，再按 E 上下车");
                return;
            }


            // 上车：Escorting（步行押送）
            if (IsState(SuspectState.Escorting))
            {
                bool requireFollow = _requireFollowBeforeBoard;
                try
                {
                    if (GetStyle() == ArrestActionStyle.HandsOnHeadFollow)
                        requireFollow = false;
                }
                catch { }
                if (requireFollow)
                {
                    // E 前置 G：如果没按 G（跟随未开启），直接拒绝（不再由 E 自动补跟随）。
                    if (!IsSuspectFollowing(suspect.Handle))
                    {
                        Notification.Show("~y~请先按 G 让嫌疑人跟随，再按 E 上车");
                        return;
                    }
                }




                // 上拷牵走：允许玩家下车状态下把嫌疑人塞进附近车辆后座（更符合"警察开门塞人"体验）
                try
                {
                    if (GetStyle() == ArrestActionStyle.CuffAndLead && !player.IsInVehicle())
                    {
                        Vehicle nearVeh = null;
                        try { nearVeh = World.GetNearbyVehicles(player, 6.0f).FirstOrDefault(v => v != null && v.Exists()); } catch { nearVeh = null; }
                        if (nearVeh == null || !nearVeh.Exists())
                        {
                            Notification.Show("~y~附近没有车辆");
                            return;
                        }

                        // 近距触发保障
                        try
                        {
                            if (!IsPlayerNearSuspectInteractionPoint(suspect, player, VehicleEscortLine.PLAYER_SUSPECT_E_INTERACT_DISTANCE))
                            {

                                Notification.Show("~y~离嫌疑人太远");
                                return;
                            }
                        }
                        catch { }

                        if (!TryBeginCoordinatedBoarding(suspect, player, nearVeh))
                            return;

                        ModLog.Info("[Escort][Vehicle] CuffAndLead on-foot E -> coordinated boarding issued");
                        return;
                    }
                }
                catch { }

                // 其它情况：保持旧逻辑（要求玩家在车内）
                if (!player.IsInVehicle())
                {
                    ModLog.Info("[Escort][Vehicle] E pressed but player not in vehicle");
                    Notification.Show("~y~请在车内按 E，或靠近车辆按 E 塞入后座");
                    return;
                }

                var vehicle = player.CurrentVehicle;
                if (vehicle == null || !vehicle.Exists())
                {
                    ModLog.Info("[Escort][Vehicle] E pressed but no valid player vehicle");
                    return;
                }

                // 近距触发保障（避免远处误触）
                try
                {
                    if (!IsPlayerNearSuspectInteractionPoint(suspect, player, VehicleEscortLine.PLAYER_SUSPECT_E_INTERACT_DISTANCE))
                    {
                        ModLog.Info("[Escort][Vehicle] E pressed but suspect too far");
;
                        Notification.Show("~y~嫌疑人距离过远，请先按 G 保持跟随并靠近后再按 E");
                        return;
                    }
                }
                catch (Exception exPos)
                {
                    ModLog.Error("[Escort][Vehicle] Distance check failed: " + exPos);
                    return;
                }

                if (!TryBeginCoordinatedBoarding(suspect, player, vehicle))
                    return;

                ModLog.Info("[Escort][Vehicle] E pressed -> coordinated boarding issued");

                return;
            }

            // 下车：仅在 InVehicle 时触发
            if (IsState(SuspectState.InVehicle))
            {
                // 同样做近距与可控性保障
                if (suspect.Position.DistanceTo(player.Position) > 6.0f)
                {
                    ModLog.Info("[Escort][Vehicle] E pressed but suspect too far to exit safely");
                    return;
                }
                // 同上：只切状态，由 OnStateChanged 统一执行 StartExitVehicle（避免重复下任务）
                ChangeState(SuspectState.ExitingVehicle);
                try { ApplyActionToOtherCompliantCaseSuspects(
                    suspect.Handle, p => TryMakeSecondaryExit(p)); } catch { }
                ModLog.Info("[Escort][Vehicle] E pressed -> ExitingVehicle issued (all suspects)");
                return;
            }

            // 其他状态不处理
            ModLog.Info("[Escort][Vehicle] E pressed but no action for current suspect state");
            Notification.Show("~y~当前状态不可上车：请先完成拘捕并进入押送");
        }
        /// <summary>
        /// 处理玩家按 G 的意图（跟随 / 下车）
        /// 事件来源：InputManager 发布 SuspectFollowRequestEvent
        /// 语义：第一次按 G -> 让当前嫌疑人跟随；再次按 G -> 取消跟随并请求其下车（若在车内）
        /// </summary>
        private void OnFollowRequest(SuspectFollowRequestEvent e)
        {
            try
            {
                var suspect = _suspectController.GetCurrentSuspect();
                if (suspect == null || !suspect.Exists())
                {
                    ModLog.Warn("[Escort][Follow] Follow requested but no current suspect");
                    Notification.Show("当前无可操作的嫌疑人");
                    return;
                }

                // 上车/下车过渡态：忽略 G（否则会把 EnteringVehicle 流程打断，日志里反复出现 transition）
                try
                {
                    if (IsState(SuspectState.EnteringVehicle) || IsState(SuspectState.ExitingVehicle))
                    {
                        Notification.Show("~y~嫌疑人正在上下车，请稍等");
                        return;
                    }
                }
                catch { }

                // 严格区分：G 键仅负责跟随/停止跟随，不负责下车。下车由 E 键负责。
                if (suspect.IsInVehicle())
                {
                    Notification.Show("~y~嫌疑人已在车内，请按 E 键让其下车");
                    return;
                }

                // 如果不在车内：切换跟随/取消跟随
                if (!IsSuspectFollowing(suspect.Handle))
                {
                    // 关闭"受惊逃跑"随机触发：按 G 只负责进入跟随，避免流程被随机打断。

                    SetSuspectFollowing(suspect.Handle, true);

                    // 被拷线：按 G 的瞬间强制维持被拷态（双手在后）
                    try
                    {
                        if (GetStyleFor(suspect.Handle) == ArrestActionStyle.CuffAndLead)
                        {
                            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }
                            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, true); } catch { }
                            try { EnsureCuffedClipset(suspect); } catch { }
                            try { EnsureCuffedUpperBodyPose(suspect); } catch { }
                        }
                    }
                    catch { }

                    // 真正下达"跟随"任务（之前只改了标记，容易出现你说的"按 G 没反应"）
                    try { MakeSuspectFollow(suspect); } catch { }
                    try { ApplyActionToOtherCompliantCaseSuspects(suspect.Handle, p => TryMakeSecondaryFollow(p)); } catch { }

                    ModLog.Info("[Escort][Follow] Suspect set to follow (handle=" + suspect.Handle + ")");
                    Notification.Show("已下达跟随：所有已拘捕嫌疑人将跟随");
                }
                else
                {
                    // 已经在跟随 -> 取消跟随
                    SetSuspectFollowing(suspect.Handle, false);

                    try { StopSuspectFollow(suspect); } catch { }

                    ModLog.Info("[Escort][Follow] Suspect follow cancelled (handle=" + suspect.Handle + ")");
                    Notification.Show("已取消跟随");
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[Escort][Follow] OnFollowRequest error: " + ex);
            }
        }

        private void OnPullOverBypassActivated(PullOverEscortBypassActivatedEvent e)
        {
            try
            {
                if (e == null) return;
                if (e.SuspectHandle <= 0) return;

                _pullOverBypass.Activate(e.SuspectHandle, Game.GameTime);
                ModLog.Info($"[Escort][Vehicle] PullOver bypass activated: suspect={e.SuspectHandle} ttlMs={PULLOVER_BYPASS_TTL_MS}");
            }
            catch (Exception ex)
            {
                ModLog.Error("[Escort][Vehicle] OnPullOverBypassActivated error: " + ex);
            }
        }

        private void OnCaseEnded(CaseEndedEvent e)
        {
            try
            {
                SetSuspectFollowing(-1, false);
                try
                {
                    if (_ctxRegistry != null)
                        _ctxRegistry.Clear();
                }
                catch { }
                ModLog.Info("[Escort][Vehicle] Case ended - reset follow state");
            }
            catch (Exception ex)
            {
                ModLog.Error("[Escort][Vehicle] OnCaseEnded error: " + ex);
            }
        }

        /// <summary>
        /// 在嫌疑人真正离开车辆（ExitingVehicle -> Escorting）后调用：
        /// - 清理上车去重记录（允许下次再次发布 boarded event）
        /// - 保留/恢复跟随状态（根据业务）
        /// </summary>
        private void OnSuspectExitVehicle()
        {
            try
            {
                var suspect = _suspectController.GetCurrentSuspect();
                int handle = suspect != null && suspect.Exists() ? suspect.Handle : -1;

                // 清理上车去重状态，这样下一次上车会再次发布 boarded event
                _lastBoardedSuspectHandle = -1;
                _lastBoardedAtMs = 0;
                ResetBoardingReservation(handle);

                ModLog.Info("[Escort][Vehicle] Suspect exited vehicle - cleared boarded record (handle=" + handle + ")");
                try { _suspectController.ClearBusy(suspect); } catch (Exception ex) { ModLog.Error("[Escort][Vehicle] UnmarkBusy after OnSuspectExitVehicle failed: " + ex); }

            }
            catch (Exception ex)
            {
                ModLog.Error("[Escort][Vehicle] OnSuspectExitVehicle error: " + ex);
            }
        }


        private void OnSuspectStateChangedForHandle(int handle, SuspectState oldState, SuspectState newState)
        {
            if (_handlingStateChange)
            {
                ModLog.Warn($"[Escort][Vehicle] 阻止了重入式状态变更: {oldState}->{newState} (handle={handle})");
                return;
            }

            _handlingStateChange = true;
            try
            {
                if (handle <= 0)
                {
                    ModLog.Warn("[Escort][Vehicle] OnSuspectStateChangedForHandle: No valid suspect handle");
                    return;
                }

                ModLog.Info($"[Escort][Vehicle] StateChanged: {oldState} -> {newState} (handle={handle})");

                switch (newState)
                {
                    case SuspectState.EnteringVehicle:
                        StartEnterVehicle(handle);
                        break;

                    case SuspectState.InVehicle:
                        OnEnteredVehicle(handle);
                        break;

                    case SuspectState.ExitingVehicle:
                        StartExitVehicle(handle);
                        break;

                    case SuspectState.Escorting:
                        ResumeEscortOnFoot(handle);
                        break;
                }

                if (newState == SuspectState.Escorting)
                {
                    try
                    {
                        var suspect = FindPedByHandle(handle);
                        if (suspect != null && suspect.Exists() && suspect.IsInVehicle())
                        {
                            ModLog.Info("[Escort][Vehicle] Detected Escorting but suspect is in vehicle -> invoking OnEnteredVehicle");
                            OnEnteredVehicle(handle);
                        }
                    }
                    catch (Exception ex)
                    {
                        ModLog.Error("[Escort][Vehicle] Error in Escorting->InVehicle compatibility check: " + ex);
                    }
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[Escort][Vehicle] OnSuspectStateChangedForHandle error: " + ex);
            }
            finally
            {
                _handlingStateChange = false;
            }
        }

        private void OnSuspectStateChanged(
            SuspectState oldState,
            SuspectState newState
        )
        {
            int handle = -1;
            try
            {
                var suspect = _suspectController?.GetCurrentSuspect();
                handle = (suspect != null && suspect.Exists()) ? suspect.Handle : GetActiveHub().SuspectHandle;
            }
            catch { handle = GetActiveHub().SuspectHandle; }

            OnSuspectStateChangedForHandle(handle, oldState, newState);
        }

        private int GetRearDoorIndexForSuspect(Vehicle vehicle, Ped suspect)
        {
            if (vehicle == null || !vehicle.Exists()) return 2;
            if (suspect == null || !suspect.Exists()) return 2;

            // seat index：-1 driver, 0 front passenger, 1 left rear, 2 right rear（常见约定）
            try
            {
                int leftRear = Function.Call<int>(Hash.GET_PED_IN_VEHICLE_SEAT, vehicle.Handle, 1);
                if (leftRear == suspect.Handle) return NormalizeDoorIndex(vehicle, 1); // 后左门
            }
            catch { }

            try
            {
                int rightRear = Function.Call<int>(Hash.GET_PED_IN_VEHICLE_SEAT, vehicle.Handle, 2);
                if (rightRear == suspect.Handle) return NormalizeDoorIndex(vehicle, 3); // 后右门
            }
            catch { }

            try
            {
                int frontPassenger = Function.Call<int>(Hash.GET_PED_IN_VEHICLE_SEAT, vehicle.Handle, 0);
                if (frontPassenger == suspect.Handle) return NormalizeDoorIndex(vehicle, 2); // 副驾门
            }
            catch { }

            return NormalizeDoorIndex(vehicle, 2);
        }


        // P0-3 Fix: Accept handle parameter instead of using GetCurrentSuspect()
        private void StartExitVehicle(int handle)
        {
            var suspect = FindPedByHandle(handle);
            StartCuffedExitVehicle(suspect, GetStyleFor(handle));
        }





        // =========================
        // 行为执行（当前阶段：空壳）
        // 下一刀会把你原本"变淡"的 Task 代码塞进        // =========================

        // P0-3 Fix: Accept handle parameter instead of using GetCurrentSuspect()
        private void StartEnterVehicle(int handle)
        {
            var suspect = FindPedByHandle(handle);
            var player = Game.Player.Character;
            if (suspect == null || !suspect.Exists()) return;

            if (suspect.IsInVehicle())
            {
                ModLog.Info($"[Escort][Vehicle] Suspect {handle} already in vehicle, skipping StartEnterVehicle");
                return;
            }

            if (player == null || !player.Exists())
            {
                ModLog.Warn("[Escort][Vehicle] StartEnterVehicle aborted: player invalid");
                _enteringVehicleStartMs = Game.GameTime - ENTERING_VEHICLE_TIMEOUT_MS - 1;
                return;
            }

            int now = Game.GameTime;
            var ctx = GetRuntimeContext(handle);
            if (ctx != null && ctx.NextBoardingRetryAtMs > now)
            {
                ModLog.Info($"[Escort][Vehicle] StartEnterVehicle waiting: suspect={handle}, retryAt={ctx.NextBoardingRetryAtMs}, now={now}");
                return;
            }

            Vehicle vehicle = ResolveReservedBoardingVehicle(ctx, suspect, player);

            if (vehicle == null || !vehicle.Exists())
            {
                ModLog.Warn($"[Escort][Vehicle] StartEnterVehicle: no vehicle found for suspect={handle}");
                _enteringVehicleStartMs = Game.GameTime - ENTERING_VEHICLE_TIMEOUT_MS - 1;
                return;
            }

            if (TryHandleBoardingApproach(handle, suspect, ctx, vehicle, now))
                return;

            var seat = ResolveReservedSeat(handle, vehicle, ctx);
            if (seat == VehicleSeat.None)
            {
                ModLog.Warn($"[Escort][Vehicle] StartEnterVehicle: no available seat for suspect={handle}, vehicle={vehicle.Handle}");
                _enteringVehicleStartMs = Game.GameTime - ENTERING_VEHICLE_TIMEOUT_MS - 1;
                return;
            }

            var style = GetStyleFor(handle);

            try
            {
                if (ShouldAutoDoors(style))
                {
                    int doorIndex = ctx != null && ctx.ReservedDoorIndex >= 0
                        ? NormalizeDoorIndex(vehicle, ctx.ReservedDoorIndex)
                        : NormalizeDoorIndex(vehicle, GetDoorIndexForSeat(seat));
                    try { VehicleDoorOps.OpenDoor(vehicle, doorIndex); } catch { }
                    try { _cuffedDoorFlow.ArmPendingShutDoor(vehicle.Handle, doorIndex, handle, Game.GameTime); } catch { }
                }
            }
            catch { }

            try { suspect.Task.ClearAll(); } catch { }

            if (IsCuffed(style))
            {
                SuspendCuffConstraints(suspect);
                ModLog.Info($"[Escort][Vehicle] Cuff constraints suspended for vehicle entry (handle={handle})");
            }

            try { suspect.Task.EnterVehicle(vehicle, seat); } catch { }

            if (ctx != null)
            {
                ctx.ReservedVehicleHandle = vehicle.Handle;
                ctx.ReservedSeat = seat;
                ctx.ReservedDoorIndex = NormalizeDoorIndex(vehicle, GetDoorIndexForSeat(seat));
                ctx.BoardingAttemptCount++;
                ctx.LastCommandAtMs = now;
                ctx.NextBoardingRetryAtMs = 0;
                ctx.Busy = true;
            }

            _enteringVehicleStartMs = Game.GameTime;

            ModLog.Info($"[Escort][Vehicle] Boarding command issued: suspect={handle}, vehicle={vehicle.Handle}, seat={seat}, attempt={(ctx != null ? ctx.BoardingAttemptCount : 1)}");
        }


        // 兼容遗留调用点：之前有部分流程会调用 _cuffedEscortFlow 的方法；
        // 现在统一走本类的合并实现。
        private void StartEnterVehicleLegacy() => StartEnterVehicle(GetActiveHub().SuspectHandle);





        // 替换 OnEnteredVehicle() 或相应检测上车处
        // P0-3 Fix: Accept handle parameter instead of using GetCurrentSuspect()
        private void OnEnteredVehicle(int handle)
        {
            var suspect = FindPedByHandle(handle);
            int now = Game.GameTime;
            OnCuffedEnteredVehicle(suspect, GetStyleFor(handle), now);
        }




        /// <summary>
        /// 每帧检测"上车/下车"过渡态是否完成。
        /// 只在 StateChanged 里检测会漏掉：任务完成发生在后续帧，但没有新的状态事件。
        /// </summary>
        public void TickUpdate()
        {
            try
            {
                var suspect = _suspectController.GetCurrentSuspect();
                var player = Game.Player.Character;
                var style = GetStyle();

                // 兜底：嫌疑人被上拷后偶发变"非实体可穿模"，这里每帧强制恢复（仅在已控制时）
                try
                {
                    if (_suspectController != null && _suspectController.IsCompliant)
                        EnsureSuspectIsSolid(suspect);
                }
                catch { }
                try { TryIssuePendingBoardingCommands(); } catch { }
                if (TickCuffedVehicleEscort(suspect, player, style, Game.GameTime))
                    return;

                // 下车后关门（兜底，避免 door pending 残留）
                try
                {
                    if (IsState(SuspectState.Escorting))
                    {
                        _cuffedDoorFlow.TryShutDoorAfterExit(
                            style,
                            (h) => FindVehicleByHandle(h),
                            (v, d) => NormalizeDoorIndex(v, d)
                        );
                    }
                }
                catch { }

                // NEW: Tick secondary suspects' vehicle transitions
                try { TickSecondarySuspectVehicleTransitions(); } catch { }
            }
            catch (Exception ex)

            {
                ModLog.Error("[Escort][Vehicle] TickUpdate error: " + ex);
            }
        }

        private void TickSecondarySuspectVehicleTransitions()
        {
            try
            {
                var cm = EFCore.Instance?.GetCaseManager();
                var handles = cm?.SuspectHandles;
                if (handles == null || handles.Count < 2) return;

                var currentSuspect = _suspectController?.GetCurrentSuspect();
                int currentHandle = (currentSuspect != null && currentSuspect.Exists()) 
                    ? currentSuspect.Handle : -1;
                int nowMs = Game.GameTime;
                SuspectState primaryState = SuspectState.None;
                try
                {
                    if (currentHandle > 0 && _hubRouter != null)
                    {
                        var primaryHub = _hubRouter.GetHubFor(currentHandle);
                        if (primaryHub != null) primaryState = primaryHub.CurrentState;
                    }
                }
                catch { }

                foreach (var h in handles)
                {
                    if (h <= 0 || h == currentHandle) continue;

                    try
                    {
                        var hub = _hubRouter?.GetHubFor(h);
                        if (hub == null) continue;

                        var ped = FindPedByHandle(h);
                        if (ped == null || !ped.Exists() || ped.IsDead) continue;

                        var style = GetStyleFor(h);
                        var secondaryState = hub.CurrentState;
                        if (IsCuffed(style) && (secondaryState == SuspectState.EnteringVehicle
                            || secondaryState == SuspectState.InVehicle
                            || secondaryState == SuspectState.ExitingVehicle))
                        {
                            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, ped.Handle, true); } catch { }
                            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, ped.Handle, true); } catch { }
                        }

                        if (secondaryState == SuspectState.EnteringVehicle && ped.IsInVehicle())
                        {
                            hub.ChangeState(SuspectState.InVehicle);
                            CloseAllVehicleDoors(ped.CurrentVehicle, style);
                            ModLog.Info($"[Escort] Secondary {h}: EnteringVehicle -> InVehicle");
                        }

                        if (secondaryState == SuspectState.ExitingVehicle && !ped.IsInVehicle())
                        {
                            hub.ChangeState(SuspectState.Escorting);

                            if (IsCuffed(style))
                            {
                                try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, ped.Handle, true); } catch { }
                                try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, ped.Handle, true); } catch { }
                                try { EnsureCuffedClipset(ped); } catch { }
                                try { EnsureCuffedUpperBodyPose(ped); } catch { }
                            }
                            try
                            {
                                Vehicle nearVeh = null;
                                try { nearVeh = World.GetNearbyVehicles(ped, 8.0f)
                                    .FirstOrDefault(v => v != null && v.Exists()); } catch { }
                                CloseAllVehicleDoors(nearVeh, style);
                            }
                            catch { }

                            ModLog.Info($"[Escort] Secondary {h}: ExitingVehicle -> Escorting");
                        }
                        if (secondaryState == SuspectState.InVehicle
                            && ped.IsInVehicle()
                            && (primaryState == SuspectState.Escorting
                                || primaryState == SuspectState.ExitingVehicle
                                || primaryState == SuspectState.Restrained
                                || primaryState == SuspectState.None))
                        {
                            ModLog.Info($"[Escort] Secondary {h}: forced exit (primary state={primaryState})");
                            TryMakeSecondaryExit(ped);
                        }
                        if (secondaryState == SuspectState.EnteringVehicle && !ped.IsInVehicle())
                        {
                            try { TryHandleBoardingRecovery(h, ped, style, nowMs); } catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void CloseAllVehicleDoors(Vehicle veh, ArrestActionStyle style)
        {
            if (veh == null || !veh.Exists()) return;
            if (!ShouldAutoDoors(style)) return;
            try
            {
                for (int di = 0; di <= 3; di++)
                {
                    try
                    {
                        float angle = Function.Call<float>(
                            Hash.GET_VEHICLE_DOOR_ANGLE_RATIO, veh.Handle, di);
                        if (angle > 0.1f)
                        {
                            VehicleDoorOps.ShutDoor(veh, di);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        // 兼容遗留调用点：之前有部分流程会调用 _cuffedEscortFlow 的方法；
        // 现在统一走本类的合并实现。
   
        // P0-3 Fix: Accept handle parameter instead of using GetCurrentSuspect()
        private void ResumeEscortOnFoot(int handle)
        {
            try
            {
                var suspect = FindPedByHandle(handle);
                if (suspect == null || !suspect.Exists() || suspect.IsDead) return;
                var player = Game.Player.Character;
                if (player == null || !player.Exists()) return;

                var style = GetStyleFor(handle);
                try { SuspectFollowOps.StartFollow(_suspectController, suspect, style); } catch { }
                ModLog.Info($"[Escort][Vehicle] ResumeEscortOnFoot: follow reissued for handle={handle}");
            }
            catch (Exception ex)
            {
                ModLog.Error("[Escort][Vehicle] ResumeEscortOnFoot failed: " + ex);
            }
        }

        private Ped TryResolveInteractSuspect(Ped suspect, Ped player)
        {
            if (suspect != null && suspect.Exists()) return suspect;
            var target = _suspectController.GetCurrentSuspect();
            if (target != null && target.Exists()) return target;
            return null;
        }

        private bool IsPlayerNearSuspectInteractionPoint(Ped suspect, Ped player, float threshold)
        {
            if (suspect == null || !suspect.Exists() || player == null || !player.Exists())
                return false;
            var suspectPos = suspect.Position;
            var playerPos = player.Position;
            // 放宽：E 上下车不应要求"贴身 1m"，否则玩家体验很差；这里统一按阈值判断。
            return suspectPos.DistanceTo(playerPos) <= threshold;
        }

    }
}
