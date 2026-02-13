// SuspectDeadEvent.cs
using GTA;

namespace EF.PoliceMod.Core
{
    public struct SuspectDeadEvent
    {
        // ֻ�� Ped.Handle�����ķ��������� new Ped(handle) ���� Exists()/IsAlive ���
        public int SuspectHandle;

        public SuspectDeadEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }
}
