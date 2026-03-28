using GTA;
using GTA.Math;

namespace EF.PoliceMod.Systems
{
    public class PullOverState
    {
        public int ActiveDriverHandle { get; set; } = -1;
        public int ActiveVehicleHandle { get; set; } = -1;
        public int ParkIssuedAtMs { get; set; } = 0;
        public int LastHaltReissueAtMs { get; set; } = 0;
        public int PulledOverReadyAtMs { get; set; } = 0;
        public bool PulledOverReady { get; set; } = false;
        public bool ExitRequested { get; set; } = false;
        public int ExitIssuedAtMs { get; set; } = 0;
        public Vector3 ParkTargetPos { get; set; }
        public float ParkTargetHeading { get; set; }
        public bool ExitWillResist { get; set; } = false;
        public string RiskTag { get; set; } = "";

        public bool IsActive => ActiveDriverHandle > 0 && ActiveVehicleHandle > 0;

        public void Clear()
        {
            ActiveDriverHandle = -1;
            ActiveVehicleHandle = -1;
            ParkIssuedAtMs = 0;
            LastHaltReissueAtMs = 0;
            PulledOverReadyAtMs = 0;
            PulledOverReady = false;
            ExitRequested = false;
            ExitIssuedAtMs = 0;
            ExitWillResist = false;
            RiskTag = "";
        }
    }
}
