using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Web
{
    [Dependency(ReplaceServices = true)]
    public class HomeSystemBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "HomeSystem";
    }
}
