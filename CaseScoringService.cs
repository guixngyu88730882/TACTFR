using EF.PoliceMod.Core;
using GTA.UI;

namespace EF.PoliceMod.Systems
{
    public static class CaseScoringService
    {
        private const int MaxCaseDurationMs = 10 * 60 * 1000;

        public static int CalculateScore(CaseManager.EndReason reason, int elapsedMs, bool suspectWasResisting)
        {
            if (elapsedMs > MaxCaseDurationMs)
                return 0;
            
            int baseScore = 0;

            switch (reason)
            {
                case CaseManager.EndReason.Completed:
                    baseScore = 10;
                    break;
                case CaseManager.EndReason.SuspectDead:
                    if (suspectWasResisting)
                        baseScore = 8;
                    else
                        baseScore = 2;
                    break;
                case CaseManager.EndReason.Failed:
                case CaseManager.EndReason.Timeout:
                case CaseManager.EndReason.Cancelled:
                    baseScore = 0;
                    break;
            }

            float timeFactor = 1f - (float)elapsedMs / MaxCaseDurationMs;
            if (timeFactor < 0.2f)
                timeFactor = 0.2f;

            return (int)(baseScore * timeFactor);
        }

        public static void ShowScore(CaseManager.EndReason reason, int score, int elapsedMs)
        {
            int seconds = elapsedMs / 1000;
            string reasonText = "任务结束";

            switch (reason)
            {
                case CaseManager.EndReason.Completed:
                    reasonText = "成功交付";
                    break;
                case CaseManager.EndReason.SuspectDead:
                    reasonText = "嫌疑人死亡";
                    break;
                case CaseManager.EndReason.Timeout:
                    reasonText = "任务超时";
                    break;
            }

            Notification.Show(
                "~y~案件结束：" + reasonText + "\n" +
                "~w~用时：" + seconds + " 秒\n" +
                "~g~评分：" + score + " / 10"
            );
        }
    }
}
