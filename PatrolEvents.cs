namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 巡逻玩法（自由行动）事件：低耦合。
    /// </summary>
    public struct PatrolModeToggledEvent
    {
        public bool Enabled { get; }
        public PatrolModeToggledEvent(bool enabled) { Enabled = enabled; }
    }

    /// <summary>
    /// 巡逻模式命令菜单开关（Y）。
    /// </summary>
    public struct PatrolMenuToggledEvent
    {
        public bool Open { get; }
        public PatrolMenuToggledEvent(bool open) { Open = open; }
    }

    /// <summary>
    /// 请求对“当前锁定目标/面前路人”进行检查。
    /// </summary>
    public struct PatrolCheckRequestedEvent { }

    /// <summary>
    /// 请求路人出示身份证（仅 UI/短信提示，不直接上任务）。
    /// </summary>
    public struct PatrolRequestIdEvent { }

    /// <summary>
    /// 请求搜查（可出违禁品/通缉/携枪等）。
    /// </summary>
    public struct PatrolSearchRequestedEvent { }


    /// <summary>
    /// 巡逻玩法：目标在“巡逻交互”中发生反抗（与任务执法 SuspectResistEvent 解耦）。
    /// </summary>
    public struct PatrolSuspectResistEvent
    {
        public GTA.Ped Suspect { get; }
        public GTA.Ped Officer { get; }
        public PatrolSuspectResistEvent(GTA.Ped suspect, GTA.Ped officer)
        {
            Suspect = suspect;
            Officer = officer;
        }
    }


    /// <summary>
    /// 巡逻玩法：目标逃跑（与任务执法链路解耦）。
    /// </summary>
    public struct PatrolSuspectFleeEvent
    {
        public GTA.Ped Suspect { get; }
        public GTA.Ped Officer { get; }
        public PatrolSuspectFleeEvent(GTA.Ped suspect, GTA.Ped officer)
        {
            Suspect = suspect;
            Officer = officer;
        }
    }

    /// <summary>
    /// 巡逻玩法：搜查发现违禁品（仅提示/后续扩展，不触发案件系统）。
    /// </summary>
    public struct PatrolContrabandFoundEvent
    {
        public GTA.Ped Suspect { get; }
        public bool WeaponFound { get; }
        public PatrolContrabandFoundEvent(GTA.Ped suspect, bool weaponFound)
        {
            Suspect = suspect;
            WeaponFound = weaponFound;
        }
    }
}

