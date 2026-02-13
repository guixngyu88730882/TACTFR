using EF.PoliceMod.Core;
using GTA;

namespace EF.PoliceMod.Systems
{
    /// <summary>
    /// 巡逻模式逃跑处理：独立于任务执法链路。
    /// </summary>
    public sealed class PatrolFleeSystem
    {
        public PatrolFleeSystem()
        {
            EventBus.Subscribe<PatrolSuspectFleeEvent>(OnFlee);
            EventBus.Subscribe<DutyEndedEvent>(_ => { /* 预留 */ });
        }

        private void OnFlee(PatrolSuspectFleeEvent e)
        {
            try
            {
                var suspect = e.Suspect;
                var officer = e.Officer;
                if (suspect == null || !suspect.Exists()) return;
                if (officer == null || !officer.Exists()) officer = Game.Player.Character;

                try { suspect.Task.ClearAll(); } catch { }
                try { suspect.Task.FleeFrom(officer); } catch { }
            }
            catch { }
        }
    }
}
