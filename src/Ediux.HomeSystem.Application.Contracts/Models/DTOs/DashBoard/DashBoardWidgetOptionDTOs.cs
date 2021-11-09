using System.Text.Json.Serialization;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.DashBoard
{
    public class DashBoardWidgetOptionDTOs: ITransientDependency
    {
        [JsonPropertyName("widgets")]
        public DashBoardWidgetsDTO[] Widgets { get; set; }

        public DashBoardWidgetOptionDTOs()
        {
            Widgets = new DashBoardWidgetsDTO[] { };
        }
    }   
}
