using GTA;
using GTA.Native;

namespace EF.PoliceMod.Executors
{
    internal static class CuffedPoseOps
    {
        private const string CLIPSET_PRISONER_CUFFED = "move_m@prisoner_cuffed";
        private const string CUFF_DICT = "mp_arresting";
        private const string CUFF_IDLE = "idle";

        public static void EnsureClipset(Ped suspect, ref int lastRequestMs, ref int lastApplyMs, int requestCooldownMs, int reapplyCooldownMs)
        {
            if (suspect == null || !suspect.Exists()) return;
            if (suspect.IsDead) return;

            try
            {
                int now = Game.GameTime;
                if (now - lastRequestMs > requestCooldownMs)
                {
                    lastRequestMs = now;
                    try { Function.Call(Hash.REQUEST_ANIM_SET, CLIPSET_PRISONER_CUFFED); } catch { }
                }
                
                bool loaded = false;
                try { loaded = Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, CLIPSET_PRISONER_CUFFED); } catch { loaded = false; }
                if (!loaded) return;

                if (now - lastApplyMs < reapplyCooldownMs) return;
                lastApplyMs = now;

                try { Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, suspect.Handle, CLIPSET_PRISONER_CUFFED, 0.25f); } catch { }
            }
            catch { }
        }

        public static void EnsureUpperBodyIdle(Ped suspect, ref int lastPoseMs, int cooldownMs)
        {
            if (suspect == null || !suspect.Exists()) return;
            if (suspect.IsDead) return;

            try
            {
                int now = Game.GameTime;
                if (now - lastPoseMs < cooldownMs) return;
                lastPoseMs = now;

                try { Function.Call(Hash.SET_ENABLE_HANDCUFFS, suspect.Handle, true); } catch { }

                bool playing = false;
                try { playing = Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, suspect.Handle, CUFF_DICT, CUFF_IDLE, 3); } catch { playing = false; }
                if (playing) return;

                try { Function.Call(Hash.REQUEST_ANIM_DICT, CUFF_DICT); } catch { }
                try
                {
                    if (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, CUFF_DICT))
                        return;
                }
                catch { return; }

                // flags=49: Loop + OnlyAnimateUpperBody
                try { Function.Call(Hash.TASK_PLAY_ANIM, suspect.Handle, CUFF_DICT, CUFF_IDLE, 4.0f, -4.0f, -1, 49, 0.0f, false, false, false); } catch { }
            }
            catch { }
        }
    }
}