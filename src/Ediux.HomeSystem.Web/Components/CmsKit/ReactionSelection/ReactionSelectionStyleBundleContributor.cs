using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Ediux.HomeSystem.Web.Pages.Components.ReactionSelection
{
    public class ReactionSelectionStyleBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/CmsKit/ReactionSelection/default.css");
        }
    }
}