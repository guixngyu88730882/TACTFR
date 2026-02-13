using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;
using EF.PoliceMod.Data;
using EF.PoliceMod.Core;
using System.Runtime.InteropServices;

namespace EF.PoliceMod.Systems
{
    public class PoliceTerminalUI
    {
        readonly WantedRegistry _wantedRegistry;
        readonly Action<int, int> _onSelected;
        readonly Action _onClosed;
        List<WantedRecord> _list = new List<WantedRecord>();
        int _index = 0;
        bool _open = false;

        // 纹理字典 / 名称（你要把图片打包成 texture dict，或用 Scaleform）
        const string BG_TEXTURE_DICT = "efpd_ui";
        const string BG_TEXTURE_NAME = "terminal_bg";

        bool _textureRequested = false;
        private bool _num8Held = false, _num2Held = false, _num5Held = false, _num0Held = false;
        private bool _num9Held = false;
        private int _uiInputDebounceMs = 150; // 灵敏度：150ms (你要更慢就改为 200-300)
        private int _lastUiInputTime = 0;

        private const int KEY_PRESSED_MASK = 0x8000;
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        private bool IsRawKeyDown(System.Windows.Forms.Keys k)
        {
            return (GetAsyncKeyState((int)k) & KEY_PRESSED_MASK) != 0;
        }

        public PoliceTerminalUI(WantedRegistry registry, Action<int, int> onSelected, Action onClosed)
        {
            _wantedRegistry = registry;
            _onSelected = onSelected;
            _onClosed = onClosed;
        }

