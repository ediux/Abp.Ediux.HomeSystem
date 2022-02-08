using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;

namespace Ediux.HomeSystem.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Ediux.HomeSystem", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Ediux.HomeSystem", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(build =>
                {
                    build.AddJsonFile("appsettings.secrets.json", optional: true);
                })
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    IHostEnvironment env = hostContext.HostingEnvironment;

                    var application = services.AddApplication<HomeSystemDbMigratorModule>(options =>
                      {
                          options.ConfigureABPPlugins(env.ContentRootPath);
                      });

                    //application.Initialize(services.BuildServiceProvider());
                    //var hostedServiceType = application.Modules.SelectMany(s => s.Assembly.GetTypes())
                    //   .Where(w => w.Name.EndsWith("DbMigrationService") && w.Name != "HomeSystemDbMigrationService")
                    //   .ToList();

         
                    //if (hostedServiceType != null && hostedServiceType.Any())
                    //{
                    //    foreach (var serviceType in hostedServiceType)
                    //    {
                    //        typeof(ServiceCollectionHostedServiceExtensions).GetMethod("AddHostedService",new Type[] { })
                    //            .MakeGenericMethod(serviceType).Invoke(null, new object[] { services });

                    //        //object serviceInstance = scope.ServiceProvider
                    //        //      .GetRequiredService(serviceType);

                    //        //if (serviceInstance == null)
                    //        //    continue;

                    //        //MethodInfo methodInfo = serviceType.GetMethod("MigrateAsync");

                    //        //if (methodInfo != null)
                    //        //{
                    //        //    ((Task)methodInfo.Invoke(serviceInstance, new object[] { })).Wait();
                    //        //}
                    //    }

                    //}


                });
    }
}
