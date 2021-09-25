using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Volo.Abp.AspNetCore.Mvc.UI.Theme.SmartAdmin.Bundling
{
    public class SmartAdminThemeGlobalStyleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.Add("/themes/smartadmin/css/vendors.bundle.css");
            context.Files.Add("/themes/smartadmin/css/app.bundle.css");
            context.Files.Add("/themes/smartadmin/css/themes/cust-theme-3.css");
            context.Files.Add("/themes/smartadmin/css/skins/skin-master.css");
            context.Files.Add("/themes/smartadmin/css/site.css");
        }
    }
}
