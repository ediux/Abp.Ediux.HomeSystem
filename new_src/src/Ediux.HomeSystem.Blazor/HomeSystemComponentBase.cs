using Ediux.HomeSystem.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ediux.HomeSystem.Blazor
{
    public abstract class HomeSystemComponentBase : AbpComponentBase
    {
        protected HomeSystemComponentBase()
        {
            LocalizationResource = typeof(HomeSystemResource);
        }
    }
}
