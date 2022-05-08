using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreClassificationAppService : HomeSystemCrudAppService<FileStoreClassification, FileClassificationDto, Guid, AbpSearchRequestDto>, IFileStoreClassificationAppService
    {
        public FileStoreClassificationAppService(IRepository<FileStoreClassification, Guid> repository,
            IdentityUserManager identityUserManager) : base(repository, identityUserManager)
        {
        }

        public override async Task<PagedResultDto<FileClassificationDto>> GetListAsync(AbpSearchRequestDto input)
        {
            var q = (await Repository.WithDetailsAsync(p => p.Childs))
                .Where(w => w.ParentClassificationId.HasValue == false)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Search), p => p.Name.Contains(input.Search));

            var result = await AsyncExecuter.ToListAsync(q);
            long c = result.LongCount();

            return new PagedResultDto<FileClassificationDto>(c, await MapToGetListOutputDtosAsync(result));
        }

        public override async Task<FileClassificationDto> GetAsync(Guid id)
        {
            var q = (await Repository.WithDetailsAsync(p => p.Childs))
               .Where(w => w.Id == id);

            var result = await AsyncExecuter.SingleOrDefaultAsync(q);
            return await MapToGetListOutputDtoAsync(result);
        }

        public override async Task DeleteAsync(Guid id)
        {
            var q = (await Repository.WithDetailsAsync(p => p.Childs, f => f.Files))
              .Where(w => w.Id == id);

            var deleteItem = await AsyncExecuter.SingleOrDefaultAsync(q);

            if (deleteItem != null)
            {
                await Repository.DeleteAsync(deleteItem);
            }
        }

        public async Task<FileClassificationDto> FindByNameAsync(string name)
        {
            var q = (await Repository.WithDetailsAsync(p => p.Childs))
               .WhereIf(!string.IsNullOrWhiteSpace(name), p => p.Name == name)
               .SingleOrDefault();

            if (q != null)
                return await MapToGetOutputDtoAsync(q);

            return null;
        }
    }
}
