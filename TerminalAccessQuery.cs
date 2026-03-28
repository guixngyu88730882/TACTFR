using EF.PoliceMod.Core;

namespace EF.PoliceMod.Systems
{
    public static class TerminalAccessQuery
    {
        public static bool CanOpenTerminal { get; private set; }
        private static bool _subscribed;
        private static readonly System.Action<TerminalAccessChangedEvent> _onTerminalAccessChanged = OnTerminalAccessChanged;
        private static readonly System.Action<DutyEndedEvent> _onDutyEnded = OnDutyEnded;

        static TerminalAccessQuery()
        {
            Reset();
            Subscribe();
        }

        public static void Subscribe()
        {
            if (_subscribed) return;
            EventBus.Subscribe<TerminalAccessChangedEvent>(_onTerminalAccessChanged);
            EventBus.Subscribe<DutyEndedEvent>(_onDutyEnded);
            _subscribed = true;
        }

        public static void Unsubscribe()
        {
            if (!_subscribed) return;
            EventBus.Unsubscribe<TerminalAccessChangedEvent>(_onTerminalAccessChanged);
            EventBus.Unsubscribe<DutyEndedEvent>(_onDutyEnded);
            _subscribed = false;
        }

        public static void Reset()
        {
            CanOpenTerminal = false;
        }

        private static void OnTerminalAccessChanged(TerminalAccessChangedEvent e)
        {
            CanOpenTerminal = e.CanOpen;
        }

        private static void OnDutyEnded(DutyEndedEvent e)
        {
            CanOpenTerminal = false;
        }
    }
}
