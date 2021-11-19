using Ediux.HomeSystem.Localization;
using System.ComponentModel;

namespace Ediux.HomeSystem.Models.DTOs.SystemSettings
{
    /// <summary>
    /// 系統設定資料中介物件
    /// </summary>
    public class SystemSettingsDTO
    {
        [DisplayName(HomeSystemResource.Common.Fields.SiteName)]
        public string WebSite { get; set; }

        [DisplayName(HomeSystemResource.Common.Fields.WelcomeSlogan)]
        public string WelcomeSlogan { get; set; }
    }


}
