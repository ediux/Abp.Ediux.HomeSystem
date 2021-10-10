using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;

using System;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.ProductKeysBook
{
    public interface IProductKeysBookService : ICrudAppService<ProductKeysBookDTO, Guid, jqDTSearchedResultRequestDto>, ITransientDependency
    {
    }
}
