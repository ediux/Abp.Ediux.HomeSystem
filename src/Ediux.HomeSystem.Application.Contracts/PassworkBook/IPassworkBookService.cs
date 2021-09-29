using Ediux.HomeSystem.Models.DTOs.PassworkBook;

using System;
using System.Collections.Generic;
using System.Text;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.PassworkBook
{
    public interface IPassworkBookService : ICrudAppService<PassworkBookDTO, long>, ITransientDependency
    {
    }
}
