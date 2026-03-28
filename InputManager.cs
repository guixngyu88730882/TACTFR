using EF.PoliceMod.Core;
using EF.PoliceMod.Input;
using GTA;
using GTA.Native;
using GTA.UI;
using System;
using Keys = System.Windows.Forms.Keys;

namespace EF.PoliceMod.Input
{
    public class InputManager
    {
        private bool _arrestKeyHeld;
        private bool _escortRequested;
        private bool _escortInteractHeld;
        private bool _openTerminalHeld = false;
        private bool _f10Held = false;
        private bool _pullOverHeld = false;
        private bool _pullOverExitHeld = false;
        private bool _dispatchMenuHeld = false;
        private bool _lockHeld = false;
        private bool _unlockHeld = false;
        private bool _f8Held = false;
        private bool _hHeldRaw = false;
        private bool _gHeldRaw = false;
        private bool _eHeldRaw = false;
        private bool _yHeldRaw = false;
        private bool _pHeldRaw = false;
        private int _lastAimedPublishedAtMs = 0;
        private Ped _lastAimedTarget = null;
        private bool _wasAiming = false;
        private int _lastAimedHandle = 0;
        private const int AIM_PUBLISH_MIN_INTERVAL_MS = 200;

        private DateTime _lastTerminalToggle = DateTime.MinValue;
        private readonly TimeSpan _terminalDebounce = TimeSpan.FromMilliseconds(800); // Fix: 加长冷却时间防止重入

        private bool IsRawKeyDown(System.Windows.Forms.Keys k)
        {
            return Game.IsKeyPressed(k);
        }

        public struct SuspectKilledByPlayerEvent
        {
            public int SuspectHandle { get; }
            public SuspectKilledByPlayerEvent(int suspectHandle) { SuspectHandle = suspectHandle; }
        }

        public void Update()
        {
            int now = Game.GameTime;
            try { EF.PoliceMod.Core.UIState.AutoRecover(now); } catch { }

            bool anyUiOpen = EF.PoliceMod.Core.UIState.IsAnyUiOpen;

            bool pressedO = IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.OpenTerminal);
            bool pressedT = IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.VehicleTerminal);

