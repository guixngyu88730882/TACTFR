using EF.PoliceMod.Core;
using GTA;
using GTA.Math;

namespace EF.PoliceMod.Systems
{
    public static class CaseSearchAreaService
    {
        private static Blip _suspectSearchAreaBlip = null;

        public static void CreateSearchArea(Vector3 center, float radius)
        {
            ClearSearchArea();

            try
            {
                _suspectSearchAreaBlip = World.CreateBlip(center, radius);
                _suspectSearchAreaBlip.Color = BlipColor.Red;
                _suspectSearchAreaBlip.Alpha = 80;
                _suspectSearchAreaBlip.IsShortRange = false;
                _suspectSearchAreaBlip.Name = "嫌疑人可能位置";
                ModLog.Info($"[CaseSearchAreaService] Search area blip created at {center} with radius {radius}");
            }
            catch (System.Exception ex)
            {
                ModLog.Error("[CaseSearchAreaService] Failed to create search area blip: " + ex);
            }
        }

        public static void ClearSearchArea()
        {
            if (_suspectSearchAreaBlip != null)
            {
                try
                {
                    if (_suspectSearchAreaBlip.Exists())
                        _suspectSearchAreaBlip.Delete();
                }
                catch { }
                _suspectSearchAreaBlip = null;
                ModLog.Info("[CaseSearchAreaService] Search area blip cleared");
            }
        }
    }
}
