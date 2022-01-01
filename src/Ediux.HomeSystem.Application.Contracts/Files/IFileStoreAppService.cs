using Ediux.HomeSystem.Models.DTOs.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.DependencyInjection;
using Volo.CmsKit.Admin.MediaDescriptors;

namespace Ediux.HomeSystem.Files
{
    public interface IFileStoreAppService : ICrudAppService<FileStoreDTO, Guid, FileStoreRequestDTO>, ITransientDependency
    {
        Task<Stream> GetStreamAsync(MediaDescriptorDto input);

        Task<RemoteStreamContent> DownloadAsync(Guid id);

        Task<bool> IsExistsAsync(Guid id);

        Task<bool> IsExistsAsync(string name);

        Task<IList<FileStoreDTO>> GetPhotosAsync(FileStoreRequestDTO input);
    }
}
