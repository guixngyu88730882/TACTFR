// HelpController.cs
using GTA;
using GTA.Native;
using GTA.UI;

namespace EF.PoliceMod.Core
{
    public class HelpController
    {
        private bool _showing = false;
        private int _until = 0;

        public HelpController()
        {
            // 订阅 ToggleHelpEvent —— 事件类型定义在 ToggleHelpEvent.cs
            EventBus.Subscribe<ToggleHelpEvent>(_ => ToggleHelp());
        }

        private void ToggleHelp()
        {
            _showing = !_showing;
            if (_showing)
            {
                // P1-2 Fix: Use dynamic key binding names instead of hardcoded keys
                string eKey = KeyBindings.GetKeyDisplayName(KeyBindings.VehicleInteract);
                string gKey = KeyBindings.GetKeyDisplayName(KeyBindings.EscortRequest);
                string lKey = KeyBindings.GetKeyDisplayName(KeyBindings.LockTarget);
                string hKey = KeyBindings.GetKeyDisplayName(KeyBindings.ArrestMenu);
                string patrolKey = KeyBindings.GetKeyDisplayName(KeyBindings.PatrolMenu);
                string oKey = KeyBindings.GetKeyDisplayName(KeyBindings.OpenTerminal);
                
                string text =
$@"{eKey} 让嫌疑人上车
{gKey} 让嫌疑人跟你/让他下车
{lKey} 对着嫌疑人摁就是锁定
{hKey} 锁定后摁就是逮捕
{patrolKey} 巡逻菜单(巡逻模式开启时)
{oKey} 打开警用终端";
                // 显示 12 秒（12000 ms）
                Screen.ShowSubtitle(text, 12000);
            }
            else
            {
                // 清除文本
                Function.Call(Hash.CLEAR_PRINTS);
            }
        }
    }
}
