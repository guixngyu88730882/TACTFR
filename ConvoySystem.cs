using EF.PoliceMod.Core;
using EF.PoliceMod.Core.Contracts;
using GTA;
using GTA.Math;
using GTA.Native;
using GTA.UI;
using System;
using System.Collections.Generic;

namespace EF.PoliceMod.Systems
{
    public class ConvoySystem : ISystem
    {
        private const int MaxSummons = 2;
        private int _summonCount = 0;

        private readonly List<Vehicle> _convoyVehicles = new List<Vehicle>();
        private readonly List<Ped> _convoyPeds = new List<Ped>();
        private readonly List<Blip> _convoyBlips = new List<Blip>();

        private int _lastTickMs = 0;
        private Ped _currentTarget = null;
        private int _lastCombatCheckMs = 0;

        public void Initialize()
        {
            EventBus.Subscribe<SummonConvoyEvent>(OnSummonConvoy);
            EventBus.Subscribe<DismissConvoyEvent>(OnDismissConvoy);
            EventBus.Subscribe<DutyEndedEvent>(OnDutyEnded);
        }

        public void Shutdown()
        {
            DismissAll();
        }

        private void OnDutyEnded(DutyEndedEvent e)
        {
            DismissAll();
        }

        private void OnSummonConvoy(SummonConvoyEvent e)
        {
            if (_summonCount >= MaxSummons)
            {
                Notification.Show("~y~车队呼叫次数已达上限");
                return;
            }

            Ped player = Game.Player.Character;
            if (player == null || !player.Exists()) return;

            Vector3 spawnPos = player.Position - player.ForwardVector * 60f;
            try
            {
                var outPos = new OutputArgument();
                bool ok = Function.Call<bool>(Hash.GET_SAFE_COORD_FOR_PED, spawnPos.X, spawnPos.Y, spawnPos.Z, true, outPos, 0);
                if (ok) spawnPos = outPos.GetResult<Vector3>();
            }
            catch { }

            var vehModel = new Model(VehicleHash.Riot); // SWAT vehicle
            vehModel.Request(1500);
            if (!vehModel.IsLoaded)
            {
                vehModel = new Model(VehicleHash.Police);
                vehModel.Request(1500);
            }

            Vehicle car = World.CreateVehicle(vehModel, spawnPos);
            if (car != null && car.Exists())
            {
                _convoyVehicles.Add(car);

                var pedModel = new Model(PedHash.Swat01SMY);
                pedModel.Request(1000);

                // Driver and passenger
                Ped driver = World.CreatePed(pedModel, spawnPos);
                Ped passenger = World.CreatePed(pedModel, spawnPos);

                if (driver != null && driver.Exists())
                {
                    driver.SetIntoVehicle(car, VehicleSeat.Driver);
                    SetupConvoyPed(driver);
                    _convoyPeds.Add(driver);
                    AddBlip(driver);
                }

                if (passenger != null && passenger.Exists())
                {
                    passenger.SetIntoVehicle(car, VehicleSeat.Passenger);
                    SetupConvoyPed(passenger);
                    _convoyPeds.Add(passenger);
                    AddBlip(passenger);
                }

                _summonCount++;
                Notification.Show($"~g~车队已部署 ({_summonCount}/{MaxSummons})");

                // Command driver to follow player
                if (driver != null && driver.Exists())
                {
                    Function.Call(Hash.TASK_VEHICLE_ESCORT, driver.Handle, car.Handle, player.Handle, -1, 20.0f, 1, 5.0f, 0, 15.0f);
                }
            }
        }

        private void OnDismissConvoy(DismissConvoyEvent e)
        {
            DismissAll();
            Notification.Show("~y~车队已解散");
        }

