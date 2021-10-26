using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Ediux.HomeSystem.Models.DTOs.DashBoard
{
    public class DashboardWidgetRequestedDTOs
    {
        [JsonPropertyName("selectedDefaultWidgets")]        
        public virtual List<string> SelectedDefaultWidgets { get; set; }

        public DashboardWidgetRequestedDTOs()
        {
            SelectedDefaultWidgets = new List<string>();
        }
    }    
}
