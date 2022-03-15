using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreClassificationAppService : HomeSystemCrudAppService<FileStoreClassification, FileClassificationDto, Guid, AbpSearchRequestDto>, IFileStoreClassificationAppService
    {
        public FileStoreClassificationAppService(IRepository<FileStoreClassification, Guid> repository) : base(repository)
        {
        }

        public override async Task<PagedResultDto<FileClassificationDto>> GetListAsync(AbpSearchRequestDto input)
        {
            var result = (await Repository.WithDetailsAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Search),p=>p.Name.Contains(input.Search))
                .ToList();
            
            long c = result.LongCount();

            return new PagedResultDto<FileClassificationDto>(c, await MapToGetListOutputDtosAsync(result));
        }
    }
}
