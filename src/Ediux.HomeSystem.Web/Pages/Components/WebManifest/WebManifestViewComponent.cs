﻿using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Pages.Components.WebManifest
{
    public class WebManifestViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("/Pages/Components/WebManifest/Default.cshtml");
        }
    }
}
