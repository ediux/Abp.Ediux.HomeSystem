using System.Text.Json.Serialization;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Settings
{
    public class DashBoardWidgetOption: ITransientDependency
    {
        [JsonPropertyName("widgets")]
        public WidgetInformation[] Widgets { get; set; }

        public DashBoardWidgetOption()
        {
            Widgets = new WidgetInformation[] { };
        }
    }

    public class WidgetInformation : ITransientDependency
    {
        public WidgetInformation()
        {
            Default = false;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("default")]
        public bool Default { get; set; }
    }
}
