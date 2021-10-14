using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.MIMETypes;

using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.MIMETypeManager
{
    public interface IMIMETypeManagerAppService : ICrudAppService<MIMETypesDTO, int, jqDTSearchedResultRequestDto>, ITransientDependency
    {
    }
}
