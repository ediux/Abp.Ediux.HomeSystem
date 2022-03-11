using System.Text.Json.Serialization;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem
{
    public class AbpSearchRequestDto : PagedAndSortedResultRequestDto, ITransientDependency
    {
        [JsonPropertyName("search")]
        public string Search { get; set; }
    }
}
