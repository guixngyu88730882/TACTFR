using System;
using System.Collections.Generic;

namespace EF.PoliceMod.Core
{
    public static class EventBus
    {

          
              private static readonly Dictionary<Type, List<Delegate>> _listeners =
     new Dictionary<Type, List<Delegate>>();
        private static readonly object _listenersLock = new object();

        public static void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            lock (_listenersLock)
            {
                if (!_listeners.ContainsKey(type)) return;

                _listeners[type].Remove(handler);

                if (_listeners[type].Count == 0)
                    _listeners.Remove(type);
            }
        }


        public static void ClearAll()
        {
            lock (_listenersLock)
            {
                _listeners.Clear();
            }
        }
        public static void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            lock (_listenersLock)
            {
                if (!_listeners.TryGetValue(type, out var list))
                {
                    list = new List<Delegate>();
                    _listeners[type] = list;
                }

                if (!list.Contains(handler))
                {
                    list.Add(handler);
                }
            }
        }

        public static void Publish<T>(T message)
        {
            var type = typeof(T);

#if DEBUG
            ModLog.Info($"Event Published: {type.Name}");
#endif

            List<Delegate> handlersSnapshot;
            lock (_listenersLock)
            {
                if (!_listeners.TryGetValue(type, out var list) || list == null || list.Count == 0)
                {
                    ModLog.Warn($"No listeners for event: {type.Name}");
                    return;
                }
                handlersSnapshot = new List<Delegate>(list); // 复制一份快照（避免在枚举时被修改）
            }

            foreach (var handler in handlersSnapshot)
            {
                try
                {
                    if (handler is Action<T> actionT)
                    {
                        actionT(message);
                    }
                    else
                    {
                        // 兼容性后备：若有人意外以不同签名注册（理论上不应），回退到 DynamicInvoke
                        handler.DynamicInvoke(message);
                    }
                }
                catch (Exception ex)
                {
                    // 记录完整异常，但不要 rethrow —— 避免单个错误把整个 mod / 游戏炸掉
                    ModLog.Error($"Event handler crash: {type.Name} | {ex}");
                }
            }
        }
    }
}
