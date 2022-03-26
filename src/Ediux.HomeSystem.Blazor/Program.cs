using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazorise.RichTextEdit;
using Blazorise;
using Blazorise.Bootstrap5;
using Microsoft.Extensions.DependencyInjection;
using Blazorise.Icons.FontAwesome;

namespace Ediux.HomeSystem.Blazor;

public class Program
{
    public async static Task Main(string[] args)
    {
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
