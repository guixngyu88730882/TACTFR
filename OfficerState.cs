namespace EF.PoliceMod.Core
{
    public enum OfficerState
    {
        Idle,               // 未值勤
        OnDuty,             // 值勤中
        TargetLocked,       // 已锁定嫌疑人
        SuspectRestrained,  // 嫌疑人已上铐
        Escorting,          // 护送中
        Delivering          // 交付中
    }
}
