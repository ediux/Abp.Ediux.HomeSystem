using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Plugins.HololivePages.HoloInforamtions
{
    public class HoloRequestDto : PagedAndSortedResultRequestDto, ITransientDependency
    {
        [JsonPropertyName("search")]
        public string Search { get; set; }
    }
}
