using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.MediaDescriptors;
using Ediux.HomeSystem.Models.DTOs.Files;
using Ediux.HomeSystem.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.CmsKit.Admin.MediaDescriptors;
using Volo.CmsKit.MediaDescriptors;

namespace Ediux.HomeSystem.Files
{
    public class FileStoreAppService : CrudAppService<File_Store, FileStoreDTO, Guid, FileStoreRequestDTO>, IFileStoreAppService
    {
        protected readonly IBlobContainer<MediaContainer> _blobContainer;
        protected readonly IBlobContainer<AutoSaveContainer> _autoSaveContainer;
        private readonly IDistributedCache<RemoteStreamContent, Guid> _cache;
        private readonly IRepository<MIMEType> _mimeTypeRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public FileStoreAppService(IRepository<File_Store, Guid> repository,
            IBlobContainer<MediaContainer> blobContainer,
            IBlobContainer<AutoSaveContainer> autoSaveContainer,
            IRepository<MIMEType> mimeTypeRepository,
            IIdentityUserRepository identityUserRepository,
            IDistributedCache<RemoteStreamContent, Guid> cache) : base(repository)
        {
            _blobContainer = blobContainer;
            _autoSaveContainer = autoSaveContainer;
            _mimeTypeRepository = mimeTypeRepository;
            _identityUserRepository = identityUserRepository;
            _cache = cache;
        }

        public async override Task<FileStoreDTO> GetAsync(Guid id)
        {
            var entity = await base.GetAsync(id);

            if (entity != null)
            {
                var stream = await _blobContainer.GetAsync(id.ToString());
                entity.FileContent = await stream.GetAllBytesAsync();
                entity.ContentType = (await _mimeTypeRepository.FindAsync(p => p.RefenceExtName == entity.ExtName))?.MIME ?? HomeSystemConsts.DefaultContentType;
            }

            return entity;
        }

        public async override Task<FileStoreDTO> CreateAsync(FileStoreDTO input)
        {
            input.Id = GuidGenerator.Create();
            var entity = MapToEntity(input);

            MIMEType mIMETypesDTO = await _mimeTypeRepository.FindAsync(p => p.RefenceExtName == entity.ExtName);

            if (mIMETypesDTO == null)
            {
                entity.MIME = await _mimeTypeRepository.InsertAsync(new MIMEType
                {
                    Description = L[HomeSystemResource.Features.MIMETypes.DefaultBinaryFile_Description, input.ExtName].Value,
                    MIME = HomeSystemConsts.DefaultContentType,
                    RefenceExtName = input.ExtName,
                });
                entity.MIMETypeId = entity.MIME.Id;
            }
            else
            {
                entity.MIMETypeId = mIMETypesDTO.Id;
                entity.MIME = mIMETypesDTO;
            }

            entity = await Repository.InsertAsync(entity);
            await _blobContainer.SaveAsync(entity.Id.ToString(), input.FileContent, overrideExisting: true);
            return input;
        }

        public async override Task<FileStoreDTO> UpdateAsync(Guid id, FileStoreDTO input)
        {
            if (await _blobContainer.ExistsAsync(id.ToString()))
            {
                if (input.FileContent == null)
                    throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.CannotBeNullOrEmpty, nameof(input)],
                        HomeSystemDomainErrorCodes.CannotBeNullOrEmpty, 
                        innerException: new ArgumentNullException(nameof(input)),
                        logLevel: Microsoft.Extensions.Logging.LogLevel.Error);

                var entity = await Repository.GetAsync(id);

                await _blobContainer.SaveAsync(id.ToString(), input.FileContent, overrideExisting: true);
                MIMEType mIMETypesDTO = await _mimeTypeRepository.FindAsync(p => p.RefenceExtName == input.ExtName);

                if (mIMETypesDTO == null)
                {
                    entity.MIME = await _mimeTypeRepository.InsertAsync(new MIMEType
                    {
                        Description = L[HomeSystemResource.Features.MIMETypes.DefaultBinaryFile_Description,input.ExtName].Value,
                        MIME = HomeSystemConsts.DefaultContentType,
                        RefenceExtName = input.ExtName,
                    });
                    entity.MIMETypeId = entity.MIME.Id;
                }
                else
                {
                    entity.MIMETypeId = mIMETypesDTO.Id;
                    entity.MIME = mIMETypesDTO;
                }

