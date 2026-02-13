public enum SuspectState
{
    None,
    Restrained,

    Escorting,          // 步行押送
    EnteringVehicle,    // ★ 上车中（禁止任何跟随 Tick）
    InVehicle,          // ★ 已在车内
    ExitingVehicle,     // ★ 下车中

    Fleeing,
    Resisting
}
