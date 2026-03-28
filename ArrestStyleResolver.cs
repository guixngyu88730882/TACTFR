using EF.PoliceMod.Gameplay;
using EF.PoliceMod.Suspects;

namespace EF.PoliceMod.Core
{
    public static class ArrestStyleResolver
    {
        public static ArrestActionStyle GetForHandle(
            int suspectHandle,
            SuspectController suspectController = null,
            SuspectStyleRegistry styleRegistry = null,
            SuspectContextRegistry ctxRegistry = null,
            ArrestActionStyle fallback = ArrestActionStyle.CuffAndLead)
        {
            var effectiveFallback = GetFallbackStyle(suspectController, fallback);

            if (suspectHandle <= 0)
                return effectiveFallback;

            ctxRegistry = ctxRegistry ?? EFCore.Instance?.GetSuspectContextRegistry();
            styleRegistry = styleRegistry ?? EFCore.Instance?.GetSuspectStyleRegistry();

            try
            {
                if (ctxRegistry != null && ctxRegistry.TryGet(suspectHandle, out var ctx) && ctx != null)
                    return ctx.ArrestStyle;
            }
            catch { }

            try
            {
                if (styleRegistry != null)
                {
                    var style = styleRegistry.GetStyleOrDefault(suspectHandle, effectiveFallback);
                    try
                    {
                        ctxRegistry?.SetArrestStyle(suspectHandle, style);
                    }
                    catch { }
                    return style;
                }
            }
            catch { }

            return effectiveFallback;
        }

        public static void SetForHandle(
            int suspectHandle,
            ArrestActionStyle style,
            SuspectStyleRegistry styleRegistry = null,
            SuspectContextRegistry ctxRegistry = null)
        {
            if (suspectHandle <= 0)
                return;

            styleRegistry = styleRegistry ?? EFCore.Instance?.GetSuspectStyleRegistry();
            ctxRegistry = ctxRegistry ?? EFCore.Instance?.GetSuspectContextRegistry();

            try { styleRegistry?.SetStyle(suspectHandle, style); } catch { }
            try { ctxRegistry?.SetArrestStyle(suspectHandle, style); } catch { }
        }

        public static void ClearForHandle(
            int suspectHandle,
            SuspectStyleRegistry styleRegistry = null,
            SuspectContextRegistry ctxRegistry = null)
        {
            if (suspectHandle <= 0)
                return;

            styleRegistry = styleRegistry ?? EFCore.Instance?.GetSuspectStyleRegistry();
            ctxRegistry = ctxRegistry ?? EFCore.Instance?.GetSuspectContextRegistry();

            try { styleRegistry?.ClearStyle(suspectHandle); } catch { }
            try { ctxRegistry?.Remove(suspectHandle); } catch { }
        }

        public static void ClearAll(
            SuspectStyleRegistry styleRegistry = null,
            SuspectContextRegistry ctxRegistry = null)
        {
            styleRegistry = styleRegistry ?? EFCore.Instance?.GetSuspectStyleRegistry();
            ctxRegistry = ctxRegistry ?? EFCore.Instance?.GetSuspectContextRegistry();

            try { styleRegistry?.ClearAll(); } catch { }
            try { ctxRegistry?.Clear(); } catch { }
        }

        private static ArrestActionStyle GetFallbackStyle(
            SuspectController suspectController,
            ArrestActionStyle fallback)
        {
            try
            {
                if (suspectController != null)
                    return suspectController.CurrentArrestStyle;
            }
            catch { }

            return fallback;
        }
    }
}
