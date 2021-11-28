using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.jqDataTables
{
    public class jqDTSearchedResultRequestDto : PagedAndSortedResultRequestDto, ITransientDependency
    {
        [JsonPropertyName("search")]
        public string Search { get; set; }
    }
}
