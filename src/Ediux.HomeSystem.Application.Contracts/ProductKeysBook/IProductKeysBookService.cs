using Ediux.HomeSystem.Models.DTOs.ProductKeysBook;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.ProductKeysBook
{
    public interface IProductKeysBookService : ICrudAppService<ProductKeysBookDTO, Guid>, ITransientDependency
    {
    }
}
