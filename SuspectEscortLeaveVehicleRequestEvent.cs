using GTA;

namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 请求：嫌疑人下车（由行为层处理）
    /// </summary>
    public class SuspectEscortLeaveVehicleRequestEvent
    {
        public Ped Player { get; }

        public SuspectEscortLeaveVehicleRequestEvent(Ped player)
        {
            Player = player;
        }
    }
}
