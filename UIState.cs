using System;
using System.Collections.Generic;

namespace EF.PoliceMod.Core
{
    public interface IUiSession
    {
        string SessionId { get; }
        bool IsOpen { get; }
        void ForceClose(string reason);
    }

    public static class UIState
    {
        public const string PoliceTerminalSession = "PoliceTerminal";
        public const string DispatchMenuSession = "DispatchMenu";
        public const string ArrestMenuSession = "ArrestMenu";
        public const string UniformMenuSession = "UniformMenu";
        public const string OfficerSquadMenuSession = "OfficerSquadMenu";
        public const string PatrolMenuSession = "PatrolMenu";

        private sealed class UiSessionEntry
        {
            public bool IsOpen;
            public int HeartbeatAtMs;
            public int OpenedAtMs;
            public IUiSession Session;
        }

        private static readonly Dictionary<string, UiSessionEntry> _entries = new Dictionary<string, UiSessionEntry>(StringComparer.Ordinal)
        {
            [PoliceTerminalSession] = new UiSessionEntry(),
            [DispatchMenuSession] = new UiSessionEntry(),
            [ArrestMenuSession] = new UiSessionEntry(),
            [UniformMenuSession] = new UiSessionEntry(),
            [OfficerSquadMenuSession] = new UiSessionEntry(),
            [PatrolMenuSession] = new UiSessionEntry(),
        };

        public static bool IsPoliceTerminalOpen => IsOpen(PoliceTerminalSession);
        public static bool IsDispatchMenuOpen => IsOpen(DispatchMenuSession);
        public static bool IsArrestMenuOpen => IsOpen(ArrestMenuSession);
        public static bool IsUniformMenuOpen => IsOpen(UniformMenuSession);
        public static bool IsOfficerSquadMenuOpen => IsOpen(OfficerSquadMenuSession);
        public static bool IsPatrolMenuOpen => IsOpen(PatrolMenuSession);

        public static bool IsAnyUiOpen =>
            IsPoliceTerminalOpen
            || IsDispatchMenuOpen
            || IsArrestMenuOpen
            || IsUniformMenuOpen
            || IsOfficerSquadMenuOpen
            || IsPatrolMenuOpen;

        public static void RegisterSession(string sessionId, IUiSession session)
        {
            if (string.IsNullOrWhiteSpace(sessionId) || session == null) return;
            var entry = GetOrCreate(sessionId);
            entry.Session = session;
            ModLog.Info($"[UIState] Registered session: {sessionId}");
        }

        public static void UnregisterSession(string sessionId, IUiSession session = null)
        {
            if (string.IsNullOrWhiteSpace(sessionId)) return;
            var entry = GetOrCreate(sessionId);
            if (session == null || ReferenceEquals(entry.Session, session))
                entry.Session = null;
        }

        public static bool AnyOtherUiOpen(string exceptSessionId)
        {
            foreach (var pair in _entries)
            {
                if (pair.Key == exceptSessionId) continue;
                if (pair.Value != null && pair.Value.IsOpen)
                    return true;
            }
            return false;
        }

        public static string GetBusyUiName(string exceptSessionId = null)
        {
            foreach (var pair in _entries)
            {
                if (pair.Key == exceptSessionId) continue;
                if (pair.Value != null && pair.Value.IsOpen)
                    return pair.Key;
            }
            return string.Empty;
        }

        public static void ForceClose(string sessionId, string reason)
        {
            if (string.IsNullOrWhiteSpace(sessionId)) return;
            var entry = GetOrCreate(sessionId);
            try
            {
                if (entry.Session != null)
                {
                    entry.Session.ForceClose(reason);
                }
            }
            catch (Exception ex)
            {
                ModLog.Error($"[UIState] ForceClose failed for {sessionId}: {ex}");
            }
            finally
            {
                MarkClosed(sessionId);
            }
        }

        public static void MarkPoliceTerminalOpen(int nowMs) => MarkOpen(PoliceTerminalSession, nowMs);
        public static void MarkPoliceTerminalClosed() => MarkClosed(PoliceTerminalSession);
        public static void BeatPoliceTerminal(int nowMs) => Beat(PoliceTerminalSession, nowMs);

        public static void MarkDispatchMenuOpen(int nowMs) => MarkOpen(DispatchMenuSession, nowMs);
        public static void MarkDispatchMenuClosed() => MarkClosed(DispatchMenuSession);
        public static void BeatDispatchMenu(int nowMs) => Beat(DispatchMenuSession, nowMs);

