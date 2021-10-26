using System.Text.Json.Serialization;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.DashBoard
{
    public class DashBoardWidgetOptionDTOs: ITransientDependency
    {
        [JsonPropertyName("widgets")]
        public WidgetInformationDTO[] Widgets { get; set; }

        public DashBoardWidgetOptionDTOs()
        {
            Widgets = new WidgetInformationDTO[] { };
        }
    }

    public class WidgetInformationDTO : ITransientDependency
    {
        public WidgetInformationDTO()
        {
            
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
