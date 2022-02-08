
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Volo.Abp;

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
