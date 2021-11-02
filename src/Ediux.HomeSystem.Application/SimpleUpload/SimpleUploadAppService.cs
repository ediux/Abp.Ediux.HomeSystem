using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.MIMETypeManager;

using System;
using System.IO;
using System.Threading.Tasks;

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
        protected readonly IBlobContainer<MediaContainer> blobContainer;
        protected readonly IRepository<File_Store> file_Stores;
        protected readonly IMIMETypeManagerAppService MIMETypeManagerAppService;
        public SimpleUploadAppService(IBlobContainer<MediaContainer> mediaContainer,
            IRepository<File_Store> file_Stores,
            IMIMETypeManagerAppService MIMETypeManagerAppService)
        {
            this.blobContainer = mediaContainer;
            this.file_Stores = file_Stores;
            this.MIMETypeManagerAppService = MIMETypeManagerAppService;
        }

        public async virtual Task<MediaDescriptorDto> CreateAsync(string entityType, CreateMediaInputWithStream inputStream)
        {
            //var definition = await MediaDescriptorDefinitionStore.GetAsync(entityType);

            ///* TODO: Shouldn't CreatePolicies be a dictionary and we check for inputStream.EntityType? */
            //await CheckAnyOfPoliciesAsync(definition.CreatePolicies);

            using (var stream = inputStream.File.GetStream())
            {
                //var newEntity = await MediaDescriptorManager.CreateAsync(entityType, inputStream.Name, inputStream.File.ContentType, inputStream.File.ContentLength ?? 0);
                File_Store file_Store = new File_Store()
                {
                    CreationTime = DateTime.UtcNow,
                    CreatorId = CurrentUser.Id,
                    ExtName = Path.GetExtension(inputStream.File.FileName),
                    Name = inputStream.File.FileName,
                    Size = inputStream.File.ContentLength.HasValue ? inputStream.File.ContentLength.Value : 0L,
                    StorageInSMB = false,
                    OriginFullPath = inputStream.File.FileName
                };

                var mimetypes = await MIMETypeManagerAppService.GetListAsync(
                    new Models.DTOs.jqDataTables.jqDTSearchedResultRequestDto()
                    {
                        Search = file_Store.ExtName
                    });
                Models.DTOs.MIMETypes.MIMETypesDTO mIMETypesDTO;

                if (mimetypes.TotalCount >= 1)
                {
                    mIMETypesDTO = mimetypes.Items[0];
                }
                else
                {
                    mIMETypesDTO = await MIMETypeManagerAppService.CreateAsync(new Models.DTOs.MIMETypes.MIMETypesDTO()
                    {
                        Description = inputStream.File.ContentType,
                        MIME = inputStream.File.ContentType,
                        RefenceExtName = file_Store.ExtName,
                        CreationTime = DateTime.UtcNow,
                        CreatorId = CurrentUser.Id
                    });
                }

                file_Store.MIMETypeId = mIMETypesDTO.Id;
                file_Store = await file_Stores.InsertAsync(file_Store);
                await blobContainer.SaveAsync(file_Store.Id.ToString(), stream, overrideExisting: true);

                return new MediaDescriptorDto() { Id = file_Store.Id, MimeType = file_Store.MIME.MIME, Name = file_Store.Name, Size = (int)file_Store.Size };
            }
        }

        public async virtual Task<MediaDescriptorDto> GetAsync(Guid id)
        {
            var file_Store = await file_Stores.GetAsync(p => p.Id == id);
            return new MediaDescriptorDto() { Id = file_Store.Id, MimeType = file_Store.MIME.MIME, Name = file_Store.Name, Size = (int)file_Store.Size };
        }

        public Task<Stream> GetStreamAsync(MediaDescriptorDto input)
        {
            return blobContainer.GetAsync(input.Id.ToString());
        }

        public virtual async Task<RemoteStreamContent> DownloadAsync(Guid id)
        {
            var entity = await file_Stores.GetAsync(p => p.Id == id, includeDetails: true);
            var stream = await blobContainer.GetAsync(id.ToString());

            return new RemoteStreamContent(stream)
            {
                ContentType = (await MIMETypeManagerAppService.GetAsync(entity.MIMETypeId)).MIME
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var success = await blobContainer.DeleteAsync(id.ToString());

            if (success)
            {
                await file_Stores.DeleteAsync(p => p.Id == id);
            }
        }
    }
}
