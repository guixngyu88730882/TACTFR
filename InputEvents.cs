using GTA;

namespace EF.PoliceMod.Input
{
    public struct Open911MenuEvent { }

    public struct LockTargetEvent { }

    public struct AttemptArrestEvent { }

    public struct OpenArrestActionMenuEvent { }

    public enum OpenPoliceTerminalSource
    {
        StationTerminal,
        VehicleTerminal
    }

    public struct OpenPoliceTerminalEvent
    {
        public OpenPoliceTerminalSource Source { get; }
        public OpenPoliceTerminalEvent(OpenPoliceTerminalSource source)
        {
            Source = source;
        }
    }

    public struct OpenOfficerSquadMenuEvent { }

    // P0-2 Fix: Removed SuspectEscortBeginEvent - use EF.PoliceMod.Core.SuspectEscortRequestEvent.SuspectEscortBeginEvent instead
    // P0-2 Fix: Removed SuspectResistEvent - use EF.PoliceMod.Core.SuspectResistEvent instead
}
