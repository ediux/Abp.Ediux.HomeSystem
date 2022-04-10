using Ediux.HomeSystem.Localization;

using Microsoft.AspNetCore.Mvc;

using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Controllers;

/* Inherit your controllers from this class.
 */
[ApiController]
[Route("api/[controller]/{id?}")]
public abstract class HomeSystemController : AbpControllerBase
{
    protected HomeSystemController()
    {
        LocalizationResource = typeof(HomeSystemResource);
    }
}
