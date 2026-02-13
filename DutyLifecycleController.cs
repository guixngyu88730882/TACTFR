using EF.PoliceMod;
using EF.PoliceMod.Core;
using EF.PoliceMod.Core.Contracts;
using EF.PoliceMod.Core.Contracts.Snapshots;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Collections.Generic;

namespace EFPoliceMod
{
    public class DutyLifecycleController : IDutyService
    {
        private readonly Vector3 _policeStationPos = new Vector3(
            441.2f,
           -981.9f,
             30.7f
        );

        private const float DutyRadius = 2.0f;

        private bool _isOnDuty = false;
        private int _dutyStartedAt = 0;
        private int _caseCount = 0;
        private Blip _dutyBlip;
        private bool _initialized = false;

        private Model _originalModel;
        private List<WeaponInfo> _savedWeapons = new List<WeaponInfo>();

        private class WeaponInfo
        {
            public WeaponHash Hash;
            public int Ammo;
        }

        public bool IsOnDuty => _isOnDuty;

        public DutyLifecycleController()
        {
            CreateDutyBlip();
        }

        public void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            ModLog.Info("[DutyLifecycleController] Initialized");
        }

        public void Tick()
        {
            OnTick();
        }

        public void Shutdown()
        {
            try
            {
                if (_dutyBlip != null && _dutyBlip.Exists())
                {
                    _dutyBlip.Delete();
                }
            }
            catch { }
            _dutyBlip = null;
            _isOnDuty = false;
            ModLog.Info("[DutyLifecycleController] Shutdown");
        }

        public DutySnapshot GetSnapshot()
        {
            return new DutySnapshot(_isOnDuty, _caseCount, _dutyStartedAt);
        }

        public void StartDuty()
        {
            if (_isOnDuty) return;

            SavePlayerState();

            _isOnDuty = true;
            _dutyStartedAt = Game.GameTime;
            _caseCount = 0;

            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CHALLENGE_UNLOCKED", "HUD_AWARDS", false); } catch { }
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "MP_AWARD", "HUD_AWARDS", false); } catch { }
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false); } catch { }

            try
            {
                Game.Player.WantedLevel = 0;
                try { Function.Call(Hash.SET_MAX_WANTED_LEVEL, 0); } catch { }
                try { Function.Call(Hash.SET_WANTED_LEVEL_MULTIPLIER, 0.0f); } catch { }
                try { Function.Call(Hash.SET_PLAYER_WANTED_LEVEL, Game.Player.Handle, 0, false); } catch { }
                try { Function.Call(Hash.SET_PLAYER_WANTED_LEVEL_NOW, Game.Player.Handle, false); } catch { }
                try { Function.Call(Hash.CLEAR_PLAYER_WANTED_LEVEL, Game.Player.Handle); } catch { }
                Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, Game.Player.Handle, true);
                Function.Call(Hash.SET_DISPATCH_COPS_FOR_PLAYER, Game.Player.Handle, false);
                ModLog.Info("[DutyLifecycleController] Player set to police faction");
            }
            catch (Exception ex)
            {
                ModLog.Error("[DutyLifecycleController] Failed to set police faction: " + ex);
            }

            Notification.Show("~b~已开始执勤~n~~y~提示：前往换装点更换警服");

            EventBus.Publish(new DutyStartedEvent());
            ModLog.Info("[DutyLifecycleController] Duty started");
        }

        public void EndDuty()
        {
            if (!_isOnDuty) return;

            _isOnDuty = false;

            EventBus.Publish(new LockTargetClearRequestedEvent());

            try
            {
                Function.Call(Hash.SET_POLICE_IGNORE_PLAYER, Game.Player.Handle, false);
                Function.Call(Hash.SET_DISPATCH_COPS_FOR_PLAYER, Game.Player.Handle, true);
                ModLog.Info("[DutyLifecycleController] Player removed from police faction");
            }
            catch { }

            RestorePlayerState();

            EventBus.Publish(new DutyEndedEvent());

            Notification.Show("~y~已结束执勤");
            ModLog.Info("[DutyLifecycleController] Duty ended");
        }

        private void SavePlayerState()
        {
            try
            {
                var player = Game.Player.Character;
                if (player == null || !player.Exists()) return;

                _originalModel = player.Model;
                ModLog.Info($"[DutyLifecycleController] Saved original model: {_originalModel.Hash}");

                _savedWeapons.Clear();
                foreach (Weapon weapon in player.Weapons)
                {
                    if (weapon != null)
                    {
                        _savedWeapons.Add(new WeaponInfo
                        {
                            Hash = weapon.Hash,
                            Ammo = weapon.Ammo
                        });
                        ModLog.Info($"[DutyLifecycleController] Saved weapon: {weapon.Hash}, ammo={weapon.Ammo}");
                    }
                }
            }
            catch (System.Exception ex)
            {
                ModLog.Error("[DutyLifecycleController] SavePlayerState failed: " + ex);
            }
        }

        public void RestorePlayerState()
        {
            try
            {
                var player = Game.Player.Character;
                if (player == null) return;

                foreach (var wi in _savedWeapons)
                {
                    try
                    {
                        player.Weapons.Give(wi.Hash, wi.Ammo, false, true);
                    }
                    catch { }
                }

                _savedWeapons.Clear();
            }
            catch (System.Exception ex)
            {
                ModLog.Error("[DutyLifecycleController] RestorePlayerState failed: " + ex);
            }
        }

        public void IncrementCaseCount()
        {
            _caseCount++;
        }

        private void OnTick()
        {
            Ped player = Game.Player.Character;
            if (!player.Exists())
                return;

            float distance = player.Position.DistanceTo(_policeStationPos);
            if (distance > DutyRadius)
                return;

            if (!_isOnDuty)
            {
                try { Screen.ShowSubtitle("~w~[~b~E~w~] 开始执勤", 1); } catch { }

                if (Game.IsControlJustPressed(Control.Context))
                {
                    StartDuty();
                }
            }
            else
            {
                try { Screen.ShowSubtitle("~w~[~b~E~w~] 结束执勤", 1); } catch { }

                if (Game.IsControlJustPressed(Control.Context))
                {
                    EndDuty();
                }
            }
        }

        private void CreateDutyBlip()
        {
            _dutyBlip = World.CreateBlip(_policeStationPos);
            if (_dutyBlip == null)
                return;

            _dutyBlip.Sprite = BlipSprite.PoliceStation;
            _dutyBlip.Color = BlipColor.Blue;
            _dutyBlip.Scale = 0.8f;
            _dutyBlip.IsShortRange = true;
            _dutyBlip.Name = "警局（上下班）";
        }
    }
}
