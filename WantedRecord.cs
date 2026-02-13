namespace EF.PoliceMod.Data
{
    public class WantedRecord
    {
        public int PedHandle;
        public string DisplayName;
        public int Stars;
        public string LastSeen;
        public string Reason;

        // 终端右侧详情用（UI 仅展示，不参与逻辑）
        public string SuspectName;
        public string Crime;

        // 终端展示：地点（例如“市区/郊区/偏远”）
        public string Location;
    }
}
