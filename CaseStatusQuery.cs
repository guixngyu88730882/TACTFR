using EF.PoliceMod.Core;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 案件只读状态门面：用于把“侦察/调度”等系统从 CaseManager 直接引用中剥离。
    /// </summary>
    public static class CaseStatusQuery
    {
        public static bool HasActiveCase { get; private set; }
        public static bool IsSuspectLost { get; private set; }
        public static int CaseStartedAtMs { get; private set; }
        public static bool HeliReconUsed { get; private set; }
        public static int CurrentSuspectHandle { get; private set; }
        private static bool _subscribed;
        private static readonly System.Action<CaseStatusChangedEvent> _onChanged = OnChanged;
        private static readonly System.Action<DutyEndedEvent> _onDutyEnded = OnDutyEnded;

        static CaseStatusQuery()
        {
            Reset();
            Subscribe();
        }

        public static void Subscribe()
        {
            if (_subscribed) return;
            EventBus.Subscribe<CaseStatusChangedEvent>(_onChanged);
            EventBus.Subscribe<DutyEndedEvent>(_onDutyEnded);
            _subscribed = true;
        }

        public static void Unsubscribe()
        {
            if (!_subscribed) return;
            EventBus.Unsubscribe<CaseStatusChangedEvent>(_onChanged);
            EventBus.Unsubscribe<DutyEndedEvent>(_onDutyEnded);
            _subscribed = false;
        }

        private static void OnChanged(CaseStatusChangedEvent e)
        {
            HasActiveCase = e.HasActiveCase;
            IsSuspectLost = e.IsSuspectLost;
            CaseStartedAtMs = e.CaseStartedAtMs;
            HeliReconUsed = e.HeliReconUsed;
            CurrentSuspectHandle = e.CurrentSuspectHandle;
            ModLog.Info($"[CaseStatusQuery] Received: active={e.HasActiveCase}, lost={e.IsSuspectLost}, handle={e.CurrentSuspectHandle}");
        }

        public static void Reset()
        {
            HasActiveCase = false;
            IsSuspectLost = false;
            CaseStartedAtMs = 0;
            HeliReconUsed = false;
            CurrentSuspectHandle = -1;
        }

        private static void OnDutyEnded(DutyEndedEvent e)
        {
            Reset();
        }
    }
}
