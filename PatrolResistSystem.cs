using EF.PoliceMod.Core;
using GTA;
using GTA.Native;
using System;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 巡逻模式的“反抗处理”独立系统：
    /// - 只订阅 PatrolSuspectResistEvent
    /// - 不接入 SuspectStateHub / SuspectController（不污染任务执法链路）
    /// </summary>
    public sealed class PatrolResistSystem
    {
        public PatrolResistSystem()
        {
            EventBus.Subscribe<PatrolSuspectResistEvent>(OnResist);
            EventBus.Subscribe<DutyEndedEvent>(_ => { /* 预留：未来可清理巡逻临时标记 */ });
        }

        private void OnResist(PatrolSuspectResistEvent e)
        {
            try
            {
                var suspect = e.Suspect;
                var officer = e.Officer;
                if (suspect == null || !suspect.Exists()) return;
                if (officer == null || !officer.Exists()) officer = Game.Player.Character;

                // best-effort：清任务 + 临时武装 + 战斗
                try { suspect.Task.ClearAll(); } catch { }

                // 给一把手枪（仅巡逻玩法；不改任务嫌疑人档案）
                try
                {
                    // 使用 native 直接给武器，避免部分版本 Weapons.Give 的兼容差异
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, suspect.Handle, (uint)WeaponHash.Pistol, 60, false, true);
                }
                catch { }

                try
                {
                    // 开始战斗（简化），不依赖 SuspectController 的状态机
                    suspect.Task.FightAgainst(officer);
                }
                catch { }
            }
            catch { }
        }
    }
}
