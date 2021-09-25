using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ediux.ABP.Features.SmartAdminUI.Blazor.Server.Host
{
    [Dependency(ReplaceServices = true)]
    public class SmartAdminUIBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SmartAdminUI";
    }
}
