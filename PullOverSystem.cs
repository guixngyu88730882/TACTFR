using EF.PoliceMod.Core;
using EF.PoliceMod.Gameplay;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Linq;
using EF.PoliceMod.Executors;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// “嫌疑人驾车逃逸/逼停”系统（社区级可用版）：
    /// - I 键发布 PullOverRequestedEvent
    /// - 本系统订阅后，对当前嫌疑人所在车辆的司机下达靠边停车任务
    /// </summary>
    public class PullOverSystem
    {
        private readonly SuspectController _suspectController;

        private int _lastPullOverAtMs = 0;
        private const int PullOverCooldownMs = 1500;

        // 当前正在进行的“逼停会话”（简化：同一时间只处理一个）
        private readonly PullOverState _state = new PullOverState();

        // 停稳后自动下车的延迟（给音频/广播留空间）
        private const int AutoExitDelayMs = 1500;

        // I 逼停指令最大有效距离（不要求贴脸；过远仍禁止，避免跨半张地图控车）
        private const float MaxPullOverCommandDistance = 75f;

        public PullOverSystem(SuspectController suspectController)
        {
            _suspectController = suspectController;
            EventBus.Subscribe<PullOverRequestedEvent>(OnPullOverRequested);
            EventBus.Subscribe<PullOverExitRequestedEvent>(OnPullOverExitRequested);
        }

        /// <summary>
        /// 每帧驱动：
        /// 1) 维护“是否已停稳”的标志（允许按 U 要求下车）
        /// 2) 若已按 U 且超时未下车，兜底 WarpOutOfVehicle
        /// 3) 下车后自动 HandsUp，方便 L 锁定 + H 拘捕
        /// </summary>
        public void TickUpdate()
        {
            try
            {
                if (!_state.IsActive)
                    return;

                var driver = Entity.FromHandle(_state.ActiveDriverHandle) as Ped;
                var veh = Entity.FromHandle(_state.ActiveVehicleHandle) as Vehicle;

                if (driver == null || !driver.Exists() || veh == null || !veh.Exists())
                {
                    _state.Clear();
                    return;
                }

                if (Game.GameTime - _state.ParkIssuedAtMs < 250)
                    return;

                if (!_state.PulledOverReady)
                {
                    bool needReissue = false;
                    try { needReissue = veh.Speed > 2.0f; } catch { needReissue = false; }

                    if (needReissue && (Game.GameTime - _state.LastHaltReissueAtMs) > 700)
                    {
                        _state.LastHaltReissueAtMs = Game.GameTime;
                        try { Function.Call(Hash.BRING_VEHICLE_TO_HALT, veh.Handle, 3.0f, 2500, false); } catch { }
                    }
                }

                bool stopped = false;
                try { stopped = Function.Call<bool>(Hash.IS_VEHICLE_STOPPED, veh.Handle); } catch { stopped = false; }

                float distToTarget = 9999f;
                try { distToTarget = veh.Position.DistanceTo(_state.ParkTargetPos); } catch { distToTarget = 9999f; }

                bool slow = false;
                try { slow = veh.Speed <= 1.0f; } catch { slow = false; }
                bool longEnough = (Game.GameTime - _state.ParkIssuedAtMs) >= 2500;
                bool readyNow = (stopped || slow) && ((distToTarget <= 12.0f) || longEnough);

                if (readyNow)
                {
                    if (_state.PulledOverReadyAtMs <= 0) _state.PulledOverReadyAtMs = Game.GameTime;
                }
                else
                {
                    _state.PulledOverReadyAtMs = 0;
                }

                _state.PulledOverReady = readyNow;

                if (_state.PulledOverReady && !_state.ExitRequested)
                {
                    try { Function.Call(Hash.SET_VEHICLE_HANDBRAKE, veh.Handle, true); } catch { }
                    try { Function.Call(Hash.SET_VEHICLE_FORWARD_SPEED, veh.Handle, 0f); } catch { }

                    try { Function.Call(Hash.SET_VEHICLE_INDICATOR_LIGHTS, veh.Handle, 0, true); } catch { }
                    try { Function.Call(Hash.SET_VEHICLE_INDICATOR_LIGHTS, veh.Handle, 1, true); } catch { }

                    if (_state.PulledOverReadyAtMs > 0 && (Game.GameTime - _state.PulledOverReadyAtMs) < AutoExitDelayMs)
                        return;

                    try { driver.Task.ClearAll(); } catch { }
                    try { driver.Task.LeaveVehicle(veh, LeaveVehicleFlags.None); } catch { }
                    _state.ExitRequested = true;
                    _state.ExitIssuedAtMs = Game.GameTime;
                    Notification.Show("~b~已下达下车指令");
                    ModLog.Info("[PullOver] Auto-exit issued (after delay)");
                }

                if (!_state.ExitRequested)
                    return;

                if (driver.IsInVehicle(veh))
                {
                    if (Game.GameTime - _state.ExitIssuedAtMs > 6000)
                    {
                        try { driver.Task.WarpOutOfVehicle(veh); } catch { }
                        ModLog.Warn($"[PullOver] Exit timeout -> WarpOutOfVehicle (driver={driver.Handle})");
                    }
                    return;
                }

                try
                {
                    if (veh != null && veh.Exists())
                    {
                        try { VehicleDoorOps.ShutDoor(veh, 0); } catch { }
                        try { Function.Call(Hash.SET_VEHICLE_INDICATOR_LIGHTS, veh.Handle, 0, false); } catch { }
                        try { Function.Call(Hash.SET_VEHICLE_INDICATOR_LIGHTS, veh.Handle, 1, false); } catch { }
                    }
                }
                catch { }

                if (_state.ExitWillResist)
                {
                    try { driver.Task.ClearAll(); } catch { }
                    try { EventBus.Publish(new EF.PoliceMod.Core.SuspectResistEvent(driver, Game.Player.Character)); } catch { }
                    Notification.Show("~r~嫌疑人拒捕！" + (string.IsNullOrEmpty(_state.RiskTag) ? "" : ("（" + _state.RiskTag + "）")));
                    ModLog.Info($"[PullOver] Driver exited -> resist (driver={driver.Handle}) tag={_state.RiskTag}");
                }
                else
                {
                    try { driver.Task.ClearAll(); } catch { }
                    try { driver.Task.HandsUp(-1); } catch { }

                    try
                    {
                        _suspectController?.TakeControl(driver);
                        _suspectController?.SetCompliant(driver);
                        try { EventBus.Publish(new PullOverEscortBypassActivatedEvent(driver.Handle)); } catch { }
                        try { EFCore.Instance?.LockTargetSystem?.AutoLockAndArrest(driver); } catch { }
                    }
                    catch { }

                    Notification.Show("~g~嫌疑人已下车举手（可直接按 ~y~G~s~ 跟随 / ~y~E~s~ 上下车；也可下车后按 ~y~H~s~ 重新选择拘捕风格）");
                    ModLog.Info($"[PullOver] Driver exited -> hands up (infinite) -> auto locked+arrested (driver={driver.Handle})");
                }

                _state.Clear();
            }
            catch (Exception ex)
            {
                ModLog.Error("[PullOver] TickUpdate error: " + ex);
            }
        }

        private void OnPullOverExitRequested(PullOverExitRequestedEvent e)
        {
            try
            {
                if (!_state.IsActive)
                {
                    Notification.Show("~y~请先对嫌疑人按 I 下达逼停");
                    return;
                }

                var driver = Entity.FromHandle(_state.ActiveDriverHandle) as Ped;
                var veh = Entity.FromHandle(_state.ActiveVehicleHandle) as Vehicle;
                if (driver == null || !driver.Exists() || veh == null || !veh.Exists())
                    return;

                if (!driver.IsInVehicle(veh))
                    return;

                if (!_state.PulledOverReady)
                {
                    float speed = 0f;
                    float dist = 9999f;
                    try { speed = veh.Speed; } catch { }
                    try { dist = veh.Position.DistanceTo(_state.ParkTargetPos); } catch { }
                    ModLog.Info($"[PullOver] U pressed but not ready: speed={speed:F2} distToTarget={dist:F1}");
                    Notification.Show("~y~车辆未停稳，稍等再按 U");
                    return;
                }

                // 若已自动下车流程在跑，U 仅作为兜底，不重复下达
                if (_state.ExitRequested)
                {
                    Notification.Show("~b~已在下车流程中");
                    return;
                }

                // 不用 ClearAllImmediately：会导致瞬移/无动画，改为温和清任务再下车
                try { driver.Task.ClearAll(); } catch { }
                try { driver.Task.LeaveVehicle(veh, LeaveVehicleFlags.None); } catch { }

                _state.ExitRequested = true;
                _state.ExitIssuedAtMs = Game.GameTime;
                Notification.Show("~b~已下达下车指令（兜底）");
                ModLog.Info("[PullOver] U pressed -> LeaveVehicle issued (manual fallback)");
            }
            catch (Exception ex)
            {
                ModLog.Error("[PullOver] OnPullOverExitRequested error: " + ex);
            }
        }

        private void OnPullOverRequested(PullOverRequestedEvent e)
        {
            try
            {
                int now = Game.GameTime;
                if (now - _lastPullOverAtMs < PullOverCooldownMs)
                    return;
                _lastPullOverAtMs = now;

                var player = Game.Player.Character;
                if (player == null || !player.Exists())
                    return;

                // 允许车内/车外都能下达逼停指令（车外同样按射线/评分选车）

                // --- 目标选择：优先使用“瞄准射线”选目标，其次回退到当前案件嫌疑人 ---
                Ped suspect = null;

                // 1) 射线：从相机朝向找 ped/vehicle（范围更广）
                if (e.IsAiming)
                {
                    try
                    {
                        Vector3 start = GameplayCamera.Position;
                        Vector3 end = start + GameplayCamera.Direction * 220f;
                        RaycastResult rr = World.Raycast(start, end, IntersectFlags.Vehicles | IntersectFlags.Peds, player);
                        if (rr.DidHit && rr.HitEntity != null && rr.HitEntity.Exists())
                        {
                            if (rr.HitEntity is Ped)
                            {
                                var p = rr.HitEntity as Ped;
                                if (p != null && p.Exists() && p.IsInVehicle()) suspect = p;
                                else if (p != null && p.Exists()) suspect = p;
                            }
                            else if (rr.HitEntity is Vehicle)
                            {
                                var v = rr.HitEntity as Vehicle;
                                if (v != null && v.Exists())
                                {
                                    try
                                    {
                                        var d2 = v.Driver;
                                        if (d2 != null && d2.Exists()) suspect = d2;
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    catch { suspect = null; }
                }

                // 2) 回退：只允许对“当前案件嫌疑人”下达逼停
                if (suspect == null || !suspect.Exists())
                {
                    try { suspect = _suspectController?.GetCurrentSuspect(); } catch { suspect = null; }
                }

                // 兜底：部分流程下 SuspectController 的引用可能被清空，这里从 CaseManager 再取一次
                if (suspect == null || !suspect.Exists())
                {
                    try { suspect = EFCore.Instance?.GetCaseManager()?.GetCurrentSuspectPed(); } catch { suspect = null; }
                }

                if (suspect == null || !suspect.Exists())
                {
                    Notification.Show("~y~当前没有嫌疑人，无法逼停");
                    return;
                }

                if (!suspect.IsInVehicle())
                {
                    Notification.Show("~y~嫌疑人不在车内，无需逼停");
                    return;
                }

                // 距离限制：不要求贴脸，但也不要太离谱
                try
                {
                    float d = player.Position.DistanceTo(suspect.Position);
                    if (d > MaxPullOverCommandDistance)
                    {
                        Notification.Show("~y~距离过远，靠近一点再按 I（" + ((int)d).ToString() + "m）");
                        return;
                    }
                }
                catch { }

                Vehicle veh = null;
                Ped driver = null;
                try { veh = suspect.CurrentVehicle; } catch { veh = null; }
                if (veh == null || !veh.Exists())
                    return;

                try { driver = veh.Driver; } catch { driver = null; }
                if (driver == null || !driver.Exists())
                    return;

                // 必须是嫌疑人本人在驾驶位（避免误判同车其他乘客）
                if (driver.Handle != suspect.Handle)
                {
                    Notification.Show("~y~嫌疑人不在驾驶位，无法逼停");
                    return;
                }



                // 追车三档（按你需求重做）：
                // 1) 普通：按 I 必停（不反抗）
                // 2) 中危：有概率逃逸；提示玩家打爆轮胎/PIT/强制拽出
                // 3) 高危：高概率车内反抗（开枪）；提示“已授权击毙”，该案不走“下车拘捕”流程
                try
                {
                    bool isHigh = false;
                    bool isMid = false;

                    try
                    {
                        float rc = _suspectController != null ? _suspectController.ResistChance : 0f;
                        bool firearm = _suspectController != null && _suspectController.HasFirearm;
                        isHigh = firearm || rc >= 0.80f;
                        isMid = !isHigh && rc >= 0.50f;
                    }
                    catch
                    {
                        isHigh = false;
                        isMid = false;
                    }

                    _state.RiskTag = isHigh ? "高危" : (isMid ? "中危" : "普通");
                    var rng = new Random(Game.GameTime + driver.Handle);

                    // ===== 高危：直接进入武装反抗（车内开枪 + 逃逸），不进入逼停会话 =====
                    if (isHigh)
                    {
                        ModLog.Info("[PullOver] 高危 -> armed resist (in-vehicle)");
                        EF.PoliceMod.Core.SmsNotification.Show(
                            "911调度",
                            "高危嫌疑人",
                            "目标武装反抗，正在驾车逃逸！\n已授权使用致命武力。"
                        );

                        try { driver.Task.ClearAll(); } catch { }

                        // 车内反抗：确保有“可车内射击”的武器（霰弹枪无法 drive-by）
                        try
                        {
                            driver.Weapons.Give(WeaponHash.MicroSMG, 240, true, true);
                            Function.Call(Hash.SET_CURRENT_PED_WEAPON, driver.Handle, (uint)WeaponHash.MicroSMG, true);
                        }
                        catch { }

                        // 追逐并猛撞：先追玩家（比 Wander 更主动）
                        try { Function.Call(Hash.TASK_VEHICLE_CHASE, driver.Handle, player.Handle); } catch { }

                        // 激进驾驶参数（best effort）
                        try { Function.Call(Hash.SET_DRIVER_AGGRESSIVENESS, driver.Handle, 1.0f); } catch { }
                        try { Function.Call(Hash.SET_DRIVER_RACING_MODIFIER, driver.Handle, 1.0f); } catch { }
                        try { Function.Call(Hash.SET_DRIVE_TASK_DRIVING_STYLE, driver.Handle, 786468); } catch { }

                        // 车内射击（best-effort）。注意：不要立刻用 DriveWander 覆盖 chase，否则会变成“随便开”。
                        try
                        {
                            var dist = player.Position.DistanceTo(driver.Position);
                            if (dist < 70f)
                            {
                                Function.Call(Hash.TASK_DRIVE_BY, driver.Handle, player.Handle, 0, 0f, 0f, 0f, 85f, 600, true, Game.GenerateHash("FIRING_PATTERN_FULL_AUTO"));
                            }
                        }
                        catch { }

                        _state.Clear();
                        return;
                    }

                    // ===== 中危：概率逃逸（不再“下车后反抗”） =====
                    if (isMid)
                    {
                        int fleeRoll = rng.Next(100);
                        bool flee = fleeRoll < 30; // “一定概率”
                        if (flee)
                        {
                            ModLog.Info("[PullOver] 中危 -> fleeing");
                            EF.PoliceMod.Core.SmsNotification.Show(
                                "911调度",
                                "中危嫌疑人",
                                "目标拒检逃逸！\n建议：PIT/打爆轮胎迫使其停车，然后将其从车内拽出并锁定拘捕。"
                            );

                            try { driver.Task.ClearAll(); } catch { }
                            try { Function.Call(Hash.TASK_VEHICLE_DRIVE_WANDER, driver.Handle, veh.Handle, 35.0f, 786468); } catch { }

                            _state.Clear();
                            return;
                        }
                    }

                    // ===== 普通/中危(未逃逸)：必停，且不走“下车反抗” =====
                    _state.ExitWillResist = false;
                }
                catch { }

                // 选一个右侧道路点作为靠边点（尽量避免直接用当前点导致卡住）
                Vector3 hintPos = veh.Position + veh.RightVector * 8.0f;
                Vector3 parkPos;
                try
                {
                    parkPos = World.GetNextPositionOnStreet(hintPos);
                }
                catch
                {
                    parkPos = veh.Position;
                }

                float heading = veh.Heading;

                // 先清理司机当前任务，避免与逃逸任务对抗
                try { driver.Task.ClearAll(); } catch { }

                try
                {
                    // ScriptHookV 报错：FATAL: Can't find native 0x000000002623D5A9
                    // 该错误通常是“当前 ScriptHookV 的 native 表里没有某个任务函数”。
                    // 为了稳定性，这里不再使用 TASK_VEHICLE_PARK，改用你提供的“模组所有函数”表内的：BRING_VEHICLE_TO_HALT。
                    // BRING_VEHICLE_TO_HALT(Vehicle vehicle, float distance, int duration, BOOL unknown)
                    try { Function.Call(Hash.BRING_VEHICLE_TO_HALT, veh.Handle, 3.0f, 4000, false); } catch { }

                    // 记录会话：等待停稳后允许 U 下车（仍允许玩家走锁定/拘捕流程）
                    _state.ActiveDriverHandle = driver.Handle;
                    _state.ActiveVehicleHandle = veh.Handle;
                    _state.ParkIssuedAtMs = Game.GameTime;
                    _state.PulledOverReady = false;
                    _state.ExitRequested = false;
                    _state.ExitIssuedAtMs = 0;
                    _state.ParkTargetPos = parkPos;
                    _state.ParkTargetHeading = heading;

                    ModLog.Info($"[PullOver] BRING_VEHICLE_TO_HALT issued driver={driver.Handle} veh={veh.Handle} pos={parkPos}");
                    EF.PoliceMod.Core.SmsNotification.Show(
                        "911调度",
                        "逼停指令",
                        "已下达逼停（" + _state.RiskTag + "）\n目标停稳后将自动下车举手（U 为兜底）"
                    );
                }
                catch (Exception exTask)
                {
                    ModLog.Error("[PullOver] TASK_VEHICLE_PARK failed: " + exTask);
                    Notification.Show("~r~逼停失败（任务下达异常）");
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[PullOver] OnPullOverRequested error: " + ex);
            }
        }
    }
}
