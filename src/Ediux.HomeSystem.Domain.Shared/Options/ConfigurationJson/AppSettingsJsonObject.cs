using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ediux.HomeSystem.Options.ConfigurationJson
{
    public class AppSettingsJsonObject
    {
        [JsonPropertyName("App")]
        public Dictionary<string, string> App { get; set; }

        [JsonPropertyName("ConnectionStrings")]
        public Dictionary<string, string> ConnectionStrings { get; set; }

        [JsonPropertyName("RemoteServices")]
        public RemoteServices RemoteServices { get; set; }

        [JsonPropertyName("AuthServer")]
        public Dictionary<string, object> AuthServer { get; set; }

        [JsonPropertyName("AbpCli")]
        public AbpCliOptions ApbCli { get; set; }

        [JsonPropertyName("StringEncryption")]
        public Dictionary<string, string> StringEncryption { get; set; }

        public static AppSettingsJsonObject LoadSettingFile(string path)
        {
            return JsonSerializer.Deserialize<AppSettingsJsonObject>(File.OpenRead(path));
        }

        public static void SaveSettingFile(AppSettingsJsonObject source,string path)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(source, new JsonSerializerOptions() { WriteIndented = true }), Encoding.UTF8);
        }
    }

    public class RemoteServices
    {
        [JsonPropertyName("Default")]
        public Dictionary<string, string> Default { get; set; }
    }

    public class AbpCliOptions
    {
        [JsonPropertyName("Bundle")]
        public Dictionary<string, object> Bundle { get; set; }
    }
}
