using System;

using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreClassificationAppService : HomeSystemCrudAppService<FileStoreClassification, FileClassificationDto, Guid, AbpSearchRequestDto>, IFileStoreClassificationAppService
    {
        public FileStoreClassificationAppService(IRepository<FileStoreClassification, Guid> repository) : base(repository)
        {
        }
    }
}
