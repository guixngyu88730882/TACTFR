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
                string text =
@"E 让嫌疑人上车
G 让嫌疑人跟你/让他下车
L 对着嫌疑人摁就是锁定
H 锁定后摁就是逮捕
Y 巡逻菜单(巡逻模式开启时)
O 打开警用终端";
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
