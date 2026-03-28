using EF.PoliceMod.Core;
using EF.PoliceMod.Core.Contracts;
using EF.PoliceMod.Core.Contracts.Snapshots;
using EF.PoliceMod.Gameplay;
using GTA;
using GTA.Native;
using GTA.UI;
using System;
using System.Linq;

namespace EF.PoliceMod.Systems
{
    public sealed class PatrolSystem : IPatrolService
    {
        private readonly LockTargetSystem _lockTargetSystem;

        private bool _enabled = false;
        private int _lastActionAtMs = 0;
        private const int ActionDebounceMs = 600;
        private int _checksCompleted = 0;
        private bool _initialized = false;

        public bool IsEnabled => _enabled;

        public PatrolSystem(LockTargetSystem lockTargetSystem)
        {
            _lockTargetSystem = lockTargetSystem;

            EventBus.Subscribe<DutyEndedEvent>(_ => _enabled = false);
            EventBus.Subscribe<PatrolModeToggledEvent>(e => _enabled = e.Enabled);
            EventBus.Subscribe<PatrolCheckRequestedEvent>(_ => RequestCheck());
            EventBus.Subscribe<PatrolRequestIdEvent>(_ => RequestIdCheck());
            EventBus.Subscribe<PatrolSearchRequestedEvent>(_ => RequestSearch());
        }

        public void Initialize()
        {
            if (_initialized) return;
            _initialized = true;
            ModLog.Info("[PatrolSystem] Initialized");
        }

        private int _searchEndTime = 0;
        private bool _isSearching = false;
        private Ped _searchTarget = null;
        private bool _playerControlLocked = false;
        private bool _playerFrozen = false;
        private bool _targetFrozen = false;

        public void Tick()
        {
            if (_isSearching && Game.GameTime > _searchEndTime)
            {
                EndSearchAnimation();
            }
        }

        public void Shutdown()
        {
            _enabled = false;
            _checksCompleted = 0;
            _initialized = false;
            ModLog.Info("[PatrolSystem] Shutdown");
        }

        public PatrolSnapshot GetSnapshot()
        {
            return new PatrolSnapshot(_enabled, _checksCompleted);
        }

        public void Enable()
        {
            if (_enabled) return;
            _enabled = true;
            EventBus.Publish(new PatrolModeToggledEvent(true));
            ModLog.Info("[PatrolSystem] Enabled");
        }

        public void Disable()
        {
            if (!_enabled) return;
            _enabled = false;
            EventBus.Publish(new PatrolModeToggledEvent(false));
            ModLog.Info("[PatrolSystem] Disabled");
        }

        public void Toggle()
        {
            if (_enabled)
                Disable();
            else
                Enable();
        }

        public void RequestCheck()
        {
            if (!_enabled) { Notification.Show("~y~巡逻模式未开启"); return; }
            if (!Debounce()) return;

            var target = ResolveTarget();
            if (target == null || !target.Exists()) { Notification.Show("~y~请先用 L 锁定目标"); return; }

            int roll = new Random(Game.GameTime + target.Handle).Next(100);
            if (roll < 70)
            {
                SmsNotification.Show("警务终端", "巡逻检查", "未发现异常。");
            }
            else
            {
                SmsNotification.Show("警务终端", "巡逻检查", "该对象行为可疑：建议查身份证或搜查。");
            }

            _checksCompleted++;
        }

        public void RequestIdCheck()
        {
            if (!_enabled) { Notification.Show("~y~巡逻模式未开启"); return; }
            if (!Debounce()) return;

            var target = ResolveTarget();
            if (target == null || !target.Exists()) { Notification.Show("~y~请先用 L 锁定目标"); return; }

            int roll = new Random(Game.GameTime + target.Handle * 7).Next(100);
            if (roll < 65)
            {
                SmsNotification.Show("警务终端", "身份证", "对方配合出示身份证（姓名/信息待后续扩展）。");
                _checksCompleted++;
                return;
            }
            if (roll < 85)
            {
                SmsNotification.Show("警务终端", "身份证", "对方拒绝配合并开始逃跑！");
                try { EventBus.Publish(new PatrolSuspectFleeEvent(target, Game.Player.Character)); } catch { }
                _checksCompleted++;
                return;
            }

            SmsNotification.Show("警务终端", "身份证", "对方突然反抗！");
            try { EventBus.Publish(new PatrolSuspectResistEvent(target, Game.Player.Character)); } catch { }
            _checksCompleted++;
        }

