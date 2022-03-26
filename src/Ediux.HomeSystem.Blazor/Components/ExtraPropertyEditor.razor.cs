using Ediux.HomeSystem.Localization;

using Microsoft.AspNetCore.Components;

using System.Threading.Tasks;

using Volo.Abp.Data;

namespace Ediux.HomeSystem.Blazor.Components
{
    public partial class ExtraPropertyEditor<TItem>
    {
        [Parameter]
        public TItem Data { get; set; }

        private string extraFieldName = string.Empty;
        private string extraFieldValue = string.Empty;

        protected override void OnInitialized()
        {
            LocalizationResource = typeof(HomeSystemResource);

            base.OnInitialized();
        }

        Task OnExtendPropertyChanged(string name, string value)
        {
            if (Data.ExtraProperties.ContainsKey(name))
            {
                Data.ExtraProperties[name] = value;
            }
            else
            {
                Data.ExtraProperties.Add(name, value);
            }

            return InvokeAsync(StateHasChanged);
        }

        Task OnAddExtendFieldClicked()
        {
            if (Data.ExtraProperties.ContainsKey(extraFieldName) == false)
            {
                Data.ExtraProperties.Add(extraFieldName, extraFieldValue);
            }

            extraFieldName = string.Empty;
            extraFieldValue = string.Empty;

            return InvokeAsync(StateHasChanged);
        }

        Task OnDeleteExtendFieldClicked(string key)
        {
            if (Data.ExtraProperties.ContainsKey(key))
            {
                Data.ExtraProperties.Remove(key);
            }
            return InvokeAsync(StateHasChanged);
        }
    }
}
