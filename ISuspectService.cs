using EF.PoliceMod.Core.Contracts.Snapshots;
using GTA;

namespace EF.PoliceMod.Core.Contracts
{
    public interface ISuspectService : ISystem
    {
        SuspectSnapshot GetSnapshot();
        Ped GetCurrentSuspect();
        bool HasSuspect { get; }
        bool IsCompliant { get; }
        bool IsResisting { get; }
        void TakeControl(Ped ped);
        void ReleaseControl();
        void Arrest(Ped ped);
        void SetCompliant(Ped ped);
        void SetResisting(Ped ped);
    }
}
