using EF.PoliceMod.Core;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 巡逻模式只读门面：解耦 Input/UI 与 EFCore.Instance。
    /// 由 PatrolModeToggledEvent 驱动。
    /// </summary>
    public static class PatrolModeQuery
    {
        public static bool Enabled { get; private set; }

        static PatrolModeQuery()
        {
            Enabled = false;
            EventBus.Subscribe<PatrolModeToggledEvent>(e => Enabled = e.Enabled);
            EventBus.Subscribe<DutyEndedEvent>(_ => Enabled = false);
        }
    }
}
