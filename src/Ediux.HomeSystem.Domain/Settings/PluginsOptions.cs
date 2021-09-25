using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Settings
{
    public class PluginsOptions
    {
        public const string SectionName = "plugins";

        [JsonProperty("plugins")]
        public PluginsData[] plugins { get; set; }

        public PluginsOptions()
        {
            plugins = new PluginsData[] { };
        }

    }

    public class PluginsData
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("PluginPath")]
        public string PluginPath { get; set; }
        [JsonProperty("Disabled")]
        public bool Disabled { get; set; }
    }
}
