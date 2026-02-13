// SuspectDeliveredEvent.cs
using GTA;

namespace EF.PoliceMod.Core
{
    public struct SuspectDeliveredEvent
    {
        // ֻ�� Ped.Handle�����ķ��������� new Ped(handle) ���� Exists()/IsAlive ���
        public int SuspectHandle;

        public SuspectDeliveredEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }
}
