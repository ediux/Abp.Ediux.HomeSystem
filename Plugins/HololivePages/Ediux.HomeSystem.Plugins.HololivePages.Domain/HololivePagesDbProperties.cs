namespace Ediux.HomeSystem.Plugins.HololivePages
{
    public static class HololivePagesDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Hololive";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "HololivePages";
    }
}
