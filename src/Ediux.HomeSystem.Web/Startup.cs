using Ediux.HomeSystem.ApplicationPluginsManager;
using Ediux.HomeSystem.EntityFrameworkCore;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Modularity.PlugIns;

namespace Ediux.HomeSystem.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var env = services.GetHostingEnvironment();
            var config = services.GetConfiguration();
            services.AddLogging();

            services.AddApplication<HomeSystemWebModule>(options =>
            {
                options.ConfigureABPPlugins(env);              
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
