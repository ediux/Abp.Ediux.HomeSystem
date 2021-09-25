using Ediux.HomeSystem.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class HomeSystemController : AbpController
    {
        protected HomeSystemController()
        {
            LocalizationResource = typeof(HomeSystemResource);
        }
    }
}