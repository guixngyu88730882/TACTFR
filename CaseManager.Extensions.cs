using EF.PoliceMod.Core;

namespace EF.PoliceMod
{
    /// <summary>
    /// 扩展：提供对外的“状态强制广播”入口，避免其它系统需要拿到 CaseManager 内部字段。
    /// </summary>
    public partial class CaseManager
    {
        public void PublishCaseStatusForExternal()
        {
            try
            {
                // 触发一次状态刷新（内部会做变更检测，避免事件噪音）
                // 这里不直接暴露 PublishCaseStatusIfChanged（保持内部实现可改）
                var active = HasActiveCase;
                var lost = IsSuspectLost;
                var started = CaseStartedAtMs;
                var reconUsed = HeliReconUsed;
                var h = CurrentSuspectHandle;
                EventBus.Publish(new CaseStatusChangedEvent(active, lost, started, reconUsed, h));
            }
            catch { }
        }
    }
}
