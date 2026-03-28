using EF.PoliceMod.Core;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Linq;


namespace EF.PoliceMod.Gameplay
{
    public class DeliverSystem
    {
        private readonly LockTargetSystem _lockTargetSystem;

        private readonly Vector3 _policeStationPos = new Vector3(441.2f, -981.9f, 30.7f);
        private readonly Vector3 _policeStationPosAlt = new Vector3(443.5f, -981.0f, 30.7f);
        private const float DeliverRadius = 18.0f;

        private bool _waitingForSuspectExit = false;
        private int _exitTargetHandle = -1;
        private int _exitTimeoutMs = 0;
        private Vehicle _exitVehicle = null;
        // P1-1 Fix: Removed World.GetAllPeds() per-frame scan, use handle-based lookup instead
        private Ped GetDeliverTarget()
        {
            try
            {
                var cm = EFCore.Instance?.GetCaseManager();
                if (cm != null && cm.HasActiveCase)
                {
                    var player = Game.Player.Character;
                    if (player == null || !player.Exists()) return null;

                    // P1-1 Fix: Use handle-based lookup instead of World.GetAllPeds()
                    var handles = cm.SuspectHandles;
                    if (handles != null && handles.Count > 0)
                    {
                        Ped bestPed = null;
                        float bestDist = float.MaxValue;

                        foreach (var h in handles)
                        {
                            if (h <= 0) continue;
                            
                            // P1-1 Fix: Use Entity.FromHandle instead of World.GetAllPeds()
                            Ped ped = null;
                            try { ped = Entity.FromHandle(h) as Ped; } catch { }
                            
                            if (ped == null || !ped.Exists()) continue;
                            if (ped.IsDead) continue;

                            // P1-1 Fix: Only consider deliver-ready suspects
                            if (!IsDeliverReady(player, ped)) continue;

                            float d = ped.Position.DistanceTo(player.Position);
                            if (d < bestDist)
                            {
                                bestDist = d;
                                bestPed = ped;
                            }
                        }

                        if (bestPed != null) return bestPed;
                    }

                    // Single suspect fallback
                    int activeHandle = cm.CurrentSuspectHandle;
                    if (activeHandle > 0)
                    {
                        Ped ped = null;
                        try { ped = Entity.FromHandle(activeHandle) as Ped; } catch { }
                        if (ped != null && ped.Exists()) return ped;
                    }
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[Deliver] GetDeliverTarget error: " + ex);
            }

            return null;
        }

        private bool IsCaseTarget(Ped suspect)
        {
            if (suspect == null || !suspect.Exists()) return false;
            try
            {
                var cm = EFCore.Instance?.GetCaseManager();
                if (cm == null || !cm.HasActiveCase) return false;

                if (cm.CurrentSuspectHandle == suspect.Handle) return true;

                var handles = cm.SuspectHandles;
                if (handles != null)
                {
                    foreach (var h in handles)
                    {
                        if (h == suspect.Handle) return true;
                    }
                }
            }
            catch { }

            return false;
        }

        private bool IsDeliverReady(Ped player, Ped suspect)
        {
            if (player == null || !player.Exists()) return false;
            if (suspect == null || !suspect.Exists()) return false;

            // 玩家必须在警局附近
            if (!IsNearPoliceStation(player.Position)) return false;

            // ★关键：嫌疑人也必须在警局附近（避免“刚接任务站在警局就能交付远处嫌疑人”）
            if (!IsNearPoliceStation(suspect.Position)) return false;

            // 建议交付条件：嫌疑人必须被你押送到位——
            // 1) 同车（嫌疑人在你的车里），或
            // 2) 已下车但贴近你（例如下车后站在门口）
            try
            {
                if (player.IsInVehicle())
                {
                    var pv = player.CurrentVehicle;
                    if (pv != null && pv.Exists() && suspect.IsInVehicle(pv)) return true;
                }
            }
            catch { }

            try
            {
                if (suspect.Position.DistanceTo(player.Position) <= 6.0f) return true;
            }
            catch { }

            return false;
        }

        private bool CanShowDeliverHint()
        {
            Ped target = GetDeliverTarget();
            if (target == null || !target.Exists())
                return false;

            Ped player = Game.Player.Character;
            if (!player.Exists())
                return false;

            return IsDeliverReady(player, target);
        }
        private bool SupportsAnimatedExit(Vehicle veh)
        {
            int seatCount = Function.Call<int>(
                Hash.GET_VEHICLE_MODEL_NUMBER_OF_SEATS,
                veh.Model.Hash
            );

            return seatCount >= 4;
        }




        public DeliverSystem(LockTargetSystem lockTargetSystem)
        {
            _lockTargetSystem = lockTargetSystem;

            EventBus.Subscribe<EF.PoliceMod.Core.DeliverRequestedEvent>(_ => OnDeliverRequested());
            EventBus.Subscribe<DutyEndedEvent>(_ => { try { _lockTargetSystem?.ForceClear(); } catch { } });
            EventBus.Subscribe<EF.PoliceMod.Core.SuspectDeliveredEvent>(_ => { try { _lockTargetSystem?.ForceClear(); } catch { } });
        }
        private void OnDeliverRequested()
        {
            if (!CanDeliver())
                return;

            Deliver();
        }

        private void FinalizeDeliveredSuspect(Ped suspect)
        {
            if (suspect == null || !suspect.Exists())
                return;

         

            try
            {
                try { suspect.Task.ClearAllImmediately(); } catch (Exception) { /* best effort */ }
                try { suspect.MarkAsNoLongerNeeded(); } catch (Exception) { /* best effort */ }
                try { suspect.Delete(); } catch (Exception ex) { ModLog.Error("[Deliver] FinalizeDeliveredSuspect Delete failed: " + ex); }
                ModLog.Info($"[Deliver] FinalizeDeliveredSuspect: cleaned suspect {suspect.Handle}");
            }
            catch (Exception ex)
            {
                ModLog.Error("[Deliver] FinalizeDeliveredSuspect failed: " + ex);
            }
        }

        public void Initialize()
        {
            // 这里暂时不靠事件，走 Tick 检测（更稳定）
        }

            // 仅允许交付当前案件嫌疑人，隔离巡逻模式目标
        public void Shutdown()
        {
        }

        /// <summary>
        /// 每帧调用（在 EFCore.OnTick 或主循环里调）
        /// </summary>
        public void Update()
        {
            if (_waitingForSuspectExit)
            {
                try
                {
                    Ped target = Entity.FromHandle(_exitTargetHandle) as Ped;

                    if (target == null || !target.Exists())
                    {
                        ModLog.Warn("[Deliver] Exit wait aborted: target lost");
                        _waitingForSuspectExit = false;
                        _exitTargetHandle = 0;
                        _exitVehicle = null;
                        _exitTimeoutMs = 0;
                        return;
                    }

                    if (!target.IsInVehicle())
                    {
                        _waitingForSuspectExit = false;
                        CompleteDelivery(target);
                        return;
                    }

                    if (Game.GameTime >= _exitTimeoutMs)
                    {
                        ModLog.Warn("[Deliver] 退出超时，强制传送");
                        try { target.Task.WarpOutOfVehicle(_exitVehicle); } catch { }
                        _waitingForSuspectExit = false;
                        CompleteDelivery(target);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ModLog.Error("[Deliver] 退出等待错误：" + ex);
                    _waitingForSuspectExit = false;
                    _exitTargetHandle = 0;
                    _exitVehicle = null;
                    _exitTimeoutMs = 0;
                }
                return;
            }

            if (!CanShowDeliverHint())
                return;

            GTA.UI.Screen.ShowSubtitle($"~y~按 {EF.PoliceMod.Core.KeyBindings.DeliverSuspect} 键交付嫌疑人", 1);
        }


        private bool CanDeliver()
        {
            Ped target = GetDeliverTarget();
            if (target == null || !target.Exists())
                return false;

            Ped player = Game.Player.Character;
            if (!player.Exists())
                return false;

            return IsDeliverReady(player, target);
        }


        private bool IsNearPoliceStation(Vector3 playerPos)
        {
            try
            {
                if (playerPos.DistanceTo(_policeStationPos) <= DeliverRadius) return true;
            }
            catch { }

            try
            {
                if (playerPos.DistanceTo(_policeStationPosAlt) <= DeliverRadius) return true;
            }
            catch { }

            return false;
        }



        private void Deliver()
        {
            Ped target = GetDeliverTarget();
            if (target == null || !target.Exists())
                return;

            if (target.IsInVehicle())
            {
                try
                {
                    try { target.Task.ClearAll(); } catch { }
                    target.BlockPermanentEvents = true;
                    target.AlwaysKeepTask = true;

                    Vehicle veh = target.CurrentVehicle;
                    // P1-1 Fix: Removed Script.Wait(0) which blocks the main thread

                    if (veh == null || !veh.Exists())
                    {
                        ModLog.Info("[Deliver] Deliver aborted: vehicle invalid");
                        return;
                    }

                    if (SupportsAnimatedExit(veh))
                    {
                        try
                        {
                            target.Task.LeaveVehicle(veh, (LeaveVehicleFlags)256);
                            _waitingForSuspectExit = true;
                            _exitTargetHandle = target.Handle;
                            _exitVehicle = veh;
                            _exitTimeoutMs = Game.GameTime + 8000; // Give it 8 seconds to play animation
                            return;
                        }
                        catch (Exception ex)
                        {
                            ModLog.Error("[Deliver] LeaveVehicle failed: " + ex);
                            try { target.Task.WarpOutOfVehicle(veh); } catch { }
                        }
                    }
                    else
                    {
                        try
                        {
                            target.Task.WarpOutOfVehicle(veh);
                            Vector3 offset = veh.RightVector * 1.5f;
                            target.Position = veh.Position + offset;
                        }
                        catch (Exception ex)
                        {
                            ModLog.Error("[Deliver] WarpOutOfVehicle failed: " + ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModLog.Error("[Deliver] Deliver vehicle-exit sequence failed: " + ex);
                }
            }

            CompleteDelivery(target);
        }

        private void CompleteDelivery(Ped target)
        {
            if (target == null || !target.Exists())
            {
                ModLog.Warn("[Deliver] CompleteDelivery aborted: target invalid");
                return;
            }

            Game.Player.Money += 10000;
            GTA.UI.Notification.Show("~g~嫌疑人已成功交付\n~w~+$10000");

            try
            {
                EventBus.Publish(new EF.PoliceMod.Core.SuspectDeliveredEvent(target.Handle));
            }
            catch (Exception exPub)
            {
                ModLog.Error("[Deliver] Publish SuspectDeliveredEvent failed: " + exPub);
            }

            try
            {
                FinalizeDeliveredSuspect(target);
            }
            catch (Exception exFinalize)
            {
                ModLog.Error("[Deliver] FinalizeDeliveredSuspect failed: " + exFinalize);
            }

            try
            {
                _lockTargetSystem.ForceClear();
            }
            catch (Exception exClear)
            {
                ModLog.Error("[Deliver] LockTargetSystem.ForceClear failed: " + exClear);
            }

            _exitTargetHandle = 0;
            _exitVehicle = null;
            _exitTimeoutMs = 0;
            _waitingForSuspectExit = false;
        }
    }
}
