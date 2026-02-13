using EF.PoliceMod.Core;
using EF.PoliceMod.Gameplay;
using GTA;
using GTA.Native;
using System;

namespace EF.PoliceMod.Executors
{
    /// <summary>
    /// 跟随/押送过程中的“嫌疑人逃跑”逻辑封装。
    /// </summary>
    internal static class SuspectEscapeOps
    {
        public static bool TryEscapeOnFollowRequest(
            SuspectController suspectController,
            SuspectStateHub stateHub,
            Ped suspect,
            Random rand,
            double chance
        )
        {
            if (suspectController == null) return false;
            if (stateHub == null) return false;
            if (suspect == null || !suspect.Exists()) return false;
            if (suspect.IsDead) return false;

            try
            {
                if (!suspectController.IsCompliant) return false;

                // ★日志对应：你这份 log 里多次出现
                // [13:24:22] [Escort][Follow] Escape triggered ...
                // 对“被拷线/已上拷”的嫌疑人，按 G 不应再触发逃跑概率，否则会把 G->E 整条链路打断。
                try
                {
                    if (suspectController.CurrentArrestStyle == ArrestActionStyle.CuffAndLead)
                        return false;
                }
                catch { }
            }
            catch { return false; }

            double roll = 1.0;
            try { roll = rand != null ? rand.NextDouble() : 1.0; } catch { roll = 1.0; }
            if (roll >= chance) return false;

            ModLog.Info($"[Escort][Follow] Escape triggered (roll={roll:F3}) suspect={suspect.Handle}");

            // 解除手铐并逃跑：必须重新锁定+拘捕
            try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_ENABLE_BOUND_ANKLES, suspect.Handle, false); } catch { }
            try { Function.Call(Hash.SET_PED_CAN_RAGDOLL, suspect.Handle, true); } catch { }
            try { Function.Call(Hash.CLEAR_PED_TASKS, suspect.Handle); } catch { }
            try { Function.Call(Hash.TASK_SMART_FLEE_PED, suspect.Handle, Game.Player.Character.Handle, 100f, -1, false, false); } catch { }

            // 释放控制并通知状态机清空
            try { suspectController.ReleaseControl(suspect); } catch { }
            try { stateHub.ChangeState(SuspectState.None); } catch { }

            try { EventBus.Publish(new SuspectEscapedEvent(suspect.Handle)); } catch { }
            return true;
        }
    }
}