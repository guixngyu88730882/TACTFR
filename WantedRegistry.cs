using System;
using System.Collections.Generic;
using System.Linq;
using EF.PoliceMod.Data;
using EF.PoliceMod.Core;
using GTA;

namespace EF.PoliceMod.Data
{
    // P1-3 Fix: WantedRegistry now provides data-driven case list for PoliceTerminalUI
    // TODO: Implement RefreshFromWorld() to fetch real wanted data from game world
    public class WantedRegistry
    {
        private List<WantedRecord> _cachedCases = new List<WantedRecord>();
        private int _lastRefreshAtMs = 0;
        private const int REFRESH_INTERVAL_MS = 30000; // Refresh every 30 seconds

        // P1-3 Fix: New method to get available cases for terminal UI
        public List<TerminalCaseData> GetAvailableCases(bool dualMode)
        {
            // Return cached cases if available and not stale
            if (_cachedCases.Count > 0 && Game.GameTime - _lastRefreshAtMs < REFRESH_INTERVAL_MS)
            {
                return ConvertToTerminalCases(_cachedCases, dualMode);
            }

            // Try to get real data from CaseManager/LockTargetSystem
            RefreshFromWorld();
            
            if (_cachedCases.Count > 0)
            {
                return ConvertToTerminalCases(_cachedCases, dualMode);
            }

            // Return empty list to trigger fallback in PoliceTerminalUI
            return new List<TerminalCaseData>();
        }

        // P1-3 Fix: Convert WantedRecord to TerminalCaseData
        private List<TerminalCaseData> ConvertToTerminalCases(List<WantedRecord> records, bool dualMode)
        {
            var result = new List<TerminalCaseData>();
            var rnd = new System.Random();
            int modeOffset = dualMode ? 10 : 0;
            string modeText = dualMode ? "双人" : "单人";

            for (int i = 0; i < records.Count; i++)
            {
                var rec = records[i];
                bool isVehicle = (i % 2 == 1);
                string typeName = isVehicle ? "驾车逃逸" : "步行嫌疑人";
                string reason = isVehicle ? "追车" : "接警";
                string loc = string.IsNullOrEmpty(rec.Location) ? (string.IsNullOrEmpty(rec.LastSeen) ? "未知" : rec.LastSeen) : rec.Location;

                int stars = rec.Stars;
                if (dualMode)
                {
                    stars = isVehicle ? 5 : Math.Max(4, stars);
                }

                string suspectName = rec.DisplayName ?? "未知";
                if (dualMode)
                {
                    var other = records[rnd.Next(records.Count)];
                    suspectName = suspectName + " & " + (other.DisplayName ?? "未知");
                }

                result.Add(new TerminalCaseData
                {
                    OptionId = i + modeOffset,
                    DisplayName = $"{loc} · {typeName} · {modeText}",
                    Stars = stars,
                    LastSeen = rec.LastSeen ?? "-",
                    Reason = reason,
                    SuspectName = suspectName,
                    Crime = rec.Crime ?? rec.Reason ?? "-",
                    Location = loc
                });
            }

            return result;
        }

        // TODO: 如果你有中心化的"通缉列表"可以直接从那里填充
        public IEnumerable<WantedRecord> GetActiveList()
        {
            var list = new List<WantedRecord>();

            // 优先：如果 CaseManager 有活动案件，显示真实案件
            try
            {
                var cm = EFCore.Instance?.GetCaseManager();
                if (cm != null)
                {
                    var suspect = cm.GetCurrentSuspectPed();
                    if (suspect != null && suspect.Exists())
                        {
                            list.Add(new WantedRecord
                            {
                                PedHandle = suspect.Handle,
                                DisplayName = "嫌疑人",
                                Stars = 0,
                                LastSeen = "当前位置",
                                Reason = "正在处理"
                            });
                            return list;
                        }

                    // 如果 CaseManager 报告有活动案件但没有具体 Ped，展示简洁提示
                    if (cm.HasActiveCase)
                    {
                        list.Add(new WantedRecord
                        {
                            PedHandle = -1,
                            DisplayName = "当前有未完成警情",
                            Stars = 0,
                            LastSeen = "-",
                            Reason = "请查看案件任务"
                        });
                        return list;
                    }
                }
            }
            catch
            {
                // ignore reflection failures and fall back
            }

            // 没有真实案件 -> 生成一个随机预览嫌疑人（发布版不是真人，只是预览）
            // 这个预览会把 PedHandle 设为 -1，表示“按确认后由 CaseManager 生成或开始案件”
            // 没有真实案件 -> 生成最多 3 条随机预览以供终端选择（PedHandle = -1 表示预览）
            var names = new[] { "汤米", "尼克", "杰克", "迈卡", "亚瑟", "唐纳德", "陈勇" };
            var crimes = new[] { "盗窃", "持械抢劫", "抗法", "袭击", "破坏公共财产", "骚扰", "非法入侵" };
            var locs = new[] { "市中心", "港区", "工业区", "小巷", "公园", "校园", "住宅区" };

            var rnd = new System.Random(); // 单一 RNG 实例，避免多次 new 导致重复
            var usedIndices = new HashSet<int>();
            int attempts = 0;
            while (list.Count < 3 && attempts < 20)
            {
                attempts++;
                int idx = rnd.Next(names.Length);
                if (usedIndices.Contains(idx)) continue;
                usedIndices.Add(idx);

                string name2 = names[idx];
                string crime2 = crimes[rnd.Next(crimes.Length)];
                string last2 = locs[rnd.Next(locs.Length)];
                int stars2 = rnd.Next(1, 5); // 1..4

                list.Add(new WantedRecord
                {
                    PedHandle = -1,
                    DisplayName = name2,
                    Stars = stars2,
                    LastSeen = last2,
                    Reason = crime2
                });
            }

            // 回退：若极端情况下 list 仍为空（理论上不会），塞一个占位
            if (list.Count == 0)
            {
                list.Add(new WantedRecord
                {
                    PedHandle = -1,
                    DisplayName = "暂无待处理案件",
                    Stars = 0,
                    LastSeen = "-",
                    Reason = "请等待新任务或前往终端接警"
                });
            }

            return list;
        }

