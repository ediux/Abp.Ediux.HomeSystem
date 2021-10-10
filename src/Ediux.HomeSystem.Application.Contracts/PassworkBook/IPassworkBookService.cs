using Ediux.HomeSystem.Models.DTOs.jqDataTables;
using Ediux.HomeSystem.Models.DTOs.PassworkBook;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.PassworkBook
{
    public interface IPassworkBookService : ICrudAppService<PassworkBookDTO, long, jqDTSearchedResultRequestDto>, ITransientDependency
    {
    }
}
