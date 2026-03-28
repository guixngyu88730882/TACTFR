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
    public class SpikeStripSystem : ISystem
    {
        private List<Prop> _spikes = new List<Prop>();
        private int _lastTickMs = 0;

        public void Initialize()
        {
            EventBus.Subscribe<DeploySpikeStripEvent>(OnDeploySpikeStrip);
            EventBus.Subscribe<RemoveSpikeStripEvent>(OnRemoveSpikeStrip);
        }

        public void Shutdown()
        {
            ClearAllSpikes();
        }

        private void OnDeploySpikeStrip(DeploySpikeStripEvent e)
        {
            if (_spikes.Count >= 5)
            {
                Notification.Show("~y~最多只能部署 5 个钉刺带");
                return;
            }

            Ped player = Game.Player.Character;
            Vector3 pos = player.Position + player.ForwardVector * 2.0f;
            pos.Z -= 0.5f;

            Model model = new Model("p_stinger_04");
            model.Request(1000);

            Prop spike = World.CreateProp(model, pos, new Vector3(0, 0, player.Heading), true, false);
            if (spike != null && spike.Exists())
            {
                // Put on ground properly
                Function.Call(Hash.PLACE_OBJECT_ON_GROUND_PROPERLY, spike.Handle);
                _spikes.Add(spike);
                Notification.Show("~g~已部署钉刺带");
            }
        }

        private void OnRemoveSpikeStrip(RemoveSpikeStripEvent e)
        {
            ClearAllSpikes();
            Notification.Show("~g~已回收所有钉刺带");
        }

        private void ClearAllSpikes()
        {
            foreach (var spike in _spikes)
            {
                if (spike != null && spike.Exists())
                {
                    spike.Delete();
                }
            }
            _spikes.Clear();
        }

        public void Tick()
        {
            if (_spikes.Count == 0) return;

            int now = Game.GameTime;
            if (now - _lastTickMs < 200) return; // Check every 200ms
            _lastTickMs = now;

            try
            {
                // Since we only want to burst tires of vehicles near spikes
                foreach (var spike in _spikes)
                {
                    if (spike == null || !spike.Exists()) continue;

                    var vehicles = World.GetNearbyVehicles(spike.Position, 3.0f);
                    foreach (var veh in vehicles)
                    {
                        if (veh == null || !veh.Exists()) continue;

                        // Check if any tire is close enough
                        // Burst all tires if the vehicle drives over it
                        if (veh.Speed > 1.0f) // Moving
                        {
                            bool hit = false;
                            if (Function.Call<bool>(Hash.IS_ENTITY_TOUCHING_ENTITY, veh.Handle, spike.Handle))
                            {
                                hit = true;
                            }
                            
                            if (hit)
                            {
                                Function.Call(Hash.SET_VEHICLE_TYRE_BURST, veh.Handle, 0, true, 1000.0f);
                                Function.Call(Hash.SET_VEHICLE_TYRE_BURST, veh.Handle, 1, true, 1000.0f);
                                Function.Call(Hash.SET_VEHICLE_TYRE_BURST, veh.Handle, 4, true, 1000.0f);
                                Function.Call(Hash.SET_VEHICLE_TYRE_BURST, veh.Handle, 5, true, 1000.0f);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[SpikeStripSystem] Tick error: " + ex.Message);
            }
        }
    }

    public class DeploySpikeStripEvent { }
    public class RemoveSpikeStripEvent { }
}