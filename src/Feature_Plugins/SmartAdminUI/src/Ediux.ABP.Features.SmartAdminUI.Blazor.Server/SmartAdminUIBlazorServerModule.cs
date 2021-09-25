using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI.Blazor.Server
{
    [DependsOn(
        typeof(AbpAspNetCoreComponentsServerThemingModule),
        typeof(SmartAdminUIBlazorModule)
        )]
    public class SmartAdminUIBlazorServerModule : AbpModule
    {
        
    }
}