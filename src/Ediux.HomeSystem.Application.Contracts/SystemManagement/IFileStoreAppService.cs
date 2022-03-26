
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.DependencyInjection;

namespace Ediux.HomeSystem.SystemManagement
{
    public interface IFileStoreAppService : ICrudAppService<FileStoreDto, Guid, FileStoreSearchRequestDto>, ITransientDependency
    {
        Task<Stream> GetStreamAsync(MediaDescriptorDto input);

        Task<RemoteStreamContent> DownloadAsync(Guid id);

        Task<bool> IsExistsAsync(Guid id);

        Task<bool> IsExistsAsync(string name);

        Task<IList<FileStoreDto>> GetPhotosAsync(FileStoreSearchRequestDto input);
    }
}
