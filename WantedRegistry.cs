using System.Collections.Generic;
using EF.PoliceMod.Data;
using EF.PoliceMod.Core;
using GTA;

namespace EF.PoliceMod.Data
{
    

    // 轻量“通缉对象提供器”。CaseManager 会把当前嫌疑人注册到 LockTargetSystem，
    // 我们这里从 LockTargetSystem / CaseManager 拉数据（如果没有就返回示例数据）。
    public class WantedRegistry
    {
        // TODO: 如果你有中心化的“通缉列表”可以直接从那里填充
        public IEnumerable<WantedRecord> GetActiveList()
        {
            var list = new List<WantedRecord>();

            // 优先：如果 CaseManager 有活动案件，显示真实案件
            try
            {
                var cm = EFCore.Instance?.GetCaseManager();
                if (cm != null)
                {
                    // 如果有公开方法 GetCurrentSuspect()，优先使用真实数据
                    var getSuspectMethod = cm.GetType().GetMethod("GetCurrentSuspect", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
                    if (getSuspectMethod != null)
                    {
                        var suspect = getSuspectMethod.Invoke(cm, null) as GTA.Ped;
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
                    }

                    // 如果 CaseManager 报告有活动案件但没有具体 Ped，展示简洁提示
                    var hasActiveProp = cm.GetType().GetProperty("HasActiveCase", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                    if (hasActiveProp != null && (bool)hasActiveProp.GetValue(cm))
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

        public void RefreshFromWorld()
        {
            // TODO: optional: 如果你能从 CaseManager/LockTargetSystem 获取动态数据，
            // 在这里把内部缓存更新为真实世界数据。
        }
    }
}
