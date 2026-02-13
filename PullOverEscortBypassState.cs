using EF.PoliceMod.Core;

namespace EF.PoliceMod.Executors
{
    /// <summary>
    /// I 逼停线（PullOverSystem）触发的“车辆指令旁路”状态：
    /// - 仅用于 HandsOnHeadFollow（抱头/举手线）
    /// - 允许在有限时间内跳过 L 锁定 + H 菜单拘捕
    /// </summary>
    internal sealed class PullOverEscortBypassState
    {
        private int _suspectHandle = -1;
        private int _expireAtMs = 0;
        private readonly int _ttlMs;

        public PullOverEscortBypassState(int ttlMs)
        {
            _ttlMs = ttlMs;
        }

        public void Activate(int suspectHandle, int nowMs)
        {
            if (suspectHandle <= 0) return;
            _suspectHandle = suspectHandle;
            _expireAtMs = nowMs + _ttlMs;
        }

        public bool IsActive(ArrestActionStyle style, int suspectHandle, int nowMs)
        {
            if (style != ArrestActionStyle.HandsOnHeadFollow) return false;
            if (suspectHandle <= 0) return false;
            if (_expireAtMs == 0) return false;
            if (nowMs > _expireAtMs) return false;
            return suspectHandle == _suspectHandle;
        }

        public void Clear()
        {
            _suspectHandle = -1;
            _expireAtMs = 0;
        }
    }
}