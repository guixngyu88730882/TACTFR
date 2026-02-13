namespace EF.PoliceMod.Core
{
    /// <summary>
    /// I 逼停线（PullOverSystem）专用：嫌疑人自动下车后，允许在未 L 锁定/未走 H 菜单拘捕的情况下
    /// 直接使用 G/E 指挥跟随与上下车（仅用于抱头/举手线）。
    /// </summary>
    public class PullOverEscortBypassActivatedEvent
    {
        public int SuspectHandle { get; }

        public PullOverEscortBypassActivatedEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }
}