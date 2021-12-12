using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Web.Pages.Components.LayoutHook.CKEditor
{
    public class CKEditorHookViewComponent : AbpViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("/Components/LayoutHooks/CKEditor/Default.cshtml");
        }
    }
}
