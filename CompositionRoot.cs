using EF.PoliceMod.Core.Contracts;
using System;
using System.Collections.Generic;

namespace EF.PoliceMod.Core
{
    public sealed class CompositionRoot
    {
        private static CompositionRoot _instance;
        private static readonly object _lock = new object();

        private readonly List<ISystem> _systems = new List<ISystem>();
        private readonly Dictionary<Type, ISystem> _systemByType = new Dictionary<Type, ISystem>();
        private bool _initialized = false;
        private bool _shutdown = false;

        public static CompositionRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new CompositionRoot();
                    }
                }
                return _instance;
            }
        }

        private CompositionRoot() { }

        public void RegisterSystem(ISystem system)
        {
            if (system == null) return;
            lock (_lock)
            {
                if (_systemByType.ContainsKey(system.GetType())) return;
                _systems.Add(system);
                _systemByType[system.GetType()] = system;
                ModLog.Info($"[CompositionRoot] Registered system: {system.GetType().Name}");
            }
        }

        public T GetSystem<T>() where T : class, ISystem
        {
            lock (_lock)
            {
                foreach (var system in _systems)
                {
                    if (system is T typed)
                        return typed;
                }
                return null;
            }
        }

        public void InitializeAll()
        {
            lock (_lock)
            {
                if (_initialized)
                {
                    ModLog.Warn("[CompositionRoot] Already initialized, skipping");
                    return;
                }

                ModLog.Info($"[CompositionRoot] Initializing {_systems.Count} systems...");

                foreach (var system in _systems)
                {
                    try
                    {
                        system.Initialize();
                        ModLog.Info($"[CompositionRoot] Initialized: {system.GetType().Name}");
                    }
                    catch (Exception ex)
                    {
                        ModLog.Error($"[CompositionRoot] Initialize failed for {system.GetType().Name}: {ex}");
                    }
                }

                _initialized = true;
                ModLog.Info("[CompositionRoot] All systems initialized");
            }
        }

        public void TickAll()
        {
            if (!_initialized || _shutdown) return;

            foreach (var system in _systems)
            {
                try
                {
                    system.Tick();
                }
                catch (Exception ex)
                {
                    ModLog.Error($"[CompositionRoot] Tick failed for {system.GetType().Name}: {ex}");
                }
            }
        }

        public void ShutdownAll()
        {
            lock (_lock)
            {
                if (_shutdown)
                {
                    ModLog.Warn("[CompositionRoot] Already shutdown, skipping");
                    return;
                }

                ModLog.Info($"[CompositionRoot] Shutting down {_systems.Count} systems...");

                for (int i = _systems.Count - 1; i >= 0; i--)
                {
                    var system = _systems[i];
                    try
                    {
                        system.Shutdown();
                        ModLog.Info($"[CompositionRoot] Shutdown: {system.GetType().Name}");
                    }
                    catch (Exception ex)
                    {
                        ModLog.Error($"[CompositionRoot] Shutdown failed for {system.GetType().Name}: {ex}");
                    }
                }

                try
                {
                    EntityTracker.Instance.CleanupAll();
                    ModLog.Info("[CompositionRoot] EntityTracker cleanup completed");
                }
                catch (Exception ex)
                {
                    ModLog.Error($"[CompositionRoot] EntityTracker cleanup failed: {ex}");
                }

                try
                {
                    EventBus.ClearAll();
                    ModLog.Info("[CompositionRoot] EventBus cleared");
                }
                catch (Exception ex)
                {
                    ModLog.Error($"[CompositionRoot] EventBus clear failed: {ex}");
                }

                _shutdown = true;
                ModLog.Info("[CompositionRoot] All systems shutdown completed");
            }
        }

        public void Reset()
        {
            lock (_lock)
            {
                if (!_shutdown)
                {
                    ShutdownAll();
                }

                _systems.Clear();
                _systemByType.Clear();
                _initialized = false;
                _shutdown = false;

                ModLog.Info("[CompositionRoot] Reset completed");
            }
        }

        public static void Destroy()
        {
            lock (_lock)
            {
                _instance?.ShutdownAll();
                _instance = null;
            }
        }

        public bool IsInitialized => _initialized;
        public bool IsShutdown => _shutdown;
        public int SystemCount => _systems.Count;
    }
}
