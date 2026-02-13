namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 终端可用性状态变化：由案件/值勤系统发布，UI 侧只读订阅，避免直接依赖 CaseManager。
    /// </summary>
    public struct TerminalAccessChangedEvent
    {
        public bool CanOpen { get; }
        public TerminalAccessChangedEvent(bool canOpen) { CanOpen = canOpen; }
    }
}