        public void Open()
        {
            // 终端：案件模板列表（地点 + 形态）
            // 用 WantedRecord 作为 UI 展示载体，PedHandle 字段改为“OptionId”。
            // 右侧详情：显示“嫌疑人姓名 + 罪行”（仅 UI 文案，不影响实际生成逻辑）。

            var names = new[] { "杰克", "亚瑟", "汤米", "尼克", "迈卡", "唐纳德" };
            var crimesOnFoot = new[] { "盗窃", "斗殴", "扰乱治安", "非法入侵" };
            var crimesVehicle = new[] { "拒检逃逸", "危险驾驶", "肇事逃逸" };
            var rnd = new Random(Game.GameTime);

            int RollStars(int optionId)
            {
                // 规则：1~2 普通；3~4 中危；5 高危。
                // 步行案件偏“低到中”；驾车逃逸偏“中到高”。
                if (optionId >= 3)
                {
                    int r2 = rnd.Next(100);
                    if (r2 < 10) return 5;
                    if (r2 < 60) return (rnd.Next(2) == 0) ? 3 : 4;
                    return (rnd.Next(2) == 0) ? 2 : 3;
                }
                else
                {
                    int r2 = rnd.Next(100);
                    if (r2 < 10) return (rnd.Next(2) == 0) ? 4 : 5;
                    if (r2 < 55) return (rnd.Next(2) == 0) ? 2 : 3;
                    return (rnd.Next(2) == 0) ? 1 : 2;
                }
            }

            _list = new List<WantedRecord>
            {
                new WantedRecord { PedHandle = 90, DisplayName = "切换：巡逻模式(自由行动)", Stars = 0, LastSeen = "-", Reason = "系统", SuspectName = "-", Crime = "-", Location = "-" },
                new WantedRecord { PedHandle = 0, DisplayName = "市区 · 步行嫌疑人 · 单人", Stars = RollStars(0), LastSeen = "-", Reason = "接警", SuspectName = names[rnd.Next(names.Length)], Crime = crimesOnFoot[rnd.Next(crimesOnFoot.Length)], Location = "市区" },
                new WantedRecord { PedHandle = 1, DisplayName = "郊区 · 步行嫌疑人 · 单人", Stars = RollStars(1), LastSeen = "-", Reason = "接警", SuspectName = names[rnd.Next(names.Length)], Crime = crimesOnFoot[rnd.Next(crimesOnFoot.Length)], Location = "郊区" },
                new WantedRecord { PedHandle = 2, DisplayName = "偏远 · 步行嫌疑人 · 单人", Stars = RollStars(2), LastSeen = "-", Reason = "接警", SuspectName = names[rnd.Next(names.Length)], Crime = crimesOnFoot[rnd.Next(crimesOnFoot.Length)], Location = "偏远" },
                new WantedRecord { PedHandle = 3, DisplayName = "市区 · 驾车逃逸 · 单人", Stars = RollStars(3), LastSeen = "-", Reason = "追车", SuspectName = names[rnd.Next(names.Length)], Crime = crimesVehicle[rnd.Next(crimesVehicle.Length)], Location = "市区" },
                new WantedRecord { PedHandle = 4, DisplayName = "郊区 · 驾车逃逸 · 单人", Stars = RollStars(4), LastSeen = "-", Reason = "追车", SuspectName = names[rnd.Next(names.Length)], Crime = crimesVehicle[rnd.Next(crimesVehicle.Length)], Location = "郊区" },
                new WantedRecord { PedHandle = 5, DisplayName = "偏远 · 驾车逃逸 · 单人", Stars = RollStars(5), LastSeen = "-", Reason = "追车", SuspectName = names[rnd.Next(names.Length)], Crime = crimesVehicle[rnd.Next(crimesVehicle.Length)], Location = "偏远" },
                new WantedRecord { PedHandle = 10, DisplayName = "市区 · 步行嫌疑人 · 双人", Stars = Math.Max(4, RollStars(0)), LastSeen = "-", Reason = "接警", SuspectName = names[rnd.Next(names.Length)] + " & " + names[rnd.Next(names.Length)], Crime = crimesOnFoot[rnd.Next(crimesOnFoot.Length)], Location = "市区" },
                new WantedRecord { PedHandle = 11, DisplayName = "郊区 · 步行嫌疑人 · 双人", Stars = Math.Max(4, RollStars(1)), LastSeen = "-", Reason = "接警", SuspectName = names[rnd.Next(names.Length)] + " & " + names[rnd.Next(names.Length)], Crime = crimesOnFoot[rnd.Next(crimesOnFoot.Length)], Location = "郊区" },
                new WantedRecord { PedHandle = 12, DisplayName = "偏远 · 步行嫌疑人 · 双人", Stars = Math.Max(4, RollStars(2)), LastSeen = "-", Reason = "接警", SuspectName = names[rnd.Next(names.Length)] + " & " + names[rnd.Next(names.Length)], Crime = crimesOnFoot[rnd.Next(crimesOnFoot.Length)], Location = "偏远" },
                new WantedRecord { PedHandle = 13, DisplayName = "市区 · 驾车逃逸 · 双人", Stars = 5, LastSeen = "-", Reason = "追车", SuspectName = names[rnd.Next(names.Length)] + " & " + names[rnd.Next(names.Length)], Crime = crimesVehicle[rnd.Next(crimesVehicle.Length)], Location = "市区" },
                new WantedRecord { PedHandle = 14, DisplayName = "郊区 · 驾车逃逸 · 双人", Stars = 5, LastSeen = "-", Reason = "追车", SuspectName = names[rnd.Next(names.Length)] + " & " + names[rnd.Next(names.Length)], Crime = crimesVehicle[rnd.Next(crimesVehicle.Length)], Location = "郊区" },
                new WantedRecord { PedHandle = 15, DisplayName = "偏远 · 驾车逃逸 · 双人", Stars = 5, LastSeen = "-", Reason = "追车", SuspectName = names[rnd.Next(names.Length)] + " & " + names[rnd.Next(names.Length)], Crime = crimesVehicle[rnd.Next(crimesVehicle.Length)], Location = "偏远" },
            };

            _index = 0;
            _open = true;

            // 请求纹理（如果你已经把纹理字典放好，这会尽量加载它）
            RequestTextureDict();
        }

