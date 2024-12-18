namespace Lottery.Application.Configuration.Options
{
    public class GeneratorOptions
    {
        public const string ConfigSection = "Generator";

        public int Min { get; set; }
        public int Max { get; set; }
        public int Count { get; set; }
        public bool AllowDuplicates { get; set; }
    }
}
