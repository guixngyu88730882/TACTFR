namespace EF.PoliceMod.Core
{
    public enum TargetState
    {
        None,       // 没有目标
        Locked,     // 已锁定
        Lost,       // 丢失（距离/死亡）
        Arrested    // 已控制（以后用）
    }
}
