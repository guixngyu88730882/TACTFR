using GTA;
using GTA.Math;
using System.Collections.Generic;

namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 低耦合嫌疑人上下文（为双人案件做准备）：
    /// - 系统只依赖“当前主嫌疑人”的只读视图
    /// - CaseManager 负责提供实现，并在多人案件时切换 Primary
    /// </summary>
    public interface ISuspectContext
    {
        bool IsOnDuty { get; }
        bool HasActiveCase { get; }

        int CaseStartedAtMs { get; }

        // “当前主嫌疑人”
        int CurrentSuspectHandle { get; }
        Ped GetCurrentSuspectPed();

        // 多嫌疑人（阶段A/B 兼容）：只读列表（handle）
        // 阶段A：通常为 1~2 个；阶段B：可扩展更多
        IReadOnlyList<int> SuspectHandles { get; }

        // 丢失/最后位置（给侦察、HUD、丢失圈等系统读）
        bool IsSuspectLost { get; }
        Vector3 SuspectLastKnownPos { get; }

        // 多嫌疑人：按 handle 查询“丢失/最后位置”（不强制系统了解 CaseManager 内部结构）
        bool TryGetLastKnownPos(int suspectHandle, out Vector3 pos);
        bool IsLost(int suspectHandle);

        // 侦察限制
        bool HeliReconUsed { get; }
        void MarkHeliReconUsed();

        // 侦察找回回填（仍由 CaseManager 实现具体“更新 blip/丢失圈”的细节）
        void MarkSuspectFound(Ped ped, Vector3 pos);
    }
}