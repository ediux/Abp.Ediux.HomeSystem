using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Ediux.HomeSystem.Web.Pages.Components.Commenting
{
    public class CommentingStyleBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/CmsKit/Commenting/default.css");
        }
    }
}
