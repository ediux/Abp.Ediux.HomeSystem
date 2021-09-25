using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Ediux.ABP.Features.SmartAdminUI
{
    [DependsOn(
        typeof(SmartAdminUIApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class SmartAdminUIHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "SmartAdminUI";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(SmartAdminUIApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