        public void Close()
        {
            _open = false;
            _num8Held = _num2Held = _num5Held = _num0Held = _num9Held = false;
            _lastUiInputTime = 0;
        }

        public void Update()
        {
            if (!_open) return;

            // 绘制背景（尝试纹理，否则画纯色背景）
            if (HasTexture())
            {
                Function.Call(Hash.DRAW_SPRITE, BG_TEXTURE_DICT, BG_TEXTURE_NAME, 0.5f, 0.5f, 1.0f, 1.0f, 0.0f, 255, 255, 255, 255);
            }
            else
            {
                // 纯色背景降级（不会抛错）
                DrawRect(0.5f, 0.5f, 1.0f, 1.0f, 0, 36, 77, 220);
            }

            // 列表绘制（左侧）
            float x = 0.12f, y = 0.16f;
            for (int i = 0; i < _list.Count && i < 8; i++)
            {
                var rec = _list[i];
                float yy = y + i * 0.06f;
                bool active = (i == _index);

                string stars = new string('★', Math.Max(0, Math.Min(5, rec.Stars))) + new string('☆', Math.Max(0, 5 - Math.Max(0, Math.Min(5, rec.Stars))));
                var c = GetRiskColor(rec.Stars);
                DrawText2D(rec.DisplayName + $"  {stars}  [{rec.LastSeen}]", x, yy, active ? 0.45f : 0.37f, active, false, c.r, c.g, c.b, 255);
            }

            // 右侧详情
            var cur = _list[_index];
            DrawText2D("详情:", 0.55f, 0.16f, 0.40f, true);
            DrawText2D($"姓名: {(string.IsNullOrEmpty(cur.SuspectName) ? "未知" : cur.SuspectName)}", 0.55f, 0.20f, 0.35f, false);
            DrawText2D($"罪行: {(string.IsNullOrEmpty(cur.Crime) ? "-" : cur.Crime)}", 0.55f, 0.24f, 0.35f, false);
            DrawText2D($"地点: {(string.IsNullOrEmpty(cur.Location) ? "-" : cur.Location)}", 0.55f, 0.28f, 0.35f, false);

            string riskText = (cur.Stars >= 5) ? "高危" : (cur.Stars >= 3 ? "中危" : "普通");
            var rc = GetRiskColor(cur.Stars);
            DrawText2D($"风险: {riskText}  ({cur.Stars}/5)", 0.55f, 0.32f, 0.35f, true, false, rc.r, rc.g, rc.b, 255);
            DrawText2D($"类型: {cur.DisplayName}", 0.55f, 0.36f, 0.35f, false);

            // 底部提示
            DrawText2D("[Num8]上  [Num2]下  [Num5]确认  [Num9]刷新  [Num0]退出", 0.5f, 0.92f, 0.30f, false, alignCenter: true);

            // ===== UI 按键去抖与灵敏度控制 =====
            // 在类字段区请确保存在这四个字段（如果不存在请在类顶部添加）：
            // private bool _num8Held=false, _num2Held=false, _num5Held=false, _num0Held=false;
            // private int _uiInputDebounceMs = 150;
            // private int _lastUiInputTime = 0;

            int now = Game.GameTime;

            // NumPad8 上（使用 Raw key，避免被 SET_PLAYER_CONTROL / DISABLE_ALL_CONTROL_ACTIONS 吞掉）
            if (IsRawKeyDown(System.Windows.Forms.Keys.NumPad8))
            {
                if (!_num8Held && now - _lastUiInputTime >= _uiInputDebounceMs)
                {
                    _index = Math.Max(0, _index - 1);
                    _num8Held = true;
                    _lastUiInputTime = now;
#if DEBUG
                    ModLog.Info("[TerminalUI] Cursor Up -> index=" + _index);
#endif

                }
            }
            else
            {
                _num8Held = false;
            }

            // NumPad2 下
            if (IsRawKeyDown(System.Windows.Forms.Keys.NumPad2))
            {
                if (!_num2Held && now - _lastUiInputTime >= _uiInputDebounceMs)
                {
                    _index = Math.Min(_list.Count - 1, _index + 1);
                    _num2Held = true;
                    _lastUiInputTime = now;
#if DEBUG
                    ModLog.Info("[TerminalUI] Cursor Down -> index=" + _index);
#endif

                }
            }
            else
            {
                _num2Held = false;
            }

            // NumPad5 确认
            if (IsRawKeyDown(System.Windows.Forms.Keys.NumPad5))
            {
                if (!_num5Held && now - _lastUiInputTime >= _uiInputDebounceMs)
                {
                    _num5Held = true;
                    _lastUiInputTime = now;
                    var rec = _list[_index];

                    // 案件模板：传 OptionId + Stars
                    int optionId = rec.PedHandle;
                    int stars = rec.Stars;
                    ModLog.Info($"[TerminalUI] Confirm selected index={_index}, optionId={optionId}, stars={stars}");
                    _onSelected?.Invoke(optionId, stars);
                }
            }
            else
            {
                _num5Held = false;
            }


            // NumPad0 退出
            if (IsRawKeyDown(System.Windows.Forms.Keys.NumPad0))
            {
                if (!_num0Held && now - _lastUiInputTime >= _uiInputDebounceMs)
                {
                    _num0Held = true;
                    _lastUiInputTime = now;
#if DEBUG
                    ModLog.Info("[TerminalUI] Exit requested");
#endif

                    _onClosed?.Invoke();
                }
            }
            else
            {
                _num0Held = false;
            }

            // NumPad9 刷新案件列表（不想玩的案件可刷新）
            if (IsRawKeyDown(System.Windows.Forms.Keys.NumPad9))
            {
                if (!_num9Held && now - _lastUiInputTime >= _uiInputDebounceMs)
                {
                    _num9Held = true;
                    _lastUiInputTime = now;

                    try
                    {
                        Open(); // 复用 Open() 内的随机生成逻辑
                    }
                    catch { }
                }
            }
            else
            {
                _num9Held = false;
            }

        }

