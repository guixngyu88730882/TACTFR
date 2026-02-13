using EF.PoliceMod.Core;    // EventBus / UIState（如果你用的是这个命名空间）
using EF.PoliceMod.Data;    // WantedRegistry（如果你放在这里）
using EF.PoliceMod.Input;   // OpenPoliceTerminalEvent（如果你放在这里）
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Collections.Generic;
namespace EF.PoliceMod.Systems
{
    public class PoliceTerminalController
    {
        readonly WantedRegistry _wantedRegistry;
        readonly PoliceTerminalUI _ui;
        readonly List<Vector3> _terminalPositions = new List<Vector3>();
        const float INTERACT_RANGE = 2.0f;
        bool _uiOpen = false;
        private Blip _terminalBlip;
        // 自绘左上角提示状态（用于持续显示，不触发系统提示音）
        private bool _wasPlayerNearTerminal = false;
        private int _helpCooldownMs = 1500;
        private int _lastHelpShownAt = 0;

        void HandleOpenTerminalEvent(OpenPoliceTerminalEvent e)
        {
            ModLog.Info("[PoliceTerminal] Received OpenPoliceTerminalEvent");

            if (_uiOpen)
            {
                ModLog.Info("[PoliceTerminal] UI already open; ignoring OpenPoliceTerminalEvent");
                return;
            }


            // 车载终端：仅 T 键入口允许在车内打开；O 键入口仍要求在警局终端点范围内
            try
            {
                var player = Game.Player.Character;
                bool onDuty = EF.PoliceMod.Systems.DutyQuery.IsOnDuty;
                bool inVehicle = player != null && player.Exists() && player.IsInVehicle();

                bool fromVehicle = false;
                try { fromVehicle = (e.Source == EF.PoliceMod.Input.OpenPoliceTerminalSource.VehicleTerminal); } catch { fromVehicle = false; }

                if (fromVehicle && onDuty && inVehicle)
                {
                    OpenUI();
                    return;
                }
            }
            catch { }

            if (!IsPlayerNearAnyTerminal())
            {
                ModLog.Info("[PoliceTerminal] Player not near any terminal");
                Notification.Show("~y~你不在警用终端附近");
                return;
            }

            if (!CanOpenTerminal())
            {
                ModLog.Info("[PoliceTerminal] CanOpenTerminal == false (case manager denied)");
                Notification.Show("~y~暂时无法打开终端");
                return;
            }

            OpenUI();
        }
        // 在类中新增：用于卸载时清理订阅与 blip
        public void Shutdown()
        {
            try
            {
                // 退订事件，防止在模组卸载/重载时出现悬挂回调
                EventBus.Unsubscribe<OpenPoliceTerminalEvent>(HandleOpenTerminalEvent);
            }
            catch (Exception ex)
            {
                ModLog.Error("[PoliceTerminal] Failed to unsubscribe OpenPoliceTerminalEvent: " + ex);
            }

            try
            {
                // 删除终端 Blip（如果存在）
                if (_terminalBlip != null)
                {
                    try
                    {
                        _terminalBlip.Delete(); // <-- 使用 Delete()
                    }
                    catch (Exception exB)
                    {
                        ModLog.Error("[PoliceTerminal] Failed to delete terminal blip: " + exB);
                    }
                    _terminalBlip = null;
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[PoliceTerminal] Shutdown blip cleanup error: " + ex);
            }

            // 如果 UI 打开，恢复控制（保险）
            try
            {
                if (_uiOpen)
                {
                    _uiOpen = false;

                    try { EF.PoliceMod.Core.UIState.MarkPoliceTerminalClosed(); } catch { }
                    RestorePlayerControl();
                    _ui?.Close();
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[PoliceTerminal] Shutdown restore UI/control error: " + ex);
            }
        }


        public PoliceTerminalController(WantedRegistry wantedRegistry)
        {
            _wantedRegistry = wantedRegistry;
            _ui = new PoliceTerminalUI(_wantedRegistry, OnSelected, OnClosed);

            // 添加默认/示例终端点（你可以替换或追加更多点）
            // 我把两个示例坐标都放进列表，若你只想要一个位置，删除其中一行即可
            _terminalPositions.Add(new Vector3(441.4f, -981.8f, 30.7f));
            _terminalPositions.Add(new Vector3(443.5f, -981.0f, 30.7f));

            // 创建地图图标（警用终端）——使用第一个坐标作为图标位置
            try
            {
                var blipPos = _terminalPositions.Count > 0 ? _terminalPositions[0] : new Vector3(441.4f, -981.8f, 30.7f);
                _terminalBlip = World.CreateBlip(blipPos);

                // ★修复点3：终端图标改“电脑/终端”
                // 注意：不同 ScriptHookVDotNet 版本 BlipSprite 枚举可能不全；这里按“增强版函数”走 native。
                // 521：常用的“电脑/终端”类图标（若你想换其它图标，只需要改这个 spriteId）。
                try { Function.Call(Hash.SET_BLIP_SPRITE, _terminalBlip.Handle, 521); } catch { }

                _terminalBlip.Color = BlipColor.Blue;
                _terminalBlip.IsShortRange = true;
                _terminalBlip.Name = "警用终端";
            }
            catch (Exception ex)
            {
                ModLog.Error("[PoliceTerminalController] Failed to create terminal blip: " + ex);
            }

            // 订阅打开终端事件（InputManager 会发布 OpenPoliceTerminalEvent）
            EventBus.Subscribe<OpenPoliceTerminalEvent>(HandleOpenTerminalEvent);
            EventBus.Subscribe<DutyEndedEvent>(_ => { try { if (_uiOpen) CloseUI(); } catch { } });
            EventBus.Subscribe<EF.PoliceMod.Core.SuspectDeliveredEvent>(_ => { try { if (_uiOpen) CloseUI(); } catch { } });

            ModLog.Info("[PoliceTerminalController] Initialized with " + _terminalPositions.Count + " terminal positions");
        }


        // CanOpenTerminal：从只读门面读取（不再依赖 CaseManager/EFCore.Instance）
        bool CanOpenTerminal()
        {
            try { return EF.PoliceMod.Systems.TerminalAccessQuery.CanOpenTerminal; }
            catch { return false; }
        }
        bool IsPlayerNearAnyTerminal()
        {
            try
            {
                var player = Game.Player.Character;
                if (player == null || !player.Exists())
                {
                    // 无法判断玩家位置：返回 false（保守处理）
                    return false;
                }

                var playerPos = player.Position;
                foreach (var p in _terminalPositions)
                {
                    if (playerPos.DistanceTo(p) <= INTERACT_RANGE) return true;
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[PoliceTerminal] IsPlayerNearAnyTerminal exception: " + ex);
                // 出错时保守返回 false，避免意外冒泡
            }

            return false;
        }


        void OpenUI()
        {
            _uiOpen = true;
            EF.PoliceMod.Core.UIState.MarkPoliceTerminalOpen(Game.GameTime);
            ModLog.Info("[PoliceTerminal] Opened - clearing help text/sound");
            // 清除可能残留的 help（停止提示音）
            try
            {
                Function.Call(Hash.CLEAR_HELP, true);
            }
            catch { }
            LockPlayerControl();
            _wantedRegistry.RefreshFromWorld();
            _ui.Open();
        }


        void CloseUI()
        {
            _uiOpen = false;
            _ui.Close();
            // 恢复全局 UI 状态
            EF.PoliceMod.Core.UIState.MarkPoliceTerminalClosed();
            ModLog.Info("[PoliceTerminal] Closed - restoring player control");
            RestorePlayerControl();
        }



        void OnSelected(int optionId, int stars)
        {
            // 终端内切换巡逻模式（不接警）
            if (optionId == 90)
            {
                try { EventBus.Publish(new EF.PoliceMod.Core.TogglePatrolModeEvent()); } catch { }
                CloseUI();
                return;
            }

            // 终端接警：把“地点+形态+星级”发给 CaseManager
            EventBus.Publish(new EF.PoliceMod.Core.TerminalCaseSelectedEvent(optionId, stars));
            CloseUI();
        }

        void OnClosed()
        {
            CloseUI();
        }

        void LockPlayerControl()
        {
            try
            {
                var player = Game.Player.Character;
                if (player != null && player.Exists())
                {
                    // 冻结实体位置
                    Function.Call(Hash.FREEZE_ENTITY_POSITION, player.Handle, true);
                    // 禁用玩家控制（彻底）
                    Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, false, 0);
                    // 再加保险：禁用所有控制动作（部分 Native 实现可能需要每帧调用，但这里在打开/关闭点调用一次）
                    Function.Call(Hash.DISABLE_ALL_CONTROL_ACTIONS, 0);
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[PoliceTerminal] LockPlayerControl error: " + ex);
            }
        }

        void RestorePlayerControl()
        {
            try
            {
                var player = Game.Player.Character;
                if (player != null && player.Exists())
                {
                    // 取消冻结
                    Function.Call(Hash.FREEZE_ENTITY_POSITION, player.Handle, false);
                    // 恢复控制
                    Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, true, 0);
                    Function.Call(Hash.ENABLE_ALL_CONTROL_ACTIONS, 0);
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[PoliceTerminal] RestorePlayerControl error: " + ex);
            }
        }
        // 自绘左上角提示（持续绘制，不触发系统提示音）
        void DrawTopLeftPrompt(string text)
        {
            // 背景矩形参数（微调这些值可以改变位置/大小）
            float rectX = 0.18f;   // 屏幕中点 X（0..1）
            float rectY = 0.06f;   // 屏幕中点 Y（0..1）
            float rectW = 0.34f;   // 宽度
            float rectH = 0.055f;  // 高度

            // 半透明深色背景
            Function.Call(Hash.DRAW_RECT, rectX, rectY, rectW, rectH, 0, 0, 0, 180);

            // 文本位置（调整使文本在背景左侧）
            float textX = rectX - rectW / 2 + 0.02f;
            float textY = rectY - rectH / 2 + 0.008f;

            // 文本样式
            Function.Call(Hash.SET_TEXT_FONT, 0);
            Function.Call(Hash.SET_TEXT_SCALE, 0.36f, 0.36f);
            Function.Call(Hash.SET_TEXT_WRAP, 0f, 1f);
            Function.Call(Hash.SET_TEXT_CENTRE, false);
            Function.Call(Hash.SET_TEXT_COLOUR, 255, 255, 255, 255);

            // 绘制文本（用 display text native）
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_TEXT, "STRING");
            Function.Call(Hash.ADD_TEXT_COMPONENT_SUBSTRING_PLAYER_NAME, text);
            Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_TEXT, textX, textY);
        }


        // 在主 Tick 中被调用（见 EFCore 的 wiring）
        public void Tick()
        {

            // 自愈：只通过 UIState 公开方法复位（避免直接改 public 字段导致状态不一致）
            try
            {
                if (!_uiOpen && EF.PoliceMod.Core.UIState.IsPoliceTerminalOpen)
                {
                    ModLog.Warn("[PoliceTerminal] UIState.IsPoliceTerminalOpen stuck true while _uiOpen=false -> auto reset");
                    EF.PoliceMod.Core.UIState.MarkPoliceTerminalClosed();
                    RestorePlayerControl();
                }
            }
            catch { }

            // 如果 UI 打开，UI 自己会处理按键与绘制（PoliceTerminalUI.Update）
            if (_uiOpen)
            {
                EF.PoliceMod.Core.UIState.BeatPoliceTerminal(Game.GameTime);
                _ui.Update();
            }
            else
            {
                if (IsPlayerNearAnyTerminal() && CanOpenTerminal())
                {
                    // 使用自绘左上角提示（持续渲染、无音、不闪）
                    DrawTopLeftPrompt("按 ~o~O~s~ 打开警用终端");
#if DEBUG
                    if (!_wasPlayerNearTerminal) ModLog.Info("[PoliceTerminal] Top-left prompt shown (enter range)");
#endif

                    _wasPlayerNearTerminal = true;
                }
                else
                {
                    _wasPlayerNearTerminal = false;
                }
           }
        }
     }
  }
    
   
