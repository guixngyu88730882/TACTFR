namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 玩家发出“逼停”指令（默认按键 I）
    /// </summary>
    public class PullOverRequestedEvent 
    { 
        public bool IsAiming { get; }

        public PullOverRequestedEvent(bool isAiming = false)
        {
            IsAiming = isAiming;
        }
    }
}