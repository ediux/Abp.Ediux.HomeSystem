using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Controllers
{
    [ApiController]
    [Authorize("FeatureManagement.ManageHostFeatures")]
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
