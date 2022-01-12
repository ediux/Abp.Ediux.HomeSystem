using Ediux.HomeSystem.Plugins.HololivePages.Localization;
using Volo.Abp.Application.Services;

namespace Ediux.HomeSystem.Plugins.HololivePages
{
    public abstract class HololivePagesAppService : ApplicationService
    {
        protected HololivePagesAppService()
        {
            LocalizationResource = typeof(HololivePagesResource);
            ObjectMapperContext = typeof(HololivePagesApplicationModule);
        }
    }
}