        // P1-3 Fix: Implemented RefreshFromWorld to populate cached cases
        public void RefreshFromWorld()
        {
            _cachedCases.Clear();

            try
            {
                // Try to get data from CaseManager
                var cm = EFCore.Instance?.GetCaseManager();
                if (cm != null && cm.HasActiveCase)
                {
                    // Get current suspect if available
                    var handles = cm.SuspectHandles;
                    if (handles != null && handles.Count > 0)
                    {
                        foreach (var handle in handles)
                        {
                            if (handle <= 0) continue;

                            Ped ped = null;
                            try { ped = Entity.FromHandle(handle) as Ped; } catch { }

                            if (ped != null && ped.Exists())
                            {
                                _cachedCases.Add(new WantedRecord
                                {
                                    PedHandle = handle,
                                    DisplayName = "嫌疑人",
                                    Stars = 0,
                                    LastSeen = "当前位置",
                                    Reason = "正在处理"
                                });
                            }
                        }
                    }
                }

                // If no active cases, generate random preview cases
                if (_cachedCases.Count == 0)
                {
                    GenerateRandomCases();
                }
            }
            catch (Exception ex)
            {
                ModLog.Error("[WantedRegistry] RefreshFromWorld error: " + ex);
                GenerateRandomCases();
            }

            _lastRefreshAtMs = Game.GameTime;
        }

        // P1-3 Fix: Generate random preview cases when no real data available
        private void GenerateRandomCases()
        {
            var names = new[] { "汤米", "尼克", "杰克", "迈卡", "亚瑟", "唐纳德", "陈勇", "维克托", "弗兰克", "萨尔" };
            var crimesOnFoot = new[] { "盗窃", "斗殴", "扰乱治安", "非法入侵", "持械抢劫", "袭击", "破坏公共财产" };
            var crimesVehicle = new[] { "拒检逃逸", "危险驾驶", "肇事逃逸", "飙车", "酒驾" };
            var locations = new[] { "市区", "郊区", "偏远" };

            var rnd = new System.Random();

            for (int locIdx = 0; locIdx < locations.Length; locIdx++)
            {
                _cachedCases.Add(new WantedRecord
                {
                    PedHandle = -1,
                    DisplayName = names[rnd.Next(names.Length)],
                    Stars = rnd.Next(1, 5),
                    LastSeen = locations[locIdx],
                    Reason = crimesOnFoot[rnd.Next(crimesOnFoot.Length)],
                    Crime = crimesOnFoot[rnd.Next(crimesOnFoot.Length)],
                    Location = locations[locIdx]
                });

                _cachedCases.Add(new WantedRecord
                {
                    PedHandle = -1,
                    DisplayName = names[rnd.Next(names.Length)],
                    Stars = Math.Max(2, rnd.Next(1, 6)),
                    LastSeen = locations[locIdx],
                    Reason = crimesVehicle[rnd.Next(crimesVehicle.Length)],
                    Crime = crimesVehicle[rnd.Next(crimesVehicle.Length)],
                    Location = locations[locIdx]
                });
            }
        }
    }

    // P1-3 Fix: New data class for terminal UI case data
    public class TerminalCaseData
    {
        public int OptionId { get; set; }
        public string DisplayName { get; set; }
        public int Stars { get; set; }
        public string LastSeen { get; set; }
        public string Reason { get; set; }
        public string SuspectName { get; set; }
        public string Crime { get; set; }
        public string Location { get; set; }
    }
}
