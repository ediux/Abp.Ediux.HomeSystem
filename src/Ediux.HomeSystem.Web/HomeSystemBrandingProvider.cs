using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Localization;
using Ediux.HomeSystem.Localization;
using Volo.Abp.SettingManagement;
using Ediux.HomeSystem.Settings;

namespace Ediux.HomeSystem.Web
{
    [Dependency(ReplaceServices = true)]
    public class HomeSystemBrandingProvider : DefaultBrandingProvider
    {
        [Dependency]
        public IHtmlLocalizer<HomeSystemResource> L { get; set; }

        [Dependency]
        public ISettingManager SettingManager { get; set; }

        public override string AppName { get { var val = SettingManager.GetOrNullGlobalAsync(HomeSystemSettings.SiteName); val.Wait(); return val.Result; } }

        
    }
}
