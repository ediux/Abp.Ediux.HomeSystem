using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.Files
{
    public class FileStoreRequestDTO : PagedAndSortedResultRequestDto, ITransientDependency
    {
        [JsonPropertyName("search")]
        public string Search { get; set; }
    }
}
