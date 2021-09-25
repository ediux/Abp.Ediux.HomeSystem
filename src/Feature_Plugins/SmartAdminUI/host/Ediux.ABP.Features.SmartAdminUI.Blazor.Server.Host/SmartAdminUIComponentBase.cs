using Ediux.ABP.Features.SmartAdminUI.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ediux.ABP.Features.SmartAdminUI.Blazor.Server.Host
{
    public abstract class SmartAdminUIComponentBase : AbpComponentBase
    {
        protected SmartAdminUIComponentBase()
        {
            LocalizationResource = typeof(SmartAdminUIResource);
        }
    }
}
