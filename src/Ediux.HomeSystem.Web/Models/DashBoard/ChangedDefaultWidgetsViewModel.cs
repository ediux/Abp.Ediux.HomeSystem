using Ediux.HomeSystem.Models.DTOs.DashBoard;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Ediux.HomeSystem.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DashBoard
{
    public class ChangedDefaultWidgetsViewModel: DashboardWidgetRequestedDTOs, ITransientDependency
    {
        [JsonIgnore]
        [DynamicFormIgnore]
        public List<SelectListItem> WidgetLists { get; set; }

        [JsonPropertyName("selectedDefaultWidgets")]
        [DisplayName(HomeSystemResource.Features.Dashboard.Options_Label_setDefaultLoad)]
        [SelectItems(nameof(WidgetLists))]      
        public override List<string> SelectedDefaultWidgets { get => base.SelectedDefaultWidgets; set => base.SelectedDefaultWidgets = value; }
        
        public ChangedDefaultWidgetsViewModel()
        {
            WidgetLists = new List<SelectListItem>();
        }
    }

    
}
