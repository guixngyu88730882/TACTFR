using EF.PoliceMod.Core;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 执勤状态的只读门面（解耦 Input/UI 与 CaseManager/EFCore.Instance 的直接依赖）。
    /// 规则：DutyStarted=true，DutyEnded=false。
    /// </summary>
    public sealed class DutyStateService
    {
        public bool IsOnDuty { get; private set; }

        public DutyStateService()
        {
            IsOnDuty = false;
            EventBus.Subscribe<DutyStartedEvent>(_ => IsOnDuty = true);
            EventBus.Subscribe<DutyEndedEvent>(_ => IsOnDuty = false);
        }
    }
}
