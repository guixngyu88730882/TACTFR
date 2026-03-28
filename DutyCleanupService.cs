using EF.PoliceMod.Core;
using EF.PoliceMod.Gameplay;
using EF.PoliceMod;
using GTA;
using GTA.Native;
using System;

namespace EF.PoliceMod.Systems
{
    public sealed class DutyCleanupService
    {
        private readonly LockTargetSystem _lockTargetSystem;
        private readonly SuspectController _suspectController;
        private readonly SuspectStateHub _suspectStateHub;
        private readonly BehaviorKeeper _behaviorKeeper;

        public DutyCleanupService(
            LockTargetSystem lockTargetSystem,
            SuspectController suspectController,
            SuspectStateHub suspectStateHub,
            BehaviorKeeper behaviorKeeper)
        {
            _lockTargetSystem = lockTargetSystem;
            _suspectController = suspectController;
            _suspectStateHub = suspectStateHub;
            _behaviorKeeper = behaviorKeeper;

            EventBus.Subscribe<DutyEndedEvent>(_ => Cleanup("DutyEnded"));
            EventBus.Subscribe<EF.PoliceMod.Core.SuspectDeliveredEvent>(_ => Cleanup("SuspectDelivered"));
        }

        private void Cleanup(string reason)
        {
            try { _lockTargetSystem?.ForceClear(); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClear lock target failed ({reason}): {ex}"); }

            try { _suspectController?.ForceClear(); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClear suspect controller failed ({reason}): {ex}"); }

            try { _suspectStateHub?.ChangeState(SuspectState.None); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] Reset state hub failed ({reason}): {ex}"); }

            try { _behaviorKeeper?.ForceClear(reason); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClear behavior keeper failed ({reason}): {ex}"); }

            try { if (UIState.IsPoliceTerminalOpen) UIState.ForceClose(UIState.PoliceTerminalSession, reason); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClose police terminal failed ({reason}): {ex}"); }

            try { if (UIState.IsDispatchMenuOpen) UIState.ForceClose(UIState.DispatchMenuSession, reason); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClose dispatch menu failed ({reason}): {ex}"); }

            try { if (UIState.IsArrestMenuOpen) UIState.ForceClose(UIState.ArrestMenuSession, reason); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClose arrest menu failed ({reason}): {ex}"); }

            try { if (UIState.IsOfficerSquadMenuOpen) UIState.ForceClose(UIState.OfficerSquadMenuSession, reason); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClose officer squad menu failed ({reason}): {ex}"); }

            try { if (UIState.IsPatrolMenuOpen) UIState.ForceClose(UIState.PatrolMenuSession, reason); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClose patrol menu failed ({reason}): {ex}"); }

            try { if (UIState.IsUniformMenuOpen) UIState.ForceClose(UIState.UniformMenuSession, reason); }
            catch (Exception ex) { ModLog.Error($"[DutyCleanup] ForceClose uniform menu failed ({reason}): {ex}"); }

            try
            {
                var player = Game.Player.Character;
                if (player != null && player.Exists())
                {
                    try { Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, player.Handle, 0.25f); }
                    catch (Exception ex) { ModLog.Error($"[DutyCleanup] Reset player clipset failed ({reason}): {ex}"); }

                    try { Function.Call(Hash.FREEZE_ENTITY_POSITION, player.Handle, false); }
                    catch (Exception ex) { ModLog.Error($"[DutyCleanup] Unfreeze player failed ({reason}): {ex}"); }

                    try { Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, true, 0); }
                    catch (Exception ex) { ModLog.Error($"[DutyCleanup] Restore player control failed ({reason}): {ex}"); }

                    try { Function.Call(Hash.ENABLE_ALL_CONTROL_ACTIONS, 0); }
                    catch (Exception ex) { ModLog.Error($"[DutyCleanup] Enable controls failed ({reason}): {ex}"); }
                }
            }
            catch (Exception ex)
            {
                ModLog.Error($"[DutyCleanup] Cleanup player state failed ({reason}): {ex}");
            }
        }
    }
}
