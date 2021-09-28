using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Localization;
using Ediux.HomeSystem.Localization;

namespace Ediux.HomeSystem.Web
{
    [Dependency(ReplaceServices = true)]    
    public class HomeSystemBrandingProvider : DefaultBrandingProvider
    {        
        [Dependency]
        public IHtmlLocalizer<HomeSystemResource> L { get; set; }

        public override string AppName => L["SiteName"].Value;
    }
}