                if (entity.Name != input.Name)
                {
                    entity.Name = input.Name;
                }

                if (entity.ExtName != input.ExtName)
                {
                    entity.ExtName = input.ExtName;
                }

                if (entity.Size != input.Size)
                {
                    entity.Size = input.Size;
                }

                if (entity.ExtraProperties.ContainsKey(HomeSystemConsts.Description) && !entity.ExtraProperties[HomeSystemConsts.Description].Equals(input.Description))
                {
                    entity.ExtraProperties[HomeSystemConsts.Description] = input.Description;
                }

                if (entity.ExtraProperties.ContainsKey(HomeSystemConsts.IsAutoSaveFile) && !entity.ExtraProperties[HomeSystemConsts.IsAutoSaveFile].Equals(input.IsAutoSaveFile))
                {
                    entity.ExtraProperties[HomeSystemConsts.IsAutoSaveFile] = input.IsAutoSaveFile;
                }

                if (entity.OriginFullPath != input.OriginFullPath)
                {
                    entity.OriginFullPath = input.OriginFullPath;
                }

                return MapToGetOutputDto(await Repository.UpdateAsync(entity, autoSave: true));
            }

            await DeleteAsync(id);
            return null;
        }

        public async override Task DeleteAsync(Guid id)
        {
            if (await _blobContainer.DeleteAsync(id.ToString()))
            {
                await base.DeleteAsync(id);
            }
        }

        public async Task<RemoteStreamContent> DownloadAsync(Guid id)
        {
            return await _cache.GetOrAddAsync(
                        id,
                        async () =>
                        {
                            var entity = await GetAsync(id);

                            var downloadItem = new RemoteStreamContent(new MemoryStream(entity.FileContent), $"{entity.Name}{entity.ExtName}")
                            {
                                ContentType = entity.ContentType
                            };
                            return downloadItem;
                        },
                        () => new DistributedCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                        });
        }

        public async override Task<PagedResultDto<FileStoreDTO>> GetListAsync(FileStoreRequestDTO input)
        {
            bool hasSucceededPolicy = (await AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Special)).Succeeded;

            var result = (await Repository.GetQueryableAsync())
                .WhereIf(!hasSucceededPolicy, p => p.CreatorId == CurrentUser.Id)
                 .ToList();

            var output = await MapToGetListOutputDtosAsync(result);

            if (output.Any())
            {
                output = output
                    .AsQueryable()
                    .WhereIf(input.Search.IsNullOrWhiteSpace() == false,
                    w => w.Name.Contains(input.Search) ||
                    w.ExtName.Contains(input.Search) ||
                    w.OriginFullPath.Contains(input.Search) ||
                    (w.Description != null && w.Description.Contains(input.Search)))
                    .ToList();

                foreach (var item in output)
                {
                    IdentityUser creatorData = await _identityUserRepository.FindAsync(item.CreatorId);

                    if (creatorData != null)
                        item.Creator = $"{creatorData.Surname}{creatorData.Name}";

                    if (item.ModifierId.HasValue)
                    {
                        IdentityUser modifierData = await _identityUserRepository.FindAsync(item.ModifierId.Value);

                        if (modifierData != null)
                            item.Modifier = $"{modifierData.Surname}{modifierData.Name}";
                    }
                }
            }

            return new PagedResultDto<FileStoreDTO>(result.Count, output);
        }

        public async Task<Stream> GetStreamAsync(MediaDescriptorDto input)
        {
            return (await DownloadAsync(input.Id))?.GetStream();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return (await Repository.FindAsync(id, includeDetails: false) != null);
        }

        public async Task<bool> IsExistsAsync(string name)
        {
            string fn = Path.GetFileNameWithoutExtension(name);
            string extName = Path.GetExtension(name);

            return (await Repository.FindAsync(p => p.Name == fn && p.ExtName == extName, includeDetails: false) != null);
        }
    }
}
