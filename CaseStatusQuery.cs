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

        static CaseStatusQuery()
        {
            Reset();
            EventBus.Subscribe<CaseStatusChangedEvent>(OnChanged);
            EventBus.Subscribe<DutyEndedEvent>(_ => Reset());
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

        private static void Reset()
        {
            HasActiveCase = false;
            IsSuspectLost = false;
            CaseStartedAtMs = 0;
            HeliReconUsed = false;
            CurrentSuspectHandle = -1;
        }
    }
}
