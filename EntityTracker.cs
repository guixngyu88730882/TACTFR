using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

namespace EF.PoliceMod.Core
{
    public enum EntityType
    {
        Ped,
        Vehicle,
        Blip,
        Prop
    }

    public class TrackedEntity
    {
        public int Handle { get; set; }
        public EntityType Type { get; set; }
        public string Tag { get; set; }
        public int CreatedAt { get; set; }
        public string SourceSystem { get; set; }
    }

    public sealed class EntityTracker
    {
        private static EntityTracker _instance;
        private static readonly object _lock = new object();

        private readonly Dictionary<int, TrackedEntity> _entities = new Dictionary<int, TrackedEntity>();
        private readonly Dictionary<string, HashSet<int>> _byTag = new Dictionary<string, HashSet<int>>();
        private readonly Dictionary<string, HashSet<int>> _bySource = new Dictionary<string, HashSet<int>>();
        private readonly Dictionary<int, Blip> _blipCache = new Dictionary<int, Blip>();

        public static EntityTracker Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new EntityTracker();
                    }
                }
                return _instance;
            }
        }

        private EntityTracker() { }

        public void Register(Ped ped, string tag, string sourceSystem)
        {
            if (ped == null || !ped.Exists()) return;
            RegisterInternal(ped.Handle, EntityType.Ped, tag, sourceSystem);
        }

        public void Register(Vehicle vehicle, string tag, string sourceSystem)
        {
            if (vehicle == null || !vehicle.Exists()) return;
            RegisterInternal(vehicle.Handle, EntityType.Vehicle, tag, sourceSystem);
        }

        public void Register(Blip blip, string tag, string sourceSystem)
        {
            if (blip == null || !blip.Exists()) return;
            lock (_lock)
            {
                _blipCache[blip.Handle] = blip;
            }
            RegisterInternal(blip.Handle, EntityType.Blip, tag, sourceSystem);
        }

        private void RegisterInternal(int handle, EntityType type, string tag, string sourceSystem)
        {
            lock (_lock)
            {
                if (_entities.ContainsKey(handle)) return;

                var tracked = new TrackedEntity
                {
                    Handle = handle,
                    Type = type,
                    Tag = tag ?? "default",
                    CreatedAt = Game.GameTime,
                    SourceSystem = sourceSystem ?? "unknown"
                };

                _entities[handle] = tracked;

                if (!_byTag.ContainsKey(tracked.Tag))
                    _byTag[tracked.Tag] = new HashSet<int>();
                _byTag[tracked.Tag].Add(handle);

                if (!_bySource.ContainsKey(tracked.SourceSystem))
                    _bySource[tracked.SourceSystem] = new HashSet<int>();
                _bySource[tracked.SourceSystem].Add(handle);

                ModLog.Info($"[EntityTracker] Registered: handle={handle}, type={type}, tag={tag}, source={sourceSystem}");
            }
        }

        public void Unregister(int handle)
        {
            lock (_lock)
            {
                if (!_entities.TryGetValue(handle, out var tracked)) return;

                _entities.Remove(handle);

                if (_byTag.TryGetValue(tracked.Tag, out var tagSet))
                    tagSet.Remove(handle);

                if (_bySource.TryGetValue(tracked.SourceSystem, out var sourceSet))
                    sourceSet.Remove(handle);

                ModLog.Info($"[EntityTracker] Unregistered: handle={handle}");
            }
        }

        public void CleanupEntity(int handle)
        {
            TrackedEntity tracked = null;
            lock (_lock)
            {
                if (!_entities.TryGetValue(handle, out tracked)) return;
                _entities.Remove(handle);

                if (_byTag.TryGetValue(tracked.Tag, out var tagSet))
                    tagSet.Remove(handle);

                if (_bySource.TryGetValue(tracked.SourceSystem, out var sourceSet))
                    sourceSet.Remove(handle);
            }

            if (tracked != null)
            {
                SafeDeleteEntity(tracked);
            }
        }

        public void CleanupByTag(string tag)
        {
            List<int> toCleanup = null;
            lock (_lock)
            {
                if (!_byTag.TryGetValue(tag, out var handles)) return;
                toCleanup = new List<int>(handles);
            }

            ModLog.Info($"[EntityTracker] CleanupByTag: tag={tag}, count={toCleanup.Count}");
            foreach (var handle in toCleanup)
            {
                CleanupEntity(handle);
            }
        }

        public void CleanupBySource(string sourceSystem)
        {
            List<int> toCleanup = null;
            lock (_lock)
            {
                if (!_bySource.TryGetValue(sourceSystem, out var handles)) return;
                toCleanup = new List<int>(handles);
            }

            ModLog.Info($"[EntityTracker] CleanupBySource: source={sourceSystem}, count={toCleanup.Count}");
            foreach (var handle in toCleanup)
            {
                CleanupEntity(handle);
            }
        }

        public void CleanupAll()
        {
            List<TrackedEntity> allEntities;
            lock (_lock)
            {
                allEntities = new List<TrackedEntity>(_entities.Values);
                _entities.Clear();
                _byTag.Clear();
                _bySource.Clear();
                _blipCache.Clear();
            }

            ModLog.Info($"[EntityTracker] CleanupAll: count={allEntities.Count}");
            foreach (var tracked in allEntities)
            {
                SafeDeleteEntity(tracked);
            }
        }

        public IReadOnlyList<int> GetHandlesByTag(string tag)
        {
            lock (_lock)
            {
                if (!_byTag.TryGetValue(tag, out var handles))
                    return new List<int>();
                return new List<int>(handles);
            }
        }

        public IReadOnlyList<int> GetHandlesBySource(string sourceSystem)
        {
            lock (_lock)
            {
                if (!_bySource.TryGetValue(sourceSystem, out var handles))
                    return new List<int>();
                return new List<int>(handles);
            }
        }

        public int GetCount()
        {
            lock (_lock)
            {
                return _entities.Count;
            }
        }

        public int GetCountByTag(string tag)
        {
            lock (_lock)
            {
                if (!_byTag.TryGetValue(tag, out var handles))
                    return 0;
                return handles.Count;
            }
        }

        private void SafeDeleteEntity(TrackedEntity tracked)
        {
            try
            {
                switch (tracked.Type)
                {
                    case EntityType.Ped:
                        var ped = Entity.FromHandle(tracked.Handle) as Ped;
                        if (ped != null && ped.Exists())
                        {
                            try { ped.Task.ClearAll(); } catch { }
                            try { ped.MarkAsNoLongerNeeded(); } catch { }
                            try { ped.Delete(); } catch { }
                        }
                        break;

                    case EntityType.Vehicle:
                        var veh = Entity.FromHandle(tracked.Handle) as Vehicle;
                        if (veh != null && veh.Exists())
                        {
                            try { veh.MarkAsNoLongerNeeded(); } catch { }
                            try { veh.Delete(); } catch { }
                        }
                        break;

                    case EntityType.Blip:
                        Blip blip = null;
                        lock (_lock)
                        {
                            _blipCache.TryGetValue(tracked.Handle, out blip);
                            _blipCache.Remove(tracked.Handle);
                        }
                        if (blip != null && blip.Exists())
                        {
                            try { blip.Delete(); } catch { }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ModLog.Error($"[EntityTracker] SafeDeleteEntity failed: handle={tracked.Handle}, type={tracked.Type}, ex={ex.Message}");
            }
        }

        public static void Reset()
        {
            lock (_lock)
            {
                _instance?.CleanupAll();
                _instance = null;
            }
        }
    }
}
