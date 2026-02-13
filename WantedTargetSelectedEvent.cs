namespace EF.PoliceMod.Core
{
    // 终端选择一个通缉对象后发布
    public struct WantedTargetSelectedEvent
    {
        public int PedHandle;
        public WantedTargetSelectedEvent(int pedHandle)
        {
            PedHandle = pedHandle;
        }
    }
}
