using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace Ediux.HomeSystem
{
    public class HomeSystemWebTestStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<HomeSystemWebTestModule>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}