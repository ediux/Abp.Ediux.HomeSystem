using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.DependencyInjection;
using Volo.CmsKit.Admin.MediaDescriptors;

namespace Ediux.HomeSystem.SimpleUpload
{
    public interface ISimpleUploadAppService : IMediaDescriptorAdminAppService, ITransientDependency
    {
        Task<MediaDescriptorDto> GetAsync(Guid id);

        Task<Stream> GetStreamAsync(MediaDescriptorDto input);

        Task<RemoteStreamContent> DownloadAsync(Guid id);

   
    }
}