        private void DismissAll()
        {
            foreach (var b in _convoyBlips) if (b != null && b.Exists()) b.Delete();
            _convoyBlips.Clear();

            foreach (var p in _convoyPeds)
            {
                if (p != null && p.Exists())
                {
                    p.MarkAsNoLongerNeeded();
                }
            }
            _convoyPeds.Clear();

            foreach (var v in _convoyVehicles)
            {
                if (v != null && v.Exists())
                {
                    v.MarkAsNoLongerNeeded();
                }
            }
            _convoyVehicles.Clear();

            _summonCount = 0;
            _currentTarget = null;
        }

        private void SetupConvoyPed(Ped p)
        {
            p.BlockPermanentEvents = true;
            p.AlwaysKeepTask = true;
            p.Armor = 100;
            p.Weapons.Give(WeaponHash.CarbineRifle, 500, true, true);
            Function.Call(Hash.SET_PED_AS_COP, p.Handle, true);

            // Friendly to player
            var player = Game.Player.Character;
            if (player != null && player.Exists())
            {
                int pGrp = 0;
                try { pGrp = player.RelationshipGroup.Hash; } catch { }
                if (pGrp != 0)
                {
                    p.RelationshipGroup = player.RelationshipGroup;
                }
            }
            Function.Call(Hash.SET_CAN_ATTACK_FRIENDLY, p.Handle, false, false);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, p.Handle, 46, true);
        }

        private void AddBlip(Ped p)
        {
            var b = p.AddBlip();
            b.Sprite = BlipSprite.Standard;
            b.Color = BlipColor.Blue;
            b.Scale = 0.6f;
            b.Name = "战术车队";
            _convoyBlips.Add(b);
        }

        public void Tick()
        {
            if (_convoyPeds.Count == 0) return;

            int now = Game.GameTime;
            if (now - _lastTickMs < 1000) return;
            _lastTickMs = now;

            var player = Game.Player.Character;
            if (player == null || !player.Exists() || player.IsDead) return;

            // Check if player is attacked
            if (now - _lastCombatCheckMs > 2000)
            {
                _lastCombatCheckMs = now;
                bool isAttacked = false;

                try
                {
                    bool damaged = false;
                    try { damaged = Function.Call<bool>(Hash.HAS_ENTITY_BEEN_DAMAGED_BY_ANY_PED, player.Handle); } catch { }
                    
                    if (damaged)
                    {
                        var attackers = World.GetNearbyPeds(player, 50f);
                        foreach (var a in attackers)
                        {
                            if (a != null && a.Exists() && a.IsInCombatAgainst(player))
                            {
                                _currentTarget = a;
                                isAttacked = true;
                                player.ClearLastWeaponDamage();
                                break;
                            }
                        }
                    }
                }
                catch { }

                if (isAttacked && _currentTarget != null && _currentTarget.Exists())
                {
                    Notification.Show("~r~玩家遭到攻击，车队开始集火目标！");
                    foreach (var p in _convoyPeds)
                    {
                        if (p != null && p.Exists() && !p.IsDead)
                        {
                            Function.Call(Hash.TASK_COMBAT_PED, p.Handle, _currentTarget.Handle, 0, 16);
                        }
                    }
                }
            }

            // Clean up dead peds/destroyed vehicles
            _convoyPeds.RemoveAll(p => p == null || !p.Exists() || p.IsDead);
            _convoyVehicles.RemoveAll(v => v == null || !v.Exists() || v.IsDead);

            // Reissue escort task if no target
            if (_currentTarget == null || !_currentTarget.Exists() || _currentTarget.IsDead)
            {
                _currentTarget = null;
                for (int i = 0; i < _convoyVehicles.Count; i++)
                {
                    var v = _convoyVehicles[i];
                    if (v != null && v.Exists() && v.Driver != null && v.Driver.Exists())
                    {
                        // Ensure it's our ped
                        if (_convoyPeds.Contains(v.Driver))
                        {
                            Function.Call(Hash.TASK_VEHICLE_ESCORT, v.Driver.Handle, v.Handle, player.Handle, -1, 20.0f, 1, 10.0f, 0, 15.0f);
                        }
                    }
                }
            }
        }
    }

    public class SummonConvoyEvent { }
    public class DismissConvoyEvent { }
}