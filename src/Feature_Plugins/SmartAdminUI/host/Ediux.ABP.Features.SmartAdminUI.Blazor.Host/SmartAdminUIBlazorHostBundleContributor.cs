using Volo.Abp.Bundling;

namespace Ediux.ABP.Features.SmartAdminUI.Blazor.Host
{
    public class SmartAdminUIBlazorHostBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {

        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}
