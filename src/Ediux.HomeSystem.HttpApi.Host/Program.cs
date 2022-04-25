using System;
using System.IO;
using System.Threading.Tasks;

using Ediux.HomeSystem.Options.ConfigurationJson;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Events;

namespace Ediux.HomeSystem;

public class Program
{
    public async static Task<int> Main(string[] args)
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
            string loadConfigPath = Path.Combine(workDir, "appsettings.json");

            if (File.Exists(loadConfigPath))
            {
                Log.Information($"Load appsettings.json on {loadConfigPath} ...");

                var appSettings = AppSettingsJsonObject.LoadSettingFile(loadConfigPath);

                if (appSettings != null)
                {
                    try
                    {
                        if (appSettings.App.ContainsKey("SelfUrl"))
                        {

                            appSettings.App["SelfUrl"] = Environment.GetEnvironmentVariable("App_SelfUrl");
                        }
                        else
                        {
                            appSettings.App.Add("SelfUrl", Environment.GetEnvironmentVariable("App_SelfUrl"));
                        }
                        Log.Information($"SelfUrl={appSettings.App["SelfUrl"]}");

                        if (appSettings.App.ContainsKey("CorsOrigins"))
                        {
                            appSettings.App["CorsOrigins"] = Environment.GetEnvironmentVariable("App_CorsOrigins");
                        }
                        else
                        {
                            appSettings.App.Add("CorsOrigins", Environment.GetEnvironmentVariable("App_CorsOrigins"));
                        }

                        Log.Information($"CorsOrigins={appSettings.App["CorsOrigins"]}");

                        if (appSettings.App.ContainsKey("RedirectAllowedUrls"))
                        {
                            appSettings.App["RedirectAllowedUrls"] = Environment.GetEnvironmentVariable("App_RedirectAllowedUrls");
                        }
                        else
                        {
                            appSettings.App.Add("RedirectAllowedUrls", Environment.GetEnvironmentVariable("App_RedirectAllowedUrls"));
                        }
                        Log.Information($"RedirectAllowedUrls={appSettings.App["RedirectAllowedUrls"]}");
                        if (appSettings.ConnectionStrings.ContainsKey("Default"))
                        {
                            appSettings.ConnectionStrings["Default"] = HomeSystemConsts.GetDefultConnectionStringFromOSENV();
                        }
                        else
                        {
                            appSettings.ConnectionStrings.Add("Default", HomeSystemConsts.GetDefultConnectionStringFromOSENV());
                        }
                        Log.Information($"Default={appSettings.ConnectionStrings["Default"]}");
                        if (appSettings.AuthServer.ContainsKey("Authority"))
                        {
                            appSettings.AuthServer["Authority"] = Environment.GetEnvironmentVariable("AuthServer_AuthorityUrl");
                            Log.Information($"Authority={appSettings.AuthServer["Authority"]}");
                        }
                        else
                        {
                            appSettings.AuthServer.Add("Authority", Environment.GetEnvironmentVariable("AuthServer_AuthorityUrl"));
                            Log.Information($"Authority={appSettings.AuthServer["Authority"]}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message + ex.StackTrace);
                    }

                    AppSettingsJsonObject.SaveSettingFile(appSettings, loadConfigPath);
                }
            }
        }




        try
        {
            Log.Information("Starting Ediux.HomeSystem.HttpApi.Host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<HomeSystemHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
