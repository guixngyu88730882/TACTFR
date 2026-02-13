using GTA;
using GTA.UI;
using EF.PoliceMod;

namespace EF.PoliceMod.Debug
{
    public class DebugHud
    {
        public void Draw()
        {
            var lockSys = EFCore.Instance.LockTargetSystem;
            var suspect = lockSys?.CurrentTarget;

            string text =
                $"~b~[EF Police Debug]\n" +
                $"HasTarget: {(suspect != null && suspect.Exists())}\n" +
                $"InVehicle: {(suspect != null && suspect.Exists() && suspect.IsInVehicle())}\n" +
                $"PlayerInVehicle: {Game.Player.Character.IsInVehicle()}";

            Screen.ShowSubtitle(text, 1);
        }
    }
}
