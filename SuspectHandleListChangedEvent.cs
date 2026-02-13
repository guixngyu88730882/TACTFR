using System.Collections.Generic;

namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 多嫌疑人：嫌疑人列表变更（低耦合广播）
    /// 任何 HUD/UI/系统需要显示 2 个嫌疑人时，只订阅此事件，不直接耦合 CaseManager。
    /// </summary>
    public sealed class SuspectHandleListChangedEvent
    {
        public IReadOnlyList<int> Handles { get; }

        public SuspectHandleListChangedEvent(IReadOnlyList<int> handles)
        {
            Handles = handles;
        }
    }
}