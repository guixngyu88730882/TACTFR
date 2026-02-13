using EF.PoliceMod.Core;
using GTA;
using GTA.Native;
using System;

namespace EF.PoliceMod.Systems
{
    public sealed class PlayerRespawnFixSystem
    {
        private int _lastHandledDeadAtMs = -999999;
        private const int DeadHandleDebounceMs = 2500;
        private bool _wasDead = false;

        public PlayerRespawnFixSystem()
        {
            EventBus.Subscribe<DutyEndedEvent>(_ => { _lastHandledDeadAtMs = -999999; });
        }

        public void Tick()
        {
            try
            {
                var p = Game.Player.Character;
                if (p == null || !p.Exists()) return;

                if (p.IsDead)
                {
                    if (!_wasDead)
                    {
                        _wasDead = true;
                        ModLog.Info("[PlayerRespawnFixSystem] Player death detected");
                    }

                    int now = Game.GameTime;
                    if (now - _lastHandledDeadAtMs < DeadHandleDebounceMs) return;
                    _lastHandledDeadAtMs = now;

                    try { Function.Call(Hash.FREEZE_ENTITY_POSITION, p.Handle, false); } catch { }
                    try { Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, true, 0); } catch { }
                    try { Function.Call(Hash.ENABLE_ALL_CONTROL_ACTIONS, 0); } catch { }
                    try { Function.Call(Hash.RENDER_SCRIPT_CAMS, false, false, 0, true, false); } catch { }
                    // 兜底：黑屏卡圈常见是 screen fade / death camera 卡死
                    try { Function.Call(Hash.DO_SCREEN_FADE_IN, 0); } catch { }
                    try { Function.Call(Hash.CLEAR_FOCUS); } catch { }
                    // 强制复活兜底：部分模型切换后会卡死在死亡镜头，game 不会自动 respawn
                    try
                    {
                        // RESURRECT_PED + CLEAR_PED_TASKS：best-effort（不会影响正常死亡流程，且有节流）
                        Function.Call(Hash.RESURRECT_PED, p.Handle);
                        Function.Call(Hash.CLEAR_PED_TASKS_IMMEDIATELY, p.Handle);
                        Function.Call(Hash.SET_ENTITY_HEALTH, p.Handle, 200);
                        Function.Call(Hash.SET_PED_TO_RAGDOLL, p.Handle, 0, 0, 0, false, false, false);
                    }
                    catch { }
                }
                else if (_wasDead)
                {
                    _wasDead = false;
                    ModLog.Info("[PlayerRespawnFixSystem] Player respawned, restoring state");
                    OnPlayerRespawned();
                }
            }
            catch { }
        }

        private void OnPlayerRespawned()
        {
            try
            {
                var dutyController = EFCore.Instance?.GetDutyController();
                if (dutyController != null)
                {
                    dutyController.RestorePlayerState();
                    ModLog.Info("[PlayerRespawnFixSystem] Player state restored after respawn");
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[PlayerRespawnFixSystem] OnPlayerRespawned failed: " + ex);
            }
        }
    }
}
