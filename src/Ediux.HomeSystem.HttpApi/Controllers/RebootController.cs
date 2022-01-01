using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Ediux.HomeSystem.Controllers
{
    [ApiController]
    [Authorize(Volo.Abp.FeatureManagement.FeatureManagementPermissions.ManageHostFeatures)]
    [Route("api/reboot")]
    public class RebootController : HomeSystemController
    {
        private IHostApplicationLifetime appLifetime;

        public RebootController(IHostApplicationLifetime appLifetime)
        {
            this.appLifetime = appLifetime;
        }

        [HttpGet]
        public ActionResult Index()
        {
            appLifetime.StopApplication();
            return NoContent();
        }
    }
}
