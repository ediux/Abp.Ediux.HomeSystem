using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ediux.HomeSystem.Web.Pages
{
    [Authorize(policy: "FeatureManagement.ManageHostFeatures")]
    public class PluginsManagerModel : HomeSystemPageModel
    {
        public void OnGet()
        {
        }
    }
}
