using System.Text.Json.Serialization;

namespace Ediux.HomeSystem.SystemManagement
{
    public class PluginsOptionsDTOs
    {
        public const string SectionName = "plugins";

        [JsonPropertyName("plugins")]
        public PluginsDataDTO[] plugins { get; set; }

        public PluginsOptionsDTOs()
        {
            plugins = new PluginsDataDTO[] { };
        }

    }

    public class PluginsDataDTO
    {
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [JsonPropertyName("PluginPath")]
        public string PluginPath { get; set; }
        [JsonPropertyName("Disabled")]
        public bool Disabled { get; set; }
    }
}
