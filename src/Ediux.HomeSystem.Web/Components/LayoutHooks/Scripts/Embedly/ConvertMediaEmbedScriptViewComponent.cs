using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Pages.Components.LayoutHook.Scripts.Embedly
{
    public class ConvertMediaEmbedScriptViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("/Components/LayoutHooks/Scripts/Embedly/ConvertSciprt.cshtml");
        }
    }
}
