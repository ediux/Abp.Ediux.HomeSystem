using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ediux.ABP.Features.SmartAdminUI
{
    public abstract class SmartAdminUIController : AbpController
    {
        protected SmartAdminUIController()
        {
            LocalizationResource = typeof(SmartAdminUIResource);
        }
    }
}
