using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(SmartAdminUIHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class SmartAdminUIConsoleApiClientModule : AbpModule
    {
        
    }
}
