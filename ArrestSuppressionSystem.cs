using EF.PoliceMod.Core;
using EF.PoliceMod.Gameplay;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 调度菜单：逮捕压制
    /// - 让小队警员去压制当前锁定嫌疑人
    /// - 压制成功后：标记嫌疑人为 Compliant + AutoLockCompliant
    /// - 玩家可直接按 H 选择拘捕动作（无需再按 L 让嫌疑人举手）
    /// </summary>
    public class ArrestSuppressionSystem
    {
        private readonly OfficerSquadSystem _squad;
        private readonly SuspectController _suspectController;
        private readonly LockTargetSystem _lockTargetSystem;

        private bool _active;
        private int _suspectHandle;
        private int _officerHandle;
        private int _aimStartAtMs;

        private const float SuppressDistance = 6.0f;
        private const int AimHoldMs = 1500;

        public ArrestSuppressionSystem(OfficerSquadSystem squad, SuspectController suspectController, LockTargetSystem lockTargetSystem)
        {
            _squad = squad;
            _suspectController = suspectController;
            _lockTargetSystem = lockTargetSystem;

            EventBus.Subscribe<DispatchArrestSuppressRequestedEvent>(_ => Start());
            EventBus.Subscribe<DutyEndedEvent>(_ => Cancel());
        }

        public void Tick()
        {
            if (!_active) return;

            Ped suspect = null;
            Ped officer = null;

            try { suspect = Entity.FromHandle(_suspectHandle) as Ped; } catch { suspect = null; }
            try { officer = Entity.FromHandle(_officerHandle) as Ped; } catch { officer = null; }

            if (suspect == null || !suspect.Exists()) { Cancel(); return; }
            if (officer == null || !officer.Exists() || officer.IsDead) { Cancel(); return; }

            float d = 9999f;
            try { d = officer.Position.DistanceTo(suspect.Position); } catch { d = 9999f; }

            if (d > SuppressDistance)
            {
                try { Function.Call(Hash.TASK_GO_TO_ENTITY, officer.Handle, suspect.Handle, -1, 3.5f, 2.0f, 1073741824, 0); } catch { }
                _aimStartAtMs = 0;
                return;
            }

            // 到位：持续瞄准一段时间后触发“跪地抱头 + 顺从”
            try { Function.Call(Hash.TASK_AIM_GUN_AT_ENTITY, officer.Handle, suspect.Handle, 800, false); } catch { }

            int now = Game.GameTime;
            if (_aimStartAtMs <= 0) _aimStartAtMs = now;
            if (now - _aimStartAtMs < AimHoldMs) return;

            TriggerSurrender(suspect);
            Cancel();
        }

        private void Start()
        {
            var suspect = EFCore.Instance?.LockTargetSystem?.CurrentTarget;
            if (suspect == null || !suspect.Exists())
            {
                Notification.Show("~y~未锁定嫌疑人");
                return;
            }

            _squad?.SummonIfNeeded(2);

            var officer = PickOfficer();
            if (officer == null || !officer.Exists())
            {
                Notification.Show("~y~没有可用警员");
                return;
            }

            _active = true;
            _suspectHandle = suspect.Handle;
            _officerHandle = officer.Handle;
            _aimStartAtMs = 0;

            Notification.Show("~b~小队正在压制嫌疑人...");
        }

        private Ped PickOfficer()
        {
            try
            {
                if (_squad?.Officers == null) return null;
                foreach (var o in _squad.Officers)
                {
                    if (o != null && o.Exists() && !o.IsDead)
                        return o;
                }
            }
            catch { }

            return null;
        }

        private void TriggerSurrender(Ped suspect)
        {
            if (suspect == null || !suspect.Exists()) return;

            try
            {
                Function.Call(Hash.REQUEST_ANIM_DICT, "random@arrests");
                if (Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, "random@arrests"))
                {
                    try { suspect.Task.PlayAnimation("random@arrests", "kneeling_arrest_idle", 4.0f, -1, AnimationFlags.StayInEndFrame); } catch { }
                }
            }
            catch { }

            try { _suspectController?.SetCompliant(suspect); } catch { }
            try { _lockTargetSystem?.AutoLockCompliant(suspect); } catch { }

            Notification.Show("~g~嫌疑人已被压制：按 H 选择拘捕动作");
        }

        private void Cancel()
        {
            _active = false;
            _suspectHandle = 0;
            _officerHandle = 0;
            _aimStartAtMs = 0;
        }
    }
}