        public static void MarkArrestMenuOpen(int nowMs) => MarkOpen(ArrestMenuSession, nowMs);
        public static void MarkArrestMenuClosed() => MarkClosed(ArrestMenuSession);
        public static void BeatArrestMenu(int nowMs) => Beat(ArrestMenuSession, nowMs);

        public static void MarkUniformMenuOpen(int nowMs) => MarkOpen(UniformMenuSession, nowMs);
        public static void MarkUniformMenuClosed() => MarkClosed(UniformMenuSession);
        public static void BeatUniformMenu(int nowMs) => Beat(UniformMenuSession, nowMs);

        public static void MarkOfficerSquadMenuOpen(int nowMs) => MarkOpen(OfficerSquadMenuSession, nowMs);
        public static void MarkOfficerSquadMenuClosed() => MarkClosed(OfficerSquadMenuSession);
        public static void BeatOfficerSquadMenu(int nowMs) => Beat(OfficerSquadMenuSession, nowMs);

        public static void MarkPatrolMenuOpen(int nowMs) => MarkOpen(PatrolMenuSession, nowMs);
        public static void MarkPatrolMenuClosed() => MarkClosed(PatrolMenuSession);
        public static void BeatPatrolMenu(int nowMs) => Beat(PatrolMenuSession, nowMs);

        public static void AutoRecover(int nowMs, int heartbeatTimeoutMs = 1500, int maxLifetimeMs = 15000)
        {
            TryRecover(PoliceTerminalSession, nowMs, heartbeatTimeoutMs, 120000);
            TryRecover(DispatchMenuSession, nowMs, heartbeatTimeoutMs, maxLifetimeMs);
            TryRecover(ArrestMenuSession, nowMs, heartbeatTimeoutMs, maxLifetimeMs);
            TryRecover(UniformMenuSession, nowMs, heartbeatTimeoutMs, maxLifetimeMs);
            TryRecover(OfficerSquadMenuSession, nowMs, heartbeatTimeoutMs, maxLifetimeMs);
            TryRecover(PatrolMenuSession, nowMs, heartbeatTimeoutMs, maxLifetimeMs);
        }

        private static bool IsOpen(string sessionId)
        {
            return GetOrCreate(sessionId).IsOpen;
        }

        private static void MarkOpen(string sessionId, int nowMs)
        {
            var entry = GetOrCreate(sessionId);
            entry.IsOpen = true;
            entry.HeartbeatAtMs = nowMs;
            entry.OpenedAtMs = nowMs;
        }

        private static void MarkClosed(string sessionId)
        {
            var entry = GetOrCreate(sessionId);
            entry.IsOpen = false;
            entry.HeartbeatAtMs = 0;
            entry.OpenedAtMs = 0;
        }

        private static void Beat(string sessionId, int nowMs)
        {
            var entry = GetOrCreate(sessionId);
            if (entry.IsOpen)
                entry.HeartbeatAtMs = nowMs;
        }

        private static void TryRecover(string sessionId, int nowMs, int heartbeatTimeoutMs, int maxLifetimeMs)
        {
            var entry = GetOrCreate(sessionId);
            if (!entry.IsOpen) return;

            int openGraceMs = 2000;
            bool withinOpenGrace = entry.OpenedAtMs > 0 && (nowMs - entry.OpenedAtMs) < openGraceMs;

            bool heartbeatStale = false;
            if (!withinOpenGrace)
            {
                heartbeatStale = entry.HeartbeatAtMs > 0 && nowMs - entry.HeartbeatAtMs > heartbeatTimeoutMs;
            }

            bool tooOld = entry.OpenedAtMs > 0 && nowMs - entry.OpenedAtMs > maxLifetimeMs;

            if (!heartbeatStale && !tooOld) return;

            ModLog.Warn($"[UIState] {sessionId} 自动回收 (stale={heartbeatStale}, tooOld={tooOld}, age={nowMs - entry.OpenedAtMs}ms)");

            try
            {
                if (entry.Session != null && entry.Session.IsOpen)
                {
                    entry.Session.ForceClose($"AutoRecover:{sessionId}");
                }
            }
            catch (Exception ex)
            {
                ModLog.Error($"[UIState] AutoRecover force-close failed for {sessionId}: {ex}");
            }

            MarkClosed(sessionId);
        }

        private static UiSessionEntry GetOrCreate(string sessionId)
        {
            if (!_entries.TryGetValue(sessionId, out var entry))
            {
                entry = new UiSessionEntry();
                _entries[sessionId] = entry;
            }
            return entry;
        }
    }
}
