using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [Dependency(ReplaceServices = true)]
    public class SmartAdminUIBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SmartAdminUI";
    }
}
