using GTA.Native;

namespace EF.PoliceMod
{
    internal static class SoundFx
    {
        public static void DutyStart()
        {
            // 尽量“叮咚/提示”明显：多重兜底
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CHALLENGE_UNLOCKED", "HUD_AWARDS", false); } catch { }
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "MP_AWARD", "HUD_AWARDS", false); } catch { }
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET", false); } catch { }
        }

        public static void ArrestConfirmed()
        {
            // 对讲机/确认：多重兜底（你后续要换成真正警用对讲机音效时，只需要改这里）
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONFIRM_BEEP", "HUD_MINI_GAME_SOUNDSET", false); } catch { }
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "TIMER_STOP", "HUD_MINI_GAME_SOUNDSET", false); } catch { }
            try { Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "MP_AWARD", "HUD_AWARDS", false); } catch { }
        }
    }
}