        void RequestTextureDict()
        {
            if (_textureRequested) return;
            _textureRequested = true;
            Function.Call(Hash.REQUEST_STREAMED_TEXTURE_DICT, BG_TEXTURE_DICT, true);
        }

        bool HasTexture()
        {
            try
            {
                return Function.Call<bool>(Hash.HAS_STREAMED_TEXTURE_DICT_LOADED, BG_TEXTURE_DICT);
            }
            catch { return false; }
        }

        void DrawRect(float x, float y, float width, float height, int r, int g, int b, int a)
        {
            Function.Call(Hash.DRAW_RECT, x, y, width, height, r, g, b, a);
        }

        private (int r, int g, int b) GetRiskColor(int stars)
        {
            if (stars >= 5) return (220, 40, 40);     // 红：高危
            if (stars >= 3) return (255, 160, 0);     // 橙：中危
            return (255, 255, 255);                   // 白：普通
        }

        void DrawText2D(string text, float x, float y, float scale = 0.4f, bool bold = false, bool alignCenter = false, int r = 255, int g = 255, int b = 255, int a = 255)
        {
            Function.Call(Hash.SET_TEXT_FONT, 0);
            Function.Call(Hash.SET_TEXT_SCALE, scale, scale);
            Function.Call(Hash.SET_TEXT_WRAP, 0.0f, 1.0f);
            if (alignCenter)
                Function.Call(Hash.SET_TEXT_CENTRE, true);
            else
                Function.Call(Hash.SET_TEXT_CENTRE, false);

            Function.Call(Hash.SET_TEXT_COLOUR, r, g, b, a);

            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, x, y);
        }
    }
}
