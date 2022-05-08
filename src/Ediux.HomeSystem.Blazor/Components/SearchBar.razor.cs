using Microsoft.AspNetCore.Components;

namespace Ediux.HomeSystem.Blazor.Components
{
    public partial class SearchBar
    {
        [Parameter]
        public string Placeholder { get; set; } = "Search";

        protected void SearchButtonClick()
        {

        }
    }
}
