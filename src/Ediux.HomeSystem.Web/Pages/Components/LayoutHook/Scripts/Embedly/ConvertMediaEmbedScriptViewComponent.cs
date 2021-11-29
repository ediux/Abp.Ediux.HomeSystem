using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Pages.Components.LayoutHook.Scripts.Embedly
{
    public class ConvertMediaEmbedScriptViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("/Pages/Components/LayoutHook/Scripts/Embedly/ConvertSciprt.cshtml");
        }
    }
}
