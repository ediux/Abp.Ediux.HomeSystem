using System;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public interface IFileStoreClassificationAppService : ICrudAppService<FileClassificationDto, Guid, AbpSearchRequestDto>, ITransientDependency
    {  
    }
}
