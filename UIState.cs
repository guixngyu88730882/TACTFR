namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 全局 UI 状态标记
    /// 用于 InputManager 判断是否由 UI 接管输入
    /// + 心跳/自愈：防止异常路径导致标记卡死，让输入系统永久 return。
    /// </summary>
    public static class UIState
    {
        public static bool IsPoliceTerminalOpen = false;
        public static bool IsDispatchMenuOpen = false;
        public static bool IsArrestMenuOpen = false;
        public static bool IsUniformMenuOpen = false;
        public static bool IsOfficerSquadMenuOpen = false;

        // 最近一次“UI 正常在跑”的心跳时间（Game.GameTime ms）
        private static int _policeTerminalHeartbeatAtMs = 0;
        private static int _dispatchMenuHeartbeatAtMs = 0;
        private static int _arrestMenuHeartbeatAtMs = 0;
        private static int _uniformMenuHeartbeatAtMs = 0;
        private static int _officerSquadMenuHeartbeatAtMs = 0;

        public static void MarkPoliceTerminalOpen(int nowMs)
        {
            IsPoliceTerminalOpen = true;
            _policeTerminalHeartbeatAtMs = nowMs;
        }

        public static void MarkPoliceTerminalClosed()
        {
            IsPoliceTerminalOpen = false;
            _policeTerminalHeartbeatAtMs = 0;
        }

        public static void BeatPoliceTerminal(int nowMs)
        {
            if (IsPoliceTerminalOpen) _policeTerminalHeartbeatAtMs = nowMs;
        }

        public static void MarkDispatchMenuOpen(int nowMs)
        {
            IsDispatchMenuOpen = true;
            _dispatchMenuHeartbeatAtMs = nowMs;
        }

        public static void MarkDispatchMenuClosed()
        {
            IsDispatchMenuOpen = false;
            _dispatchMenuHeartbeatAtMs = 0;
        }

        public static void BeatDispatchMenu(int nowMs)
        {
            if (IsDispatchMenuOpen) _dispatchMenuHeartbeatAtMs = nowMs;
        }

        public static void MarkArrestMenuOpen(int nowMs)
        {
            IsArrestMenuOpen = true;
            _arrestMenuHeartbeatAtMs = nowMs;
        }

        public static void MarkArrestMenuClosed()
        {
            IsArrestMenuOpen = false;
            _arrestMenuHeartbeatAtMs = 0;
        }

        public static void BeatArrestMenu(int nowMs)
        {
            if (IsArrestMenuOpen) _arrestMenuHeartbeatAtMs = nowMs;
        }

        public static void MarkUniformMenuOpen(int nowMs)
        {
            IsUniformMenuOpen = true;
            _uniformMenuHeartbeatAtMs = nowMs;
        }

        public static void MarkUniformMenuClosed()
        {
            IsUniformMenuOpen = false;
            _uniformMenuHeartbeatAtMs = 0;
        }

        public static void BeatUniformMenu(int nowMs)
        {
            if (IsUniformMenuOpen) _uniformMenuHeartbeatAtMs = nowMs;
        }

        public static void MarkOfficerSquadMenuOpen(int nowMs)
        {
            IsOfficerSquadMenuOpen = true;
            _officerSquadMenuHeartbeatAtMs = nowMs;
        }

        public static void MarkOfficerSquadMenuClosed()
        {
            IsOfficerSquadMenuOpen = false;
            _officerSquadMenuHeartbeatAtMs = 0;
        }

        public static void BeatOfficerSquadMenu(int nowMs)
        {
            if (IsOfficerSquadMenuOpen) _officerSquadMenuHeartbeatAtMs = nowMs;
        }



        /// <summary>
        /// 自愈：如果某 UI 标记为 open，但长时间没有心跳，说明 UI 可能已异常退出/未继续 Tick，
        /// 这里把标记复位，避免 InputManager 永久 return。
        /// </summary>
        public static void AutoRecover(int nowMs, int timeoutMs = 1500)
        {
            try
            {
                if (IsPoliceTerminalOpen && _policeTerminalHeartbeatAtMs > 0 && nowMs - _policeTerminalHeartbeatAtMs > timeoutMs)
                {
                    ModLog.Warn("[UIState] PoliceTerminal stuck open -> auto reset");
                    MarkPoliceTerminalClosed();
                }

                if (IsDispatchMenuOpen && _dispatchMenuHeartbeatAtMs > 0 && nowMs - _dispatchMenuHeartbeatAtMs > timeoutMs)
                {
                    ModLog.Warn("[UIState] DispatchMenu stuck open -> auto reset");
                    MarkDispatchMenuClosed();
                }

                if (IsArrestMenuOpen && _arrestMenuHeartbeatAtMs > 0 && nowMs - _arrestMenuHeartbeatAtMs > timeoutMs)
                {
                    ModLog.Warn("[UIState] ArrestMenu stuck open -> auto reset");
                    MarkArrestMenuClosed();
                }

                if (IsUniformMenuOpen && _uniformMenuHeartbeatAtMs > 0 && nowMs - _uniformMenuHeartbeatAtMs > timeoutMs)
                {
                    ModLog.Warn("[UIState] UniformMenu stuck open -> auto reset");
                    MarkUniformMenuClosed();
                }

                if (IsOfficerSquadMenuOpen && _officerSquadMenuHeartbeatAtMs > 0 && nowMs - _officerSquadMenuHeartbeatAtMs > timeoutMs)
                {
                    ModLog.Warn("[UIState] OfficerSquadMenu stuck open -> auto reset");
                    MarkOfficerSquadMenuClosed();
                }
            }
            catch { }
        }
    }
}
