using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazorise.RichTextEdit;

using Ediux.HomeSystem.Options.ConfigurationJson;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

using System;
using System.IO;
using System.Threading.Tasks;
namespace Ediux.HomeSystem.Blazor;

public class Program
{
    public async static Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
     .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
     .Enrich.FromLogContext()
     .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();

        if (Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true")
        {
            string workDir = Environment.CurrentDirectory;
            string loadConfigPath = Path.Combine(workDir, "wwwroot", "appsettings.json");

            if (File.Exists(loadConfigPath))
            {
                var appSettings = AppSettingsJsonObject.LoadSettingFile(loadConfigPath);

                if (appSettings != null)
                {
                    if (appSettings.App.ContainsKey("SelfUrl"))
                    {
                        appSettings.App["SelfUrl"] = Environment.GetEnvironmentVariable("App_SelfUrl");
                    }
                    else
                    {
                        appSettings.App.Add("SelfUrl", Environment.GetEnvironmentVariable("App_SelfUrl"));
                    }

                    if (appSettings.AuthServer.ContainsKey("Authority"))
                    {
                        appSettings.AuthServer["Authority"] = Environment.GetEnvironmentVariable("AuthServer_AuthorityUrl");
                    }
                    else
                    {
                        appSettings.App.Add("Authority", Environment.GetEnvironmentVariable("AuthServer_AuthorityUrl"));
                    }

                    if(appSettings.RemoteServices.Default.ContainsKey("BaseUrl"))
                    {
                        appSettings.RemoteServices.Default["BaseUrl"] = Environment.GetEnvironmentVariable("RemoteServices_Default_BaseUrl");
                    }
                    else
                    {
                        appSettings.RemoteServices.Default.Add("BaseUrl", Environment.GetEnvironmentVariable("RemoteServices_Default_BaseUrl"));
                    }

                    AppSettingsJsonObject.SaveSettingFile(appSettings, loadConfigPath);
                }
            }
        }

        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        var application = await builder.AddApplicationAsync<HomeSystemBlazorModule>(options =>
        {
            options.HostBuilder.Services.AddBlazorContextMenu();
            options.HostBuilder.Services.AddBlazorise()
           .AddBootstrap5Providers()
           .AddFontAwesomeIcons().AddBlazoriseRichTextEdit();
            options.UseAutofac();
        });

        var host = builder.Build();

        await application.InitializeApplicationAsync(host.Services);

        await host.RunAsync();
    }
}
