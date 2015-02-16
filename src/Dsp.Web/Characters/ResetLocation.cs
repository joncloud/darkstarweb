namespace Dsp.Web.Characters
{
    public class ResetLocation
    {
        public ResetLocationType Type { get; set; }
    }

    public enum ResetLocationType
    {
        Home = 0,
        RuludeGardens = 1
    }
}