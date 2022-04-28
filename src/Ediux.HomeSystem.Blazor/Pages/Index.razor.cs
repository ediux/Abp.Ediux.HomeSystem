using Ediux.HomeSystem.Localization;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Threading.Tasks;

namespace Ediux.HomeSystem.Blazor.Pages;

public partial class Index
{
    [Inject] public IJSRuntime JS { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("changeTitle", L[HomeSystemResource.Common.SiteName].Value);
        }
    }
}
