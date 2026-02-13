using EF.PoliceMod.Core;
using EF.PoliceMod.Input;
using GTA;
using GTA.Native;
using System;
using Keys = System.Windows.Forms.Keys;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// H 键拘捕动作菜单：
    /// 1) 铐起嫌疑人并牵着走
    /// 2) 要求嫌疑人抱头自己跟随
    /// 小键盘 8/2 选择，5 确认；Backspace 取消。
    /// </summary>
    public class ArrestMenuController
    {
        private bool _open;

        private int _selected = 0;
        private readonly string[] _items = new[]
        {
            "1. 铐起嫌疑人拉着他",
            "2. 抱头跟随",
        };

        private bool _upHeld;
        private bool _downHeld;
        private bool _confirmHeld;
        private bool _closeHeld;

        public ArrestMenuController()
        {
            EventBus.Subscribe<OpenArrestActionMenuEvent>(OnOpenRequested);
            EventBus.Subscribe<DutyEndedEvent>(_ => { try { Close(); } catch { } });
            EventBus.Subscribe<EF.PoliceMod.Core.SuspectDeliveredEvent>(_ => { try { Close(); } catch { } });
        }

        private void OnOpenRequested(OpenArrestActionMenuEvent e)
        {
            if (_open) return;
            Open();
        }

        private void Open()
        {
            _open = true;
            UIState.MarkArrestMenuOpen(Game.GameTime);
            _selected = 0;

            try { Function.Call(Hash.CLEAR_HELP, true); } catch { }
        }

        private void Close()
        {
            _open = false;
            UIState.MarkArrestMenuClosed();

            _upHeld = false;
            _downHeld = false;
            _confirmHeld = false;
            _closeHeld = false;
        }

        private void DrawMenu()
        {
            // 右上角（放在调度菜单下方一点）
            float panelW = 0.30f;
            float rowH = 0.032f;
            float headerH = 0.038f;
            float panelH = headerH + (_items.Length * rowH) + 0.01f;
            float panelX = 0.83f;
            float panelY = 0.32f;

            try { Function.Call(Hash.DRAW_RECT, panelX, panelY, panelW, panelH, 0, 0, 0, 190); } catch { }

            // 标题
            try
            {
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 0.34f, 0.34f);
                Function.Call(Hash.SET_TEXT_CENTRE, true);
                Function.Call(Hash.SET_TEXT_COLOUR, 255, 220, 80, 255);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, "拘捕动作");
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, panelX, panelY - panelH / 2 + 0.01f);
            }
            catch { }

            for (int i = 0; i < _items.Length; i++)
            {
                float rowY = (panelY - panelH / 2 + headerH) + (i * rowH) + (rowH / 2);

                if (i == _selected)
                {
                    try { Function.Call(Hash.DRAW_RECT, panelX, rowY, panelW - 0.01f, rowH, 255, 255, 255, 35); } catch { }
                }

                try
                {
                    Function.Call(Hash.SET_TEXT_FONT, 0);
                    Function.Call(Hash.SET_TEXT_SCALE, 0.31f, 0.31f);
                    Function.Call(Hash.SET_TEXT_CENTRE, false);
                    Function.Call(Hash.SET_TEXT_COLOUR, 255, 255, 255, 255);
                    Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                    Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, (i == _selected ? "> " : "  ") + _items[i]);
                    Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, panelX - panelW / 2 + 0.01f, rowY - 0.012f);
                }
                catch { }
            }

            try
            {
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 0.26f, 0.26f);
                Function.Call(Hash.SET_TEXT_CENTRE, true);
                Function.Call(Hash.SET_TEXT_COLOUR, 200, 200, 200, 255);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, "小键盘8/2选择 5确认  Back取消");
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, panelX, panelY + panelH / 2 - 0.025f);
            }
            catch { }
        }

        private void ExecuteSelected()
        {
            if (_selected == 0)
                ArrestStyleState.SelectedStyle = ArrestActionStyle.CuffAndLead;
            else
                ArrestStyleState.SelectedStyle = ArrestActionStyle.HandsOnHeadFollow;

            EventBus.Publish(new AttemptArrestEvent());
        }

        public void Tick()
        {
            if (!_open) return;

            UIState.BeatArrestMenu(Game.GameTime);
            DrawMenu();

            bool close = Game.IsKeyPressed(Keys.Back);
            if (close)
            {
                if (!_closeHeld)
                {
                    _closeHeld = true;
                    Close();
                }
                return;
            }
            _closeHeld = false;

            bool up = Game.IsKeyPressed(Keys.NumPad8);
            bool down = Game.IsKeyPressed(Keys.NumPad2);
            bool confirm = Game.IsKeyPressed(Keys.NumPad5);

            if (up)
            {
                if (!_upHeld)
                {
                    _selected--;
                    if (_selected < 0) _selected = _items.Length - 1;
                }
                _upHeld = true;
            }
            else _upHeld = false;

            if (down)
            {
                if (!_downHeld)
                {
                    _selected++;
                    if (_selected >= _items.Length) _selected = 0;
                }
                _downHeld = true;
            }
            else _downHeld = false;

            if (confirm)
            {
                if (!_confirmHeld)
                {
                    ExecuteSelected();
                    Close();
                }
                _confirmHeld = true;
            }
            else _confirmHeld = false;
        }

        public void Shutdown()
        {
            try { EventBus.Unsubscribe<OpenArrestActionMenuEvent>(OnOpenRequested); } catch { }
            try { Close(); } catch { }
        }
    }
}