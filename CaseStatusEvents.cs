namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 案件状态变化事件：由 CaseManager 发布，系统侧只读订阅（解耦）。
    /// </summary>
    public struct CaseStatusChangedEvent
    {
        public bool HasActiveCase { get; }
        public bool IsSuspectLost { get; }
        public int CaseStartedAtMs { get; }
        public bool HeliReconUsed { get; }
        public int CurrentSuspectHandle { get; }

        public CaseStatusChangedEvent(
            bool hasActiveCase,
            bool isSuspectLost,
            int caseStartedAtMs,
            bool heliReconUsed,
            int currentSuspectHandle)
        {
            HasActiveCase = hasActiveCase;
            IsSuspectLost = isSuspectLost;
            CaseStartedAtMs = caseStartedAtMs;
            HeliReconUsed = heliReconUsed;
            CurrentSuspectHandle = currentSuspectHandle;
        }
    }
}
