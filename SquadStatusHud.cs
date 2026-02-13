using GTA;
using GTA.Native;
using GTA.Math;
using System;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 右侧状态栏（简化 LSPDFR 风格）：
    /// - 当前掌控警员数
    /// - 当前位置（街道/区域）
    /// - 上车后显示车牌
    /// </summary>
    public class SquadStatusHud
    {
        private readonly OfficerSquadSystem _squad;

        public SquadStatusHud(OfficerSquadSystem squad)
        {
            _squad = squad;
        }

        public void Tick()
        {
            try { Draw(); } catch { }
        }

        private void Draw()
        {
            var player = Game.Player.Character;
            if (player == null || !player.Exists()) return;

            string plate = "";
            try
            {
                if (player.IsInVehicle())
                {
                    var v = player.CurrentVehicle;
                    if (v != null && v.Exists())
                    {
                        try { plate = Function.Call<string>(Hash.GET_VEHICLE_NUMBER_PLATE_TEXT, v.Handle); } catch { plate = ""; }
                    }
                }
            }
            catch { plate = ""; }

            string loc = GetLocationText(player);

            // 放在小地图右侧（更贴近小地图右边缘）
            float x = 0.37f;
            float y = 0.86f;
            float w = 0.26f;
            float h = 0.13f;

            // 去掉不透明背景：只留文字

            string text = $"~b~小队~s~: {_squad?.OfficerCount ?? 0}/2\n" +
                          $"~w~位置~s~: {loc}\n" +
                          (string.IsNullOrEmpty(plate) ? "" : $"~w~车牌~s~: {plate}");

            try
            {
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 0.30f, 0.30f);
                Function.Call(Hash.SET_TEXT_CENTRE, false);
                Function.Call(Hash.SET_TEXT_COLOUR, 255, 255, 255, 230);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, x - w / 2 + 0.01f, y - h / 2 + 0.012f);
            }
            catch { }
        }

        private string GetLocationText(Ped player)
        {
            if (player == null || !player.Exists()) return "";

            Vector3 pos = player.Position;
            string street = "";
            string zone = "";

            try
            {
                var streetHash = new OutputArgument();
                var crossingHash = new OutputArgument();
                Function.Call(Hash.GET_STREET_NAME_AT_COORD, pos.X, pos.Y, pos.Z, streetHash, crossingHash);

                int sh = streetHash.GetResult<int>();
                if (sh != 0)
                    street = Function.Call<string>(Hash.GET_STREET_NAME_FROM_HASH_KEY, sh);
            }
            catch { street = ""; }

            try
            {
                // 增强版 Hash 里可能没有 GET_LABEL_TEXT：这里直接显示 zoneName（例如 DAVIS / PBOX 等）
                string zoneName = Function.Call<string>(Hash.GET_NAME_OF_ZONE, pos.X, pos.Y, pos.Z);
                zone = string.IsNullOrEmpty(zoneName) ? "" : zoneName;
            }
            catch { zone = ""; }

            if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(zone)) return street + " / " + zone;
            if (!string.IsNullOrEmpty(street)) return street;
            if (!string.IsNullOrEmpty(zone)) return zone;

            // 降级：实在取不到就回到坐标
            return $"{pos.X:0},{pos.Y:0}";
        }
    }
}