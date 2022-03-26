using System.Text.Json.Serialization;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class DashBoardWidgetOptionDto: ITransientDependency
    {
        [JsonPropertyName("widgets")]
        public DashBoardWidgetsDto[] Widgets { get; set; }

        public DashBoardWidgetOptionDto()
        {
            Widgets = new DashBoardWidgetsDto[] { };
        }
    }   
}
