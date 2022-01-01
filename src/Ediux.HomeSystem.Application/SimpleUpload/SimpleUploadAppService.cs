using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Files;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.MIMETypeManager;
using Ediux.HomeSystem.Models.DTOs.Files;
using System;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.CmsKit.Admin.MediaDescriptors;
using Volo.CmsKit.MediaDescriptors;

namespace Ediux.HomeSystem.SimpleUpload
{
    public class SimpleUploadAppService : ApplicationService, ISimpleUploadAppService
    {
        //protected readonly IBlobContainer<MediaContainer> blobContainer;
        //protected readonly IRepository<File_Store> file_Stores;
        //protected readonly IMIMETypeManagerAppService MIMETypeManagerAppService;
        protected readonly IFileStoreAppService _fileStoreAppService;

        public SimpleUploadAppService(
            IFileStoreAppService fileStoreAppService)
        {
            _fileStoreAppService = fileStoreAppService;
        }

        public async virtual Task<MediaDescriptorDto> CreateAsync(string entityType, CreateMediaInputWithStream inputStream)
        {
            //var definition = await MediaDescriptorDefinitionStore.GetAsync(entityType);

            ///* TODO: Shouldn't CreatePolicies be a dictionary and we check for inputStream.EntityType? */
            //await CheckAnyOfPoliciesAsync(definition.CreatePolicies);

            using (var stream = inputStream.File.GetStream())
            {
                var file_Store = await _fileStoreAppService.CreateAsync(new FileStoreDTO()
                {
                    ExtName = Path.GetExtension(inputStream.File.FileName),
                    Name = Path.GetFileNameWithoutExtension(inputStream.File.FileName),
                    Description = string.Format(L[HomeSystemResource.Common.SimpleUploadDescriptTemplate].Value, DateTime.UtcNow),
                    Size = inputStream.File.ContentLength.HasValue ? inputStream.File.ContentLength.Value : 0L,
                    OriginFullPath = inputStream.File.FileName,
                    FileContent = stream.GetAllBytes()
                });
                return new MediaDescriptorDto() { Id = file_Store.Id, MimeType = file_Store.ContentType, Name = file_Store.Name, Size = (int)file_Store.Size };
            }
        }

        public async virtual Task<MediaDescriptorDto> GetAsync(Guid id)
        {
            var file_Store = await _fileStoreAppService.GetAsync(id);
            return new MediaDescriptorDto() { Id = file_Store.Id, MimeType = file_Store.ContentType, Name = file_Store.Name, Size = (int)file_Store.Size };
        }

        public Task<Stream> GetStreamAsync(MediaDescriptorDto input)
        {
            return _fileStoreAppService.GetStreamAsync(input);
        }

        public virtual async Task<RemoteStreamContent> DownloadAsync(Guid id)
        {
            return await _fileStoreAppService.DownloadAsync(id);          
        }

        public async Task DeleteAsync(Guid id)
        {
            await _fileStoreAppService.DeleteAsync(id);
        }
    }
}
