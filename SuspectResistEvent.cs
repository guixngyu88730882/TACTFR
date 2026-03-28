using GTA;

namespace EF.PoliceMod.Core
{
    // P0-2 Fix: Unified SuspectResistEvent - previously duplicated in Input namespace
    public struct SuspectResistEvent
    {
        // Support both Ped and Handle for backward compatibility
        public Ped Suspect { get; }
        public Ped Attacker { get; }
        public int SuspectHandle { get; }
        public int PlayerHandle { get; }

        // Constructor for Ped-based usage (from Input namespace)
        public SuspectResistEvent(Ped suspect, Ped attacker)
        {
            Suspect = suspect;
            Attacker = attacker;
            SuspectHandle = suspect != null && suspect.Exists() ? suspect.Handle : 0;
            PlayerHandle = attacker != null && attacker.Exists() ? attacker.Handle : 0;
        }

        // Constructor for Handle-based usage
        public SuspectResistEvent(int suspectHandle, int playerHandle)
        {
            SuspectHandle = suspectHandle;
            PlayerHandle = playerHandle;
            Suspect = null;
            Attacker = null;
        }
    }
}
