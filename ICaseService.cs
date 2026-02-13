using EF.PoliceMod.Core.Contracts.Snapshots;

namespace EF.PoliceMod.Core.Contracts
{
    public interface ICaseService : ISystem
    {
        CaseSnapshot GetSnapshot();
        bool HasActiveCase { get; }
        bool CanAcceptNewCase { get; }
        void StartCase();
        void EndCase(CaseEndReason reason);
    }

    public enum CaseEndReason
    {
        Completed,
        Failed,
        Cancelled,
        SuspectDead,
        Timeout,
        OffDuty
    }
}
