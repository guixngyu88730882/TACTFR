using EF.PoliceMod.Core;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 低耦合的“执勤状态查询”静态门面：
    /// 任何地方都不要再去摸 EFCore.Instance/CaseManager 只为判断 IsOnDuty。
    /// </summary>
    public static class DutyQuery
    {
        public static bool IsOnDuty { get; private set; }

        static DutyQuery()
        {
            IsOnDuty = false;
            EventBus.Subscribe<DutyStartedEvent>(_ => IsOnDuty = true);
            EventBus.Subscribe<DutyEndedEvent>(_ => IsOnDuty = false);
        }
    }
}
