using EF.PoliceMod.Core.Contracts.Snapshots;

namespace EF.PoliceMod.Core.Contracts
{
    public interface IPatrolService : ISystem
    {
        PatrolSnapshot GetSnapshot();
        bool IsEnabled { get; }
        void Enable();
        void Disable();
        void Toggle();
        void RequestCheck();
        void RequestIdCheck();
        void RequestSearch();
    }
}
