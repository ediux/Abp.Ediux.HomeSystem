using Ediux.HomeSystem.EntityFrameworkCore;
using Ediux.HomeSystem.MultiTenancy;
using Ediux.HomeSystem.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Ediux.HomeSystem
{
    [DependsOn(
        typeof(HomeSystemEntityFrameworkCoreTestModule)
        )]
    public class HomeSystemDomainTestModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<PluginsManager>();
            context.Services.AddTransient<CollectiblePluginInSource>();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options =>
            {
                if (Environment.GetEnvironmentVariable("AbpMultiTenancy") == "Enabled")
                    options.IsEnabled = true;
                else
                    options.IsEnabled = false;
            });

#if DEBUG
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
        }
    }
}