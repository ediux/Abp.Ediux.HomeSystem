using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Pages.Components.Firebase
{
    public class FirebaseHeaderViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {            
            return View("/Pages/Components/Firebase/Header.cshtml");
        }
    }
}
