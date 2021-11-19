using Ediux.HomeSystem.Localization;
using System.ComponentModel;

namespace Ediux.HomeSystem.Models.DTOs.SystemSettings
{
    public class BatchSettingsDTO
    {
        [DisplayName(HomeSystemResource.Common.Fields.Timer_Period)]
        public int Timer_Period { get; set; }
    }
}
