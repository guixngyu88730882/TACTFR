using System.Collections.Generic;
using GTA;
using Keys = System.Windows.Forms.Keys;

namespace EF.PoliceMod.Core
{
    public static class ModConfig
    {
        public static bool EnableAimToPickupByAiming { get; set; } = false;

        public const float LockDistance = 15f;
        public const float LoseDistance = 100f;
        public const float ArrestDistance = 8f;

        public static int MaxBackupCars { get; set; } = 4;

        // Support cooldowns
        // Heli recon: 使用一次后 120s 内不得再次使用
        public const int HeliReconCooldownMs = 120 * 1000;

        public static void EnableAimPickup() => EnableAimToPickupByAiming = true;
        public static void DisableAimPickup() => EnableAimToPickupByAiming = false;
    }

    public static class KeyBindings
    {
        public static Keys DeliverSuspect { get; set; } = Keys.Z;
        public static Keys LockTarget { get; set; } = Keys.L;
        public static Keys OpenTerminal { get; set; } = Keys.O;
        public static Keys VehicleTerminal { get; set; } = Keys.T;
        public static Keys PatrolMenu { get; set; } = Keys.Q;
        public static Keys ArrestMenu { get; set; } = Keys.H;
        public static Keys EscortRequest { get; set; } = Keys.G;
        public static Keys VehicleInteract { get; set; } = Keys.E;
        public static Keys PullOver { get; set; } = Keys.I;
        public static Keys PullOverExit { get; set; } = Keys.U;
        public static Keys DispatchMenu { get; set; } = Keys.F7;
        public static Keys OfficerSquadMenu { get; set; } = Keys.F8;
        public static Keys ToggleHelp { get; set; } = Keys.F10;

        private static readonly Dictionary<string, Keys> _defaultBindings = new Dictionary<string, Keys>
        {
            { "DeliverSuspect", Keys.Z },
            { "LockTarget", Keys.L },
            { "OpenTerminal", Keys.O },
            { "VehicleTerminal", Keys.T },
            { "PatrolMenu", Keys.Q },
            { "ArrestMenu", Keys.H },
            { "EscortRequest", Keys.G },
            { "VehicleInteract", Keys.E },
            { "PullOver", Keys.I },
            { "PullOverExit", Keys.U },
            { "DispatchMenu", Keys.F7 },
            { "OfficerSquadMenu", Keys.F8 },
            { "ToggleHelp", Keys.F10 }
        };

        public static void ResetToDefaults()
        {
            DeliverSuspect = Keys.Z;
            LockTarget = Keys.L;
            OpenTerminal = Keys.O;
            VehicleTerminal = Keys.T;
            PatrolMenu = Keys.Q;
            ArrestMenu = Keys.H;
            EscortRequest = Keys.G;
            VehicleInteract = Keys.E;
            PullOver = Keys.I;
            PullOverExit = Keys.U;
            DispatchMenu = Keys.F7;
            OfficerSquadMenu = Keys.F8;
            ToggleHelp = Keys.F10;
        }

        public static string GetKeyDisplayName(Keys key)
        {
            return key.ToString();
        }
    }
}
