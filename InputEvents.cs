using GTA;

namespace EF.PoliceMod.Input
{
    // 打开 911 菜单（以后用）
    public struct Open911MenuEvent { }
    public struct ToggleHelpEvent { }


    // 锁定目标（L 键）：目标由 LockTargetSystem 自行查找（避免在输入层传 Ped 引用引发不稳定）
    public struct LockTargetEvent { }
    // ====== Deprecated: Aiming-driven shortcuts are disabled project-wide ======
    // The aim->surrender / aim->resist shortcut was causing native Task concurrency and crashes.
    // We keep deprecated types as empty / obsolete placeholders so other files that still reference
    // the types compile while we remove all event publishes/subscriptions.
    [System.Obsolete("SuspectCompliantEvent is deprecated and disabled.")]
    public struct SuspectCompliantEvent { }

    // 交付嫌疑人
    public struct DeliverSuspectEvent { }

    // 尝试拘捕（菜单确认后触发）
    public struct AttemptArrestEvent { }

    // 打开拘捕动作菜单（H）
    public struct OpenArrestActionMenuEvent { }

    // 新增 —— 打开警用终端（由 InputManager 发布）
    public enum OpenPoliceTerminalSource
    {
        StationTerminal,
        VehicleTerminal
    }

    public struct OpenPoliceTerminalEvent
    {
        public OpenPoliceTerminalSource Source { get; }
        public OpenPoliceTerminalEvent(OpenPoliceTerminalSource source) { Source = source; }
    }

    // F8：打开警员小队控制菜单
    public struct OpenOfficerSquadMenuEvent { }

    // ====== Deprecated: Aiming-driven shortcuts are disabled project-wide ======
    // The aim->surrender / aim->resist shortcut was causing native Task concurrency and crashes.
    // We keep a deprecated-compatible type here but include the Target field so any publishers
    // that still set Target will compile and subscribers can read it safely.
    // Keep marked Obsolete to remind future cleanup.
    [System.Obsolete("PlayerAimedAtPedEvent is deprecated and disabled. Aim-to-skip-arrest path removed.")]
    public struct PlayerAimedAtPedEvent
    {
        // intentionally left empty to prevent passing Ped references through deprecated aim event.
    }






    // 押送正式开始（由 SuspectController 发布，SuspectStateHub / EFCore 订阅）
    public struct SuspectEscortBeginEvent
    {
        public Ped Suspect;
        public Ped Officer;
        public SuspectEscortBeginEvent(Ped suspect, Ped officer)
        {
            Suspect = suspect;
            Officer = officer;
        }
    }

    // （如果需要）补齐其它小事件的空壳：SuspectArrestedEvent / SuspectBoardedVehicleEvent / SuspectResistEvent
    // 如果你的项目已有这些定义，则可以删掉下面的注释；如果没有，保留下面的简单定义以通过编译。
    public struct SuspectArrestedEvent { public Ped Suspect; public SuspectArrestedEvent(Ped suspect) { Suspect = suspect; } }
    public struct SuspectBoardedVehicleEvent { public Ped Suspect; public SuspectBoardedVehicleEvent(Ped suspect) { Suspect = suspect; } }
    public struct SuspectResistEvent { public Ped Suspect; public Ped Attacker; public SuspectResistEvent(Ped suspect, Ped attacker) { Suspect = suspect; Attacker = attacker; } }
}
// 添加 SuspectDeliveredEvent（DeliverSystem 使用）
public struct SuspectDeliveredEvent
{
    public Ped Suspect;
    public SuspectDeliveredEvent(Ped suspect) { Suspect = suspect; }
}


// （若未来需要：可在此继续补齐其它 Input 事件，但不要与 Core 命名空间重复）


