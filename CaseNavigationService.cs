using EF.PoliceMod.Core;
using GTA;
using GTA.Math;
using GTA.Native;
using System;

namespace EF.PoliceMod.Systems
{
    public static class CaseNavigationService
    {
        private static Blip _deliveryRouteBlip = null;
        private static int _lastRouteCreatedAt = 0;
        private static int _lastRouteSuspectHandle = -1;
        private static readonly Vector3 _policeStationPos = new Vector3(443.5f, -981.0f, 30.7f);

        public static void SetDeliveryRoute(int suspectHandle)
        {
            int now = Game.GameTime;
            // 防抖：如果同一嫌疑人、30秒内已处理过，直接跳过
            if (suspectHandle == _lastRouteSuspectHandle && now - _lastRouteCreatedAt < 30000)
            {
                ModLog.Info("[CaseNavigationService] SetDeliveryRoute skipped: same suspect, recent route (debounced)");
                return;
            }

            try
            {
                if (_deliveryRouteBlip != null)
                {
                    try { _deliveryRouteBlip.ShowRoute = false; _deliveryRouteBlip.Delete(); } catch { }
                    _deliveryRouteBlip = null;
                }

                _deliveryRouteBlip = World.CreateBlip(_policeStationPos);
                _deliveryRouteBlip.Sprite = BlipSprite.PoliceStation;
                _deliveryRouteBlip.Color = BlipColor.Blue;
                _deliveryRouteBlip.IsShortRange = false;
                _deliveryRouteBlip.Name = "交付地点：警局";

                bool routeSet = false;
                try
                {
                    _deliveryRouteBlip.ShowRoute = true;
                    routeSet = true;
                    ModLog.Info("[CaseNavigationService] Delivery blip created and ShowRoute=true");
                }
                catch (Exception ex)
                {
                    ModLog.Warn("[CaseNavigationService] ShowRoute failed: " + ex);
                }

                try
                {
                    Function.Call(Hash.SET_NEW_WAYPOINT, _policeStationPos.X, _policeStationPos.Y);
                    if (!routeSet) ModLog.Info("[CaseNavigationService] Fallback: SET_NEW_WAYPOINT called");
                }
                catch (Exception ex)
                {
                    ModLog.Warn("[CaseNavigationService] SET_NEW_WAYPOINT failed: " + ex);
                }

                _lastRouteCreatedAt = Game.GameTime;
                _lastRouteSuspectHandle = suspectHandle;

                SmsNotification.Show("911调度", "押送指令", "嫌疑人已上车，导航已设置：返回警局交付");
            }
            catch (Exception ex)
            {
                ModLog.Error("[CaseNavigationService] SetDeliveryRoute error: " + ex);
            }
        }

        public static void ClearDeliveryRoute()
        {
            if (_deliveryRouteBlip != null)
            {
                try { _deliveryRouteBlip.ShowRoute = false; _deliveryRouteBlip.Delete(); } catch { }
                _deliveryRouteBlip = null;
            }
            try { Function.Call(Hash.CLEAR_GPS_PLAYER_WAYPOINT); } catch { }
            try { Function.Call(Hash.SET_WAYPOINT_OFF); } catch { }
            
            _lastRouteCreatedAt = 0;
            _lastRouteSuspectHandle = -1;
            ModLog.Info("[CaseNavigationService] Delivery route cleared");
        }
    }
}