            bool patrolMenuHotkey = IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.PatrolMenu);
            if (patrolMenuHotkey)
            {
                if (!_yHeldRaw)
                {
                    _yHeldRaw = true;
                    if (!anyUiOpen)
                    {
                        EventBus.Publish(new EF.PoliceMod.Core.PatrolMenuToggledEvent(true));
                        ModLog.Info("[Input] Patrol menu hotkey pressed");
                    }
                }
            }
            else
            {
                _yHeldRaw = false;
            }

            bool terminalKeyDown = pressedO || pressedT;

            if (EF.PoliceMod.Core.UIState.IsPoliceTerminalOpen)
            {
                _openTerminalHeld = terminalKeyDown;

                try
                {
                    Function.Call(GTA.Native.Hash.DISABLE_ALL_CONTROL_ACTIONS, 0);
                }
                catch { }

                return;
            }
            else if (terminalKeyDown)
            {
                if (!_openTerminalHeld)
                {
                    _openTerminalHeld = true;

                    if (!anyUiOpen)
                    {
                        if (DateTime.UtcNow - _lastTerminalToggle > _terminalDebounce)
                        {
                            _lastTerminalToggle = DateTime.UtcNow;

                            var src = pressedT ? EF.PoliceMod.Input.OpenPoliceTerminalSource.VehicleTerminal : EF.PoliceMod.Input.OpenPoliceTerminalSource.StationTerminal;

                            try
                            {
                                EventBus.Publish(new OpenPoliceTerminalEvent(src));
                                ModLog.Info($"[Input] OpenPoliceTerminalEvent published, source={src}");
                            }
                            catch (Exception ex)
                            {
                                ModLog.Error("[Input] Failed to publish OpenPoliceTerminalEvent: " + ex);
                            }
                        }
                    }
                }
            }
            else
            {
                _openTerminalHeld = false;
            }

            bool isAiming = false;
            try
            {
                bool onFootAim = false;
                bool vehicleAim = false;
                try { onFootAim = Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 0, (int)GTA.Control.Aim); } catch { onFootAim = false; }
                try { vehicleAim = Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 0, (int)GTA.Control.VehicleAim); } catch { vehicleAim = false; }
                isAiming = onFootAim || vehicleAim;
            }
            catch (Exception ex)
            {
                ModLog.Error("[Input] Exception calling IS_CONTROL_PRESSED: " + ex);
                isAiming = false;
            }

            now = Game.GameTime;
            bool enoughTimePassed = now - _lastAimedPublishedAtMs >= AIM_PUBLISH_MIN_INTERVAL_MS;

            if (isAiming)
            {
                if (enoughTimePassed)
                {
                    _lastAimedPublishedAtMs = now;
#if DEBUG
                    ModLog.Info("[Input] Player is aiming");
#endif
                }
            }
            else
            {
                _lastAimedPublishedAtMs = now;
            }

            if (IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.ArrestMenu))
            {
                if (!_hHeldRaw)
                {
                    _hHeldRaw = true;
                    EventBus.Publish(new OpenArrestActionMenuEvent());
                }
            }
            else
            {
                _hHeldRaw = false;
            }



            if (IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.DispatchMenu) || IsRawKeyDown(System.Windows.Forms.Keys.F7))
            {
                if (!_dispatchMenuHeld)
                {
                    _dispatchMenuHeld = true;

                    if (!EF.PoliceMod.Core.FeatureGates.EnableF7DispatchMenu)
                    {
                        Notification.Show("~y~当前版本已暂时关闭 F7 调度菜单");
                    }
                    else
                    {
                        EventBus.Publish(new Open911MenuEvent());
                    }
                }
            }
            else
            {
                _dispatchMenuHeld = false;
            }

            if (IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.EscortRequest))
            {
                if (!_gHeldRaw)
                {
                    _gHeldRaw = true;
                    EventBus.Publish(new SuspectFollowRequestEvent());
                }
            }
            else
            {
                _gHeldRaw = false;
            }

            if (IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.LockTarget))
            {
                if (!_lockHeld)
                {
                    _lockHeld = true;
                    EventBus.Publish(new LockTargetEvent());
                }
            }
            else
            {
                _lockHeld = false;
            }

            bool ctrlDown = IsRawKeyDown(Keys.ControlKey) || IsRawKeyDown(Keys.LControlKey) || IsRawKeyDown(Keys.RControlKey);
            if (ctrlDown)
            {
                if (!_unlockHeld)
                {
                    _unlockHeld = true;
                    EventBus.Publish(new LockTargetClearRequestedEvent());
                }
            }
            else
            {
                _unlockHeld = false;
            }

            if (Game.IsKeyPressed(EF.PoliceMod.Core.KeyBindings.ToggleHelp))
            {
                if (!_f10Held)
                {
                    _f10Held = true;
                    EventBus.Publish(new EF.PoliceMod.Core.ToggleHelpEvent());
#if DEBUG
                    ModLog.Info("[Input] F10 pressed -> ToggleHelpEvent published");
#endif
                }
            }
            else
            {
                _f10Held = false;
            }

            if (Game.IsKeyPressed(EF.PoliceMod.Core.KeyBindings.OfficerSquadMenu))
            {
                if (!_f8Held)
                {
                    _f8Held = true;
                    EventBus.Publish(new OpenOfficerSquadMenuEvent());
                }
            }
            else
            {
                _f8Held = false;
            }

            if (IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.PullOver))
            {
                if (!_pullOverHeld)
                {
                    _pullOverHeld = true;
                    ModLog.Info("[Input] I pressed (pull over)");
                    EventBus.Publish(new EF.PoliceMod.Core.PullOverRequestedEvent(isAiming));
                }
            }
            else
            {
                _pullOverHeld = false;
            }

            if (Game.IsKeyPressed(EF.PoliceMod.Core.KeyBindings.PullOverExit))
            {
                if (!_pullOverExitHeld)
                {
                    _pullOverExitHeld = true;
                    EventBus.Publish(new EF.PoliceMod.Core.PullOverExitRequestedEvent());
                }
            }
            else
            {
                _pullOverExitHeld = false;
            }

            if (IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.DeliverSuspect))
            {
                if (!_pHeldRaw)
                {
                    _pHeldRaw = true;
                    EventBus.Publish(new DeliverRequestedEvent());
                    ModLog.Info($"[Input] {EF.PoliceMod.Core.KeyBindings.DeliverSuspect} pressed -> DeliverRequestedEvent");
                }
            }
            else
            {
                _pHeldRaw = false;
            }

            if (IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.VehicleInteract))
            {
                if (!_eHeldRaw)
                {
                    _eHeldRaw = true;
                    var player = Game.Player.Character;
                    if (player != null && player.Exists())
                    {
                        EventBus.Publish(new EscortVehicleInteractEvent(player.Handle));
                        ModLog.Info("[Input] E pressed → EscortVehicleInteractEvent");
                    }
                }
            }
            else
            {
                _eHeldRaw = false;
            }
        }
    }
}
