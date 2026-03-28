using GTA;
using System.Collections.Generic;
using Keys = System.Windows.Forms.Keys;

namespace EF.PoliceMod.Core
{
    public static class InputService
    {
        public static bool IsKeyDown(Keys key)
        {
            return Game.IsKeyPressed(key);
        }

        public static bool IsAnyKeyDown(Keys primary, params Keys[] fallbacks)
        {
            if (IsKeyDown(primary)) return true;
            if (fallbacks == null) return false;
            for (int i = 0; i < fallbacks.Length; i++)
            {
                if (IsKeyDown(fallbacks[i])) return true;
            }
            return false;
        }

        public static bool IsMenuUpDown() => IsAnyKeyDown(KeyBindings.MenuUp, Keys.Up);
        public static bool IsMenuDownDown() => IsAnyKeyDown(KeyBindings.MenuDown, Keys.Down);
        public static bool IsMenuLeftDown() => IsAnyKeyDown(KeyBindings.MenuLeft, Keys.Left);
        public static bool IsMenuRightDown() => IsAnyKeyDown(KeyBindings.MenuRight, Keys.Right);
        public static bool IsMenuConfirmDown() => IsAnyKeyDown(KeyBindings.MenuConfirm, Keys.Return, Keys.Space);
        public static bool IsMenuCancelDown() => IsAnyKeyDown(KeyBindings.MenuCancel, Keys.Escape);
        public static bool IsMenuRefreshDown() => IsAnyKeyDown(KeyBindings.MenuRefresh, Keys.R);

        public static string GetDisplayName(Keys primary, params Keys[] fallbacks)
        {
            var names = new List<string>();
            AddDisplayName(names, primary);
            if (fallbacks != null)
            {
                for (int i = 0; i < fallbacks.Length; i++)
                    AddDisplayName(names, fallbacks[i]);
            }
            return string.Join("/", names.ToArray());
        }

        private static void AddDisplayName(List<string> names, Keys key)
        {
            string name = KeyBindings.GetKeyDisplayName(key);
            if (string.IsNullOrWhiteSpace(name)) return;
            if (!names.Contains(name)) names.Add(name);
        }
    }
}
