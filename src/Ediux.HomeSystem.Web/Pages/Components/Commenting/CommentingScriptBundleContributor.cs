﻿using System.Collections.Generic;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;

namespace Ediux.HomeSystem.Web.Pages.Components.Commenting
{
    public class CommentingScriptBundleContributor : BundleContributor
    {
        public override void ConfigureBundle(BundleConfigurationContext context)
        {
            context.Files.AddIfNotContains("/Pages/Components/Commenting/default.js");
        }
    }
}