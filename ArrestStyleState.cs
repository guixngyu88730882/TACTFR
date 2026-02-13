namespace EF.PoliceMod.Core
{
    public enum ArrestActionStyle
    {
        CuffAndLead = 1,
        HandsOnHeadFollow = 2,
    }

    public static class ArrestStyleState
    {
        public static ArrestActionStyle SelectedStyle = ArrestActionStyle.CuffAndLead;
    }
}