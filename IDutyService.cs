using EF.PoliceMod.Core.Contracts.Snapshots;

namespace EF.PoliceMod.Core.Contracts
{
    public interface IDutyService : ISystem
    {
        DutySnapshot GetSnapshot();
        bool IsOnDuty { get; }
        void StartDuty();
        void EndDuty();
    }
}
