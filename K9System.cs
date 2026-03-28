using EF.PoliceMod.Core;
using EF.PoliceMod.Core.Contracts;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;

namespace EF.PoliceMod.Systems
{
    public class K9System : ISystem
    {
        private Ped _k9Ped;
        private Blip _k9Blip;
        private int _lastTickMs = 0;
        private Ped _currentTarget;

        public void Initialize()
        {
            EventBus.Subscribe<SummonK9Event>(OnSummonK9);
            EventBus.Subscribe<DismissK9Event>(OnDismissK9);
            EventBus.Subscribe<K9AttackEvent>(OnK9Attack);
            EventBus.Subscribe<EF.PoliceMod.Core.SuspectResistEvent>(OnSuspectResist);
            EventBus.Subscribe<EF.PoliceMod.Core.PatrolSuspectFleeEvent>(OnPatrolFlee);
            EventBus.Subscribe<EF.PoliceMod.Core.PatrolSuspectResistEvent>(OnPatrolResist);
        }

        private void OnSuspectResist(EF.PoliceMod.Core.SuspectResistEvent e)
        {
            if (e.Suspect != null && e.Suspect.Exists())
            {
                EventBus.Publish(new K9AttackEvent(e.Suspect.Handle));
            }
            else if (e.SuspectHandle > 0)
            {
                EventBus.Publish(new K9AttackEvent(e.SuspectHandle));
            }
        }

        private void OnPatrolFlee(EF.PoliceMod.Core.PatrolSuspectFleeEvent e)
        {
            if (e.Suspect != null && e.Suspect.Exists())
            {
                EventBus.Publish(new K9AttackEvent(e.Suspect.Handle));
            }
        }

        private void OnPatrolResist(EF.PoliceMod.Core.PatrolSuspectResistEvent e)
        {
            if (e.Suspect != null && e.Suspect.Exists())
            {
                EventBus.Publish(new K9AttackEvent(e.Suspect.Handle));
            }
        }

        public void Shutdown()
        {
            Dismiss();
        }

        private void OnSummonK9(SummonK9Event e)
        {
            if (_k9Ped != null && _k9Ped.Exists() && !_k9Ped.IsDead)
            {
                Notification.Show("~y~警犬已在身边");
                return;
            }

            Dismiss();

            Ped player = Game.Player.Character;
            Vector3 pos = player.Position + player.RightVector * 1.5f;

            Model dogModel = new Model("a_c_rottweiler");
            dogModel.Request(2000);

            _k9Ped = World.CreatePed(dogModel, pos);
            if (_k9Ped != null && _k9Ped.Exists())
            {
                _k9Blip = _k9Ped.AddBlip();
                _k9Blip.Color = BlipColor.Blue;
                _k9Blip.Sprite = BlipSprite.Standard;
                _k9Blip.Scale = 0.7f;
                _k9Blip.Name = "K-9 警犬";

                // Setup K-9 properties
                _k9Ped.IsPersistent = true;
                _k9Ped.BlockPermanentEvents = true;
                _k9Ped.MaxHealth = 500;
                _k9Ped.Health = 500;

                // Add to player group
                int playerGroup = Function.Call<int>(Hash.GET_PED_GROUP_INDEX, player.Handle);
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, _k9Ped.Handle, playerGroup);

                Notification.Show("~g~K-9 警犬已部署");
            }
        }

        private void OnDismissK9(DismissK9Event e)
        {
            Dismiss();
            Notification.Show("~g~K-9 警犬已解散");
        }

        private void Dismiss()
        {
            if (_k9Blip != null && _k9Blip.Exists())
            {
                _k9Blip.Delete();
                _k9Blip = null;
            }

            if (_k9Ped != null && _k9Ped.Exists())
            {
                _k9Ped.MarkAsNoLongerNeeded();
                _k9Ped = null;
            }
            _currentTarget = null;
        }

        private void OnK9Attack(K9AttackEvent e)
        {
            if (_k9Ped == null || !_k9Ped.Exists() || _k9Ped.IsDead)
            {
                Notification.Show("~y~K-9 警犬不可用");
                return;
            }

            Ped target = null;
            try { target = Entity.FromHandle(e.TargetHandle) as Ped; } catch { }

            if (target != null && target.Exists() && !target.IsDead)
            {
                _currentTarget = target;
                int playerGroup = Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character.Handle);
                Function.Call(Hash.REMOVE_PED_FROM_GROUP, _k9Ped.Handle);
                _k9Ped.Task.Combat(target);
                Notification.Show("~r~警犬正在攻击目标！");
            }
        }

        public void Tick()
        {
            if (_k9Ped == null || !_k9Ped.Exists() || _k9Ped.IsDead) return;

            int now = Game.GameTime;
            if (now - _lastTickMs < 500) return;
            _lastTickMs = now;

            // If target is dead or arrested, return to player
            if (_currentTarget != null)
            {
                if (!_currentTarget.Exists() || _currentTarget.IsDead || Function.Call<bool>(Hash.IS_PED_CUFFED, _currentTarget.Handle))
                {
                    _currentTarget = null;
                    _k9Ped.Task.ClearAll();
                    int playerGroup = Function.Call<int>(Hash.GET_PED_GROUP_INDEX, Game.Player.Character.Handle);
                    Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, _k9Ped.Handle, playerGroup);
                    Notification.Show("~g~警犬已召回");
                }
            }
            
            // Warp if too far
            Ped player = Game.Player.Character;
            if (player != null && player.Exists() && _currentTarget == null)
            {
                if (_k9Ped.Position.DistanceTo(player.Position) > 100f)
                {
                    _k9Ped.Position = player.Position + player.RightVector * 2.0f;
                }
            }
        }
    }

    public class SummonK9Event { }
    public class DismissK9Event { }
    public class K9AttackEvent
    {
        public int TargetHandle { get; }
        public K9AttackEvent(int targetHandle) { TargetHandle = targetHandle; }
    }
}