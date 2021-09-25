using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.Application.Services;

namespace Ediux.ABP.Features.SmartAdminUI
{
    public abstract class SmartAdminUIAppService : ApplicationService
    {
        protected SmartAdminUIAppService()
        {
            LocalizationResource = typeof(SmartAdminUIResource);
            ObjectMapperContext = typeof(SmartAdminUIApplicationModule);
        }
    }
}
