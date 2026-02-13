namespace EF.PoliceMod.Core.Contracts
{
    public interface IDispatchService : ISystem
    {
        int BackupCount { get; }
        bool CanRequestBackup { get; }
        void RequestBackup();
        void ClearAllBackup();
        void SetConvoyFollowPlayer();
        void SetConvoyFreeRoam();
    }
}
