using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI.Blazor.WebAssembly
{
    [DependsOn(
        typeof(SmartAdminUIBlazorModule),
        typeof(SmartAdminUIHttpApiClientModule),
        typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
        )]
    public class SmartAdminUIBlazorWebAssemblyModule : AbpModule
    {
        
    }
}