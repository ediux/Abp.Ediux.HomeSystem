using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

namespace Ediux.HomeSystem.Blazor.Components
{
    public partial class BlogPost<TItem>
    {
        [Parameter]
        public TItem Data { get; set; }

        [Parameter]
        public EventCallback<TItem> DataChanged { get; set; }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await DataChanged.InvokeAsync(Data);

            await base.SetParametersAsync(parameters);
        }
    }
}
