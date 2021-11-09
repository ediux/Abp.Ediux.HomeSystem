using System.Collections.Generic;
using System.Text.Json.Serialization;

using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.DashBoard
{
    public class DashboardWidgetRequestedDTOs : ITransientDependency
    {
        [JsonPropertyName("selectedDefaultWidgets")]
        public virtual List<string> SelectedDefaultWidgets { get; set; }

        public DashboardWidgetRequestedDTOs()
        {
            SelectedDefaultWidgets = new List<string>();
        }
    }
}
