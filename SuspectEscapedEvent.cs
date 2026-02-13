using GTA;

namespace EF.PoliceMod.Core
{
    public struct SuspectEscapedEvent
    {
        public int SuspectHandle;

        public SuspectEscapedEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }
}