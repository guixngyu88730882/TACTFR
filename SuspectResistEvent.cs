using GTA;

namespace EF.PoliceMod.Core
{
    public struct SuspectResistEvent
    {
        // ֻ�� Ped.Handle�����ķ��������� new Ped(handle) ���� Exists()/IsAlive ���
        public int SuspectHandle;
        public int PlayerHandle;

        public SuspectResistEvent(int suspectHandle, int playerHandle)
        {
            SuspectHandle = suspectHandle;
            PlayerHandle = playerHandle;
        }
    }
}
