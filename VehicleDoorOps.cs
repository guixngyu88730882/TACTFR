using GTA;
using GTA.Native;

namespace EF.PoliceMod.Executors
{
    internal static class VehicleDoorOps
    {
        public static void OpenDoor(Vehicle veh, int doorIndex)
        {
            if (veh == null || !veh.Exists()) return;
            try { Function.Call(Hash.SET_VEHICLE_DOOR_OPEN, veh.Handle, doorIndex, false, false); } catch { }
        }

        public static void ShutDoor(Vehicle veh, int doorIndex)
        {
            if (veh == null || !veh.Exists()) return;
            try { Function.Call(Hash.SET_VEHICLE_DOOR_SHUT, veh.Handle, doorIndex, false); } catch { }
        }
    }
}