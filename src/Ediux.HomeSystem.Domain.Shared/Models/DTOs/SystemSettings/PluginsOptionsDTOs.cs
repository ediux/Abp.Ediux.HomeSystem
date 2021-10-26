using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Settings
{
    public class PluginsOptionsDTOs
    {
        public const string SectionName = "plugins";

        [JsonProperty("plugins")]
        public PluginsDataDTO[] plugins { get; set; }

        public PluginsOptionsDTOs()
        {
            plugins = new PluginsDataDTO[] { };
        }

    }

    public class PluginsDataDTO
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("PluginPath")]
        public string PluginPath { get; set; }
        [JsonProperty("Disabled")]
        public bool Disabled { get; set; }
    }
}
