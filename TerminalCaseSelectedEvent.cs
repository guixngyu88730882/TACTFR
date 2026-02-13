namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 终端选择案件模板（地点 + 形态 + 星级风险）。
    /// OptionId：0..2=步行；3..5=驾车逃逸。
    /// Stars：1~2 普通；3~4 中危；5 高危。
    /// </summary>
    public class TerminalCaseSelectedEvent
    {
        public int OptionId { get; }
        public int Stars { get; }

        public TerminalCaseSelectedEvent(int optionId, int stars)
        {
            OptionId = optionId;
            Stars = stars;
        }
    }
}