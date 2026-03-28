using EF.PoliceMod.Core;
using GTA;
using GTA.Native;
using GTA.UI;
using System;
using System.Runtime.InteropServices;
using Keys = System.Windows.Forms.Keys;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 巡逻模式命令菜单（PatrolMenu 键打开）：小键盘 8/2 选择，5 确认，PatrolMenu/Num0 关闭
    /// P1-2 Fix: Now uses PatrolMenu key binding instead of hardcoded H
    /// </summary>
    public sealed class PatrolMenuController : EF.PoliceMod.Core.IUiSession
    {
        // P1-2 Fix: Use UIState as single source of truth
        public string SessionId => EF.PoliceMod.Core.UIState.PatrolMenuSession;
        public bool IsOpen => EF.PoliceMod.Core.UIState.IsPatrolMenuOpen;
        
        private int _selected = 0;
        private int _openedAtMs = 0;
        private int _lastToggleAtMs = 0;
        private const int ToggleDebounceMs = 250;

        private bool _upHeld = false;
        private bool _downHeld = false;
        private bool _confirmHeld = false;
        private bool _cancelHeld = false;

        private readonly string[] _items = new[]
        {
            "快速检查(概率)",
            "要求出示身份证(概率)",
            "搜查(概率)",
        };

        private const int KEY_PRESSED_MASK = 0x8000;
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private bool IsRawKeyDown(System.Windows.Forms.Keys k)
        {
            return (GetAsyncKeyState((int)k) & KEY_PRESSED_MASK) != 0;
        }

        public PatrolMenuController()
        {
            EventBus.Subscribe<PatrolMenuToggledEvent>(HandlePatrolMenuToggled);
            EventBus.Subscribe<DutyEndedEvent>(HandleDutyEnded);
        }

        private void HandlePatrolMenuToggled(PatrolMenuToggledEvent e)
        {
            if (e.Open)
            {
                bool patrolOn = EF.PoliceMod.Systems.PatrolModeQuery.Enabled;
                bool hasActiveCase = EF.PoliceMod.Systems.CaseStatusQuery.HasActiveCase;
                var core = EFCore.Instance;
                bool hasLockedTarget = core != null && core.LockTargetSystem != null && core.LockTargetSystem.HasTarget;

                if (patrolOn && hasLockedTarget && !hasActiveCase)
                {
                    Open();
                }
            }
            else
            {
                Close();
            }
        }

        private void HandleDutyEnded(DutyEndedEvent e)
        {
            Close();
        }

        public void Shutdown()
        {
            try { EventBus.Unsubscribe<PatrolMenuToggledEvent>(HandlePatrolMenuToggled); } catch { }
            try { EventBus.Unsubscribe<DutyEndedEvent>(HandleDutyEnded); } catch { }
            try { Close(); } catch { }
        }

        private void Open()
        {
            // P1-2 Fix: Check if any other UI is open
            string busyUi = EF.PoliceMod.Core.UIState.GetBusyUiName(SessionId);
            if (!string.IsNullOrEmpty(busyUi))
            {
                Notification.Show($"~y~请先关闭 {busyUi}");
                return;
            }

            // P1-2 Fix: Register session with UIState
            EF.PoliceMod.Core.UIState.RegisterSession(SessionId, this);
            
            EF.PoliceMod.Core.UIState.MarkPatrolMenuOpen(Game.GameTime);
            _selected = 0;
            _openedAtMs = Game.GameTime;
        }

        private void Close()
        {
            EF.PoliceMod.Core.UIState.MarkPatrolMenuClosed();
            _upHeld = _downHeld = _confirmHeld = _cancelHeld = false;
        }

        // P1-2 Fix: Implement IUiSession.ForceClose
        public void ForceClose(string reason)
        {
            ModLog.Info($"[PatrolMenu] ForceClose called: {reason}");
            Close();
        }

        public void Tick()
        {
            if (!IsOpen) return;

            DrawMenu();
            HandleInput();
        }

        private void DrawMenu()
        {
            float panelW = 0.26f;
            float rowH = 0.032f;
            float headerH = 0.038f;
            float panelH = headerH + (_items.Length * rowH) + 0.01f;
            float panelX = 0.80f;
            float panelY = 0.62f;

            try { Function.Call(Hash.DRAW_RECT, panelX, panelY, panelW, panelH, 0, 0, 0, 170); } catch { }

            try
            {
                Function.Call(Hash.SET_TEXT_FONT, 0);
                Function.Call(Hash.SET_TEXT_SCALE, 0.34f, 0.34f);
                Function.Call(Hash.SET_TEXT_CENTRE, true);
                Function.Call(Hash.SET_TEXT_COLOUR, 120, 200, 255, 255);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, "巡逻指令(H)");
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
                    Function.Call(Hash.SET_TEXT_SCALE, 0.32f, 0.32f);
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
                Function.Call(Hash.SET_TEXT_SCALE, 0.28f, 0.28f);
                Function.Call(Hash.SET_TEXT_CENTRE, true);
                Function.Call(Hash.SET_TEXT_COLOUR, 200, 200, 200, 255);
                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
                string upKey = KeyBindings.MenuUp.ToString();
                string downKey = KeyBindings.MenuDown.ToString();
                string confirmKey = KeyBindings.MenuConfirm.ToString();
                string cancelKey = KeyBindings.MenuCancel.ToString();
                Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, $"{upKey}/{downKey}选择 {confirmKey}确认  H/{cancelKey}关闭");
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, panelX, panelY + panelH / 2 - 0.025f);
            }
            catch { }
        }

        private void HandleInput()
        {
            int now = Game.GameTime;
            bool canClose = (now - _openedAtMs) > 250;

            // P1-2 Fix: Use PatrolMenu key binding for cancel instead of ArrestMenu
            bool cancel = IsRawKeyDown(KeyBindings.MenuCancel)
                || IsRawKeyDown(EF.PoliceMod.Core.KeyBindings.PatrolMenu);
            if (cancel && canClose)
            {
                if (!_cancelHeld)
                {
                    _cancelHeld = true;
                    Close();
                }
                return;
            }
            _cancelHeld = false;

            bool up = IsRawKeyDown(KeyBindings.MenuUp);
            bool down = IsRawKeyDown(KeyBindings.MenuDown);
            bool confirm = IsRawKeyDown(KeyBindings.MenuConfirm);

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
                }
                _confirmHeld = true;
            }
            else _confirmHeld = false;
        }

        private int _lastExecuteAtMs = 0;
        private const int ExecuteDebounceMs = 500;

        private void ExecuteSelected()
        {
            int now = Game.GameTime;
            if (now - _lastExecuteAtMs < ExecuteDebounceMs) return;
            _lastExecuteAtMs = now;

            switch (_selected)
            {
                case 0:
                    EventBus.Publish(new PatrolCheckRequestedEvent());
                    break;
                case 1:
                    EventBus.Publish(new PatrolRequestIdEvent());
                    break;
                case 2:
                    EventBus.Publish(new PatrolSearchRequestedEvent());
                    break;
                case 3:
                    EventBus.Publish(new PatrolBreathalyzerRequestedEvent());
                    break;
            }
        }
    }
}

