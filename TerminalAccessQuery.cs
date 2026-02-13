using EF.PoliceMod.Core;

namespace EF.PoliceMod.Systems
{
    public static class TerminalAccessQuery
    {
        public static bool CanOpenTerminal { get; private set; }

        static TerminalAccessQuery()
        {
            CanOpenTerminal = false;
            EventBus.Subscribe<TerminalAccessChangedEvent>(e => CanOpenTerminal = e.CanOpen);

            EventBus.Subscribe<DutyEndedEvent>(_ => CanOpenTerminal = false);

            EventBus.Subscribe<CaseEndedEvent>(_ =>
            {
                CanOpenTerminal = true;
            });
        }
    }
}
