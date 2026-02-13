using EF.PoliceMod.Core;
using EF.PoliceMod.Gameplay;
using EF.PoliceMod;
using GTA;
using GTA.Native;
using System;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 执勤/交付后的“无遗留”统一清理服务（低耦合）：
    /// - 订阅 DutyEndedEvent / SuspectDeliveredEvent
    /// - 把散落在 EFCore 里的强制清理收拢到一个地方
    /// - 目标：稳定优先，宁可多清一点，也不要残留影响下一局/下一案
    /// </summary>
    public sealed class DutyCleanupService
    {
        private readonly LockTargetSystem _lockTargetSystem;
        private readonly SuspectController _suspectController;
        private readonly SuspectStateHub _suspectStateHub;
        private readonly BehaviorKeeper _behaviorKeeper;

        public DutyCleanupService(
            LockTargetSystem lockTargetSystem,
            SuspectController suspectController,
            SuspectStateHub suspectStateHub,
            BehaviorKeeper behaviorKeeper)
        {
            _lockTargetSystem = lockTargetSystem;
            _suspectController = suspectController;
            _suspectStateHub = suspectStateHub;
            _behaviorKeeper = behaviorKeeper;

            EventBus.Subscribe<DutyEndedEvent>(_ => Cleanup("DutyEnded"));
            EventBus.Subscribe<SuspectDeliveredEvent>(_ => Cleanup("SuspectDelivered"));
        }

        private void Cleanup(string reason)
        {
            try { _lockTargetSystem?.ForceClear(); } catch { }
            try { _suspectController?.ForceClear(); } catch { }
            try { _suspectStateHub?.ChangeState(SuspectState.None); } catch { }
            try { _behaviorKeeper?.ForceClear(reason); } catch { }

            try
            {
                var player = Game.Player.Character;
                if (player != null && player.Exists())
                {
                    // 还原步态（比如拎人/演出导致的 clipset 残留）
                    try { Function.Call(Hash.RESET_PED_MOVEMENT_CLIPSET, player.Handle, 0.25f); } catch { }

                    // 保险：部分 UI/系统可能冻结过玩家（例如终端），这里再补一刀解冻
                    try { Function.Call(Hash.FREEZE_ENTITY_POSITION, player.Handle, false); } catch { }
                    try { Function.Call(Hash.SET_PLAYER_CONTROL, Game.Player.Handle, true, 0); } catch { }
                    try { Function.Call(Hash.ENABLE_ALL_CONTROL_ACTIONS, 0); } catch { }
                }
            }
            catch { }
        }
    }
}
