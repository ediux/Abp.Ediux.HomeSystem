using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Packages.StarRatingSvg;
using Volo.Abp.Modularity;

namespace Ediux.HomeSystem.Web.Pages.Components.Rating
{
    [DependsOn(typeof(StarRatingSvgScriptContributor))]
    public class RatingScriptBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Components/CmsKit/Rating/default.js");
        }
    }
}