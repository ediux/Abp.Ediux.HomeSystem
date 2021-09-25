using System;
using System.Collections.Generic;
using System.Text;
using Ediux.HomeSystem.Localization;
using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem
{
    /* Inherit your application services from this class.
     */
    public abstract class HomeSystemAppService : ApplicationService
    {
        protected HomeSystemAppService()
        {
            LocalizationResource = typeof(HomeSystemResource);
        }
    }
}
