using Ediux.HomeSystem.ApplicationPluginsManager;
using Ediux.HomeSystem.EntityFrameworkCore;
using Ediux.HomeSystem.Settings;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System;
using System.IO;
using System.Linq;

using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity.PlugIns;

namespace Ediux.HomeSystem.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var env = services.GetHostingEnvironment();
            var config = services.GetConfiguration();

            services.AddApplication<HomeSystemWebModule>(options =>
            {
                var positionOptions = new PluginsOptions();
                config.GetSection(PluginsOptions.SectionName).Bind(positionOptions);

                //var c = config.GetSection("plugins").GetChildren();

                //services.AddTransient<IApplicationPluginsManager, ApplicationPluginsManager.ApplicationPluginsManager>();
                string pluginFolderPath = Path.Combine(env.ContentRootPath, "Plugins");

                if (Directory.Exists(pluginFolderPath) == false)
                {
                    Directory.CreateDirectory(pluginFolderPath);
                }

                if (positionOptions != null)
                {
                    if (positionOptions != null && positionOptions.plugins.Count() > 0)
                    {
                        FilePlugInSource filePlugInSource = new FilePlugInSource(positionOptions.plugins.Where(a => a.Disabled == false).Select(s => s.PluginPath).ToArray());
                        options.PlugInSources.Add(filePlugInSource);
                    }
                }
              
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
