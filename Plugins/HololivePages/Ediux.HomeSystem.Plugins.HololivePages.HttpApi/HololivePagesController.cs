using Ediux.HomeSystem.Plugins.HololivePages.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.HomeSystem.Plugins.HololivePages
{
    public abstract class HololivePagesController : AbpController
    {
        protected HololivePagesController()
        {
            LocalizationResource = typeof(HololivePagesResource);
        }
    }
}