        public void RequestSearch()
        {
            if (!_enabled) { Notification.Show("~y~巡逻模式未开启"); return; }
            if (!Debounce()) return;

            var target = ResolveTarget();
            if (target == null || !target.Exists()) { Notification.Show("~y~请先用 L 锁定目标"); return; }

            PlaySearchAnimation(target);

            int roll = new Random(Game.GameTime + target.Handle * 13).Next(100);
            if (roll < 40) // 75 -> 40
            {
                SmsNotification.Show("警务终端", "搜查", "搜查结果：未发现违禁品。");
                _checksCompleted++;
                return;
            }
            if (roll < 70) // 92 -> 70
            {
                SmsNotification.Show("警务终端", "搜查", "~o~发现违禁品~s~：对方试图逃跑！");
                try { EventBus.Publish(new PatrolContrabandFoundEvent(target, false)); } catch { }
                try { EventBus.Publish(new PatrolSuspectFleeEvent(target, Game.Player.Character)); } catch { }
                _checksCompleted++;
                return;
            }

            SmsNotification.Show("警务终端", "搜查", "~r~发现携带武器~s~：对方拔枪反抗！");
            try { EventBus.Publish(new PatrolContrabandFoundEvent(target, true)); } catch { }
            try { EventBus.Publish(new PatrolSuspectResistEvent(target, Game.Player.Character)); } catch { }
            _checksCompleted++;
        }

        public void RequestBreathalyzer()
        {
            if (!_enabled) { Notification.Show("~y~巡逻模式未开启"); return; }
            if (!Debounce()) return;

            var target = ResolveTarget();
            if (target == null || !target.Exists()) { Notification.Show("~y~请先用 L 锁定目标"); return; }

            int roll = new Random(Game.GameTime + target.Handle * 17).Next(100);
            if (roll < 80)
            {
                SmsNotification.Show("警务终端", "酒精测试", "测试结果：~g~阴性（未饮酒/吸毒）~s~。");
            }
            else
            {
                SmsNotification.Show("警务终端", "酒精测试", "测试结果：~r~阳性（酒驾/毒驾）~s~。可以实施逮捕！");
                if (roll > 90)
                {
                    try { EventBus.Publish(new PatrolSuspectFleeEvent(target, Game.Player.Character)); } catch { }
                }
                else if (roll > 85)
                {
                    try { EventBus.Publish(new PatrolSuspectResistEvent(target, Game.Player.Character)); } catch { }
                }
            }

            _checksCompleted++;
        }

        private void PlaySearchAnimation(Ped target)
        {
            try
            {
                var player = Game.Player.Character;
                if (player == null || !player.Exists()) return;

                _playerControlLocked = false;
                _playerFrozen = false;
                _targetFrozen = false;

                // 升级为更电影级别/平缓的搜身动画
                string dict = "missexile3";
                string anim = "ex03_dingy_search_b_michael";

                // Function.Call(Hash.REQUEST_ANIM_DICT, dict);
                // SHVDN handles dictionary loading in Task.PlayAnimation

                try
                {
                    try { Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, false, 0); _playerControlLocked = true; } catch { }
                    try { Function.Call(Hash.FREEZE_ENTITY_POSITION, player.Handle, true); _playerFrozen = true; } catch { }
                    try { if (target != null && target.Exists()) { Function.Call(Hash.FREEZE_ENTITY_POSITION, target.Handle, true); _targetFrozen = true; } } catch { }

                    player.Task.ClearAll();
                    // 播放搜查动画
                    player.Task.PlayAnimation(dict, anim, 8.0f, -1, AnimationFlags.StayInEndFrame);
                }
                catch { }

                if (target != null && target.Exists())
                {
                    try
                    {
                        target.Task.ClearAll();
                        target.Task.StandStill(3000);
                        target.Task.HandsUp(-1);
                    }
                    catch { }
                }

                _isSearching = true;
                _searchTarget = target;
                _searchEndTime = Game.GameTime + 3000;

                ModLog.Info("[PatrolSystem] Cinematic Search animation started");
            }
            catch (Exception ex)
            {
                ModLog.Error("[PatrolSystem] Search animation failed: " + ex);
            }
        }

        private void EndSearchAnimation()
        {
            _isSearching = false;
            var player = Game.Player.Character;
            if (player != null && player.Exists())
            {
                try { player.Task.ClearAll(); } catch { }
            }

            try { if (_targetFrozen && _searchTarget != null && _searchTarget.Exists()) Function.Call(Hash.FREEZE_ENTITY_POSITION, _searchTarget.Handle, false); } catch { }
            try { if (_playerFrozen && player != null && player.Exists()) Function.Call(Hash.FREEZE_ENTITY_POSITION, player.Handle, false); } catch { }
            try { if (_playerControlLocked) Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, true, 0); } catch { }
            
            _searchTarget = null;
            ModLog.Info("[PatrolSystem] Cinematic Search animation ended");
        }

        private Ped ResolveTarget()
        {
            try
            {
                var t = _lockTargetSystem?.CurrentTarget;
                if (t != null && t.Exists()) return t;
            }
            catch { }

            return null;
        }

        private bool Debounce()
        {
            int now = Game.GameTime;
            if (now - _lastActionAtMs < ActionDebounceMs) return false;
            _lastActionAtMs = now;
            return true;
        }
    }
}

