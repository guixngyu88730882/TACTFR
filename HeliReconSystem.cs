using EF.PoliceMod.Core;
using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Linq;

namespace EF.PoliceMod.Systems
{
    public sealed class HeliReconSystem
    {
        private const int ReconDurationMs = 60 * 1000;
        private const int ReconTickIntervalMs = 900;

        private int _lastRequestedAtMs = -9999999;

        private bool _active = false;
        private int _startedAtMs = 0;
        private int _lastScanAtMs = 0;

        private int _targetHandle = -1;

        private bool _waitingForSms = false;
        private int _smsSendAtMs = 0;
        private int _smsDelayMs = 0;

        private bool _pendingAcquire = false;
        private int _pendingAcquireAtMs = 0;
        private int _pendingAcquireDelayMs = 0;

        private int _lastConfirmedAtMs = 0;
        private const int ConfirmProtectionMs = 20 * 1000;

        public HeliReconSystem()
        {
            EventBus.Subscribe<HeliReconRequestedEvent>(_ => TryStartRecon());
        }

        private void TryStartRecon()
        {
            try
            {
                int now = Game.GameTime;

                int cd = ModConfig.HeliReconCooldownMs;
                int remain = (_lastRequestedAtMs + cd) - now;
                if (remain > 0)
                {
                    int sec = Math.Max(1, remain / 1000);
                    SmsNotification.Show("911调度", "侦察请求", $"直升机侦察冷却中：{sec}s");
                    return;
                }

                if (!EF.PoliceMod.Systems.DutyQuery.IsOnDuty)
                {
                    SmsNotification.Show("911调度", "侦察请求", "请先开始执勤后再呼叫侦察支援。");
                    return;
                }

                if (!EF.PoliceMod.Systems.CaseStatusQuery.HasActiveCase)
                {
                    SmsNotification.Show("911调度", "侦察请求", "当前没有进行中的案件。");
                    return;
                }

                if (!EF.PoliceMod.Systems.CaseStatusQuery.IsSuspectLost)
                {
                    SmsNotification.Show("911调度", "侦察请求", "目标未丢失，无需侦察。");
                    return;
                }

                if (EF.PoliceMod.Systems.CaseStatusQuery.HeliReconUsed)
                {
                    SmsNotification.Show("911调度", "侦察请求", "本案直升机侦察已使用过（每案一次）。");
                    return;
                }

                if (now - EF.PoliceMod.Systems.CaseStatusQuery.CaseStartedAtMs > 10 * 60 * 1000)
                {
                    SmsNotification.Show("911调度", "侦察请求", "侦察窗口已过期（开案超过10分钟）。");
                    return;
                }

                _targetHandle = EF.PoliceMod.Systems.CaseStatusQuery.CurrentSuspectHandle;
                if (_targetHandle <= 0)
                {
                    SmsNotification.Show("911调度", "侦察请求", "无法确定目标。");
                    return;
                }
                try { EFCore.Instance?.GetCaseManager()?.MarkHeliReconUsed(); } catch { }

                _active = true;
                _lastRequestedAtMs = now;
                _startedAtMs = now;
                _lastScanAtMs = 0;
                _waitingForSms = false;
                _pendingAcquire = true;
                _pendingAcquireAtMs = now;
                _pendingAcquireDelayMs = (new Random(unchecked(Environment.TickCount ^ now)).Next(9000, 12001));

                ModLog.Info($"[HeliRecon] Started: targetHandle={_targetHandle}");
                SmsNotification.Show("911调度", "侦察支援", "直升机已出动，正在搜索嫌疑人位置（约10秒）...");
            }
            catch { }
        }

        public void Tick()
        {
            int now = Game.GameTime;

            if (_pendingAcquire)
            {
                if (now - _pendingAcquireAtMs < _pendingAcquireDelayMs)
                    return;

                _pendingAcquire = false;
            }

            if (_waitingForSms)
            {
                if (now - _smsSendAtMs >= _smsDelayMs)
                {
                    _waitingForSms = false;
                    SmsNotification.Show("911调度", "侦察回报", "嫌犯目前已到新的位置，地图标记已更新。");
                    _active = false;
                }
                return;
            }

            if (!_active) return;

            if (now - _startedAtMs > ReconDurationMs)
            {
                _active = false;
                SmsNotification.Show("911调度", "侦察支援", "侦察结束：未能找到嫌疑人。请继续在标注范围内搜索。");
                return;
            }

            if (now - _lastScanAtMs < ReconTickIntervalMs) return;
            _lastScanAtMs = now;

            try
            {
                var ped = World.GetAllPeds().FirstOrDefault(p => p != null && p.Exists() && p.Handle == _targetHandle);
                if (ped == null || !ped.Exists()) return;
                ISuspectContext ctx = EFCore.Instance?.GetCaseManager() as ISuspectContext;
                if (ctx == null)
                {
                    ModLog.Warn("[HeliRecon] ISuspectContext is null");
                    return;
                }

                Vector3 pos;
                try { pos = ped.Position; }
                catch
                {
                    if (!ctx.TryGetLastKnownPos(_targetHandle, out pos))
                        pos = ctx.SuspectLastKnownPos;
                }

                if (_lastConfirmedAtMs != 0 && now - _lastConfirmedAtMs < ConfirmProtectionMs)
                    return;

                ModLog.Info($"[HeliRecon] MarkSuspectFound: ped={ped.Handle}, pos={pos}");
                ctx.MarkSuspectFound(ped, pos);
                _lastConfirmedAtMs = now;

                _waitingForSms = true;
                _smsSendAtMs = now;
                _smsDelayMs = (new Random(unchecked(Environment.TickCount ^ now)).Next(10000, 20001));
            }
            catch (Exception ex) { ModLog.Error($"[HeliRecon] Tick error: {ex}"); }
        }
    }
}
