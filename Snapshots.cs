using GTA.Math;

namespace EF.PoliceMod.Core.Contracts.Snapshots
{
    public readonly struct DutySnapshot
    {
        public bool IsOnDuty { get; }
        public int CaseCount { get; }
        public int GameTimeStarted { get; }

        public DutySnapshot(bool isOnDuty, int caseCount, int gameTimeStarted)
        {
            IsOnDuty = isOnDuty;
            CaseCount = caseCount;
            GameTimeStarted = gameTimeStarted;
        }
    }

    public readonly struct CaseSnapshot
    {
        public bool HasActiveCase { get; }
        public int SuspectHandle { get; }
        public Vector3 SuspectLastKnownPos { get; }
        public bool IsSuspectLost { get; }
        public int CaseStartTime { get; }
        public int StarRating { get; }

        public CaseSnapshot(
            bool hasActiveCase,
            int suspectHandle,
            Vector3 suspectLastKnownPos,
            bool isSuspectLost,
            int caseStartTime,
            int starRating)
        {
            HasActiveCase = hasActiveCase;
            SuspectHandle = suspectHandle;
            SuspectLastKnownPos = suspectLastKnownPos;
            IsSuspectLost = isSuspectLost;
            CaseStartTime = caseStartTime;
            StarRating = starRating;
        }
    }

    public readonly struct SuspectSnapshot
    {
        public int Handle { get; }
        public bool Exists { get; }
        public bool IsCompliant { get; }
        public bool IsResisting { get; }
        public bool IsInVehicle { get; }
        public Vector3 Position { get; }

        public SuspectSnapshot(
            int handle,
            bool exists,
            bool isCompliant,
            bool isResisting,
            bool isInVehicle,
            Vector3 position)
        {
            Handle = handle;
            Exists = exists;
            IsCompliant = isCompliant;
            IsResisting = isResisting;
            IsInVehicle = isInVehicle;
            Position = position;
        }
    }

    public readonly struct PatrolSnapshot
    {
        public bool IsEnabled { get; }
        public int ChecksCompleted { get; }

        public PatrolSnapshot(bool isEnabled, int checksCompleted)
        {
            IsEnabled = isEnabled;
            ChecksCompleted = checksCompleted;
        }
    }

    public readonly struct OfficerSquadSnapshot
    {
        public int OfficerCount { get; }
        public bool ArrestMode { get; }
        public bool NonLethalMode { get; }

        public OfficerSquadSnapshot(int officerCount, bool arrestMode, bool nonLethalMode)
        {
            OfficerCount = officerCount;
            ArrestMode = arrestMode;
            NonLethalMode = nonLethalMode;
        }
    }
}
