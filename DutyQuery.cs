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
        private static bool _subscribed;
        private static readonly System.Action<DutyStartedEvent> _onDutyStarted = OnDutyStarted;
        private static readonly System.Action<DutyEndedEvent> _onDutyEnded = OnDutyEnded;

        static DutyQuery()
        {
            Reset();
            Subscribe();
        }

        public static void Subscribe()
        {
            if (_subscribed) return;
            EventBus.Subscribe<DutyStartedEvent>(_onDutyStarted);
            EventBus.Subscribe<DutyEndedEvent>(_onDutyEnded);
            _subscribed = true;
        }

        public static void Unsubscribe()
        {
            if (!_subscribed) return;
            EventBus.Unsubscribe<DutyStartedEvent>(_onDutyStarted);
            EventBus.Unsubscribe<DutyEndedEvent>(_onDutyEnded);
            _subscribed = false;
        }

        public static void Reset()
        {
            IsOnDuty = false;
        }

        private static void OnDutyStarted(DutyStartedEvent e)
        {
            IsOnDuty = true;
        }

        private static void OnDutyEnded(DutyEndedEvent e)
        {
            IsOnDuty = false;
        }
    }
}
