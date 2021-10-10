using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.Models.DTOs.jqDataTables
{
    public class jqDTSearchedResultRequestDto : PagedAndSortedResultRequestDto, ITransientDependency
    {
        public string Search { get; set; }
    }
}
