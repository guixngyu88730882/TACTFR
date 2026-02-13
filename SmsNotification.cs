using GTA;
using GTA.Native;
using GTA.UI;
using System;

namespace EF.PoliceMod.Core
{
    /// <summary>
    /// GTA5 原生“短信/Feed 消息”通知（带头像图标，效果接近你截图左侧堆叠通知）。
    /// </summary>
    public static class SmsNotification
    {
        /// <param name="title">标题（发件人）</param>
        /// <param name="subtitle">副标题（可留空）</param>
        /// <param name="message">正文</param>
        /// <param name="iconName">内置头像纹理：例如 CHAR_CALL911 / CHAR_DEFAULT 等</param>
        public static void Show(string title, string subtitle, string message)
        {
            // 直接使用 SHVDN 的短信通知（最接近你截图、且不会触发“native 不存在”崩溃）
            try
            {
                Notification.Show(
                    NotificationIcon.Call911,
                    title ?? string.Empty,
                    subtitle ?? string.Empty,
                    message ?? string.Empty,
                    true,
                    true
                );
                return;
            }
            catch { }

            // 最终兜底
            try { GTA.UI.Notification.Show((title ?? "") + "~n~" + (message ?? "")); } catch { }
        }
    }
}