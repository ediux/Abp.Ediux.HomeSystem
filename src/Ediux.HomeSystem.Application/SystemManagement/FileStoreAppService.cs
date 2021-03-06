using Ediux.HomeSystem.BlobContainers;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Microsoft.Extensions.Logging;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreAppService : HomeSystemCrudAppService<File_Store, FileStoreDto, Guid, FileStoreSearchRequestDto>, IFileStoreAppService
    {
        protected readonly IBlobContainer<MediaContainer> _blobContainer;
        protected readonly IBlobContainer<PluginsContainer> _pluginContainer;

        private readonly IDistributedCache<FileStoreDto, Guid> _cache;
        private readonly IMIMETypeManagerAppService _mIMETypeManager;

        public FileStoreAppService(IRepository<File_Store, Guid> repository,
            IBlobContainer<MediaContainer> blobContainer,
            IBlobContainer<PluginsContainer> pluginContainer,
            IMIMETypeManagerAppService mIMETypeManager,
            IdentityUserManager identityUserManager,
            IDistributedCache<FileStoreDto, Guid> cache) : base(repository, identityUserManager)
        {
            _blobContainer = blobContainer;
            _pluginContainer = pluginContainer;

            _mIMETypeManager = mIMETypeManager;
            _cache = cache;

        }

        public async override Task<FileStoreDto> GetAsync(Guid id)
        {
            var entity = (await Repository.WithDetailsAsync(a => a.MIME, b => b.Classification, c => c.Plugins, d => d.RefencedByMessages)).FirstOrDefault(p => p.Id == id);

            if (entity != null)
            {
                FileStoreDto fileStoreDto = await MapToGetOutputDtoAsync(entity);

                switch (fileStoreDto.Blob.BlobContainerName)
                {
                    default:
                    case "auto-save-temp":
                    case "cms-kit-media":
                        if (await _blobContainer.ExistsAsync(id.ToString()))
                        {
                            MemoryStream ms = new MemoryStream();

                            fileStoreDto.Blob.FileContent = await _blobContainer.GetAllBytesAsync(entity.Id.ToString());
                        }
                        else
                        {
                            fileStoreDto.Blob.FileContent = new byte[] { };
                            fileStoreDto.Size = 0;
                        }
                        break;
                    case "plugins":
                        if (await _pluginContainer.ExistsAsync(id.ToString()))
                        {
                            fileStoreDto.Blob.FileContent = await _pluginContainer.GetAllBytesAsync(entity.Id.ToString());
                        }
                        else
                        {
                            fileStoreDto.Blob.FileContent = new byte[] { };
                            fileStoreDto.Size = 0;
                        }
                        break;
                }

                return fileStoreDto;

            }

            return null;
        }

        public async override Task<FileStoreDto> CreateAsync(FileStoreDto input)
        {
            if (input == null)
            {
                throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.CannotBeNullOrEmpty, nameof(input)], code: HomeSystemDomainErrorCodes.CannotBeNullOrEmpty, innerException: new ArgumentNullException(nameof(input)), logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }

            if (input.Classification == null)
            {
                throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.CannotBeNullOrEmpty, "input.Classification"], code: HomeSystemDomainErrorCodes.CannotBeNullOrEmpty, logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }

            input.Id = GuidGenerator.Create();

            var mIMETypesDTO = await _mIMETypeManager.GetByExtNameAsync(input.ExtName);

            if (mIMETypesDTO == null)
            {
                input.MIMETypes = new MIMETypesDto()
                {
                    ContentType = HomeSystemConsts.DefaultContentType,
                    RefenceExtName = input.ExtName,
                    Description = L[HomeSystemResource.Features.Files.DefaultContextType, input.ExtName].Value
                };

                input.MIMETypes = await _mIMETypeManager.CreateAsync(input.MIMETypes);
            }
            else
            {
                input.MIMETypes = mIMETypesDTO;
            }

            if (CurrentUser.Id.HasValue)
            {
                input.CreatorId = CurrentUser.Id.Value;
            }

            input.CreatorDate = DateTime.Now;

            var entity = MapToEntity(input);
            entity = await Repository.InsertAsync(entity);

            switch (input.Blob.BlobContainerName)
            {
                default:
                case "auto-save-temp":
                case "cms-kit-media":
                    if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                    {
                        await _blobContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent);
                    }
                    break;

                case "plugins":
                    if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                    {
                        await _pluginContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent);
                    }
                    break;
            }

            return input;
        }

        public async override Task<FileStoreDto> UpdateAsync(Guid id, FileStoreDto input)
        {
            var updatedEntity = await MapToEntityAsync(input);
            var entity = await Repository.GetAsync(id);

            if (entity == null)
            {
                throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.DataMissingWithIdentity, id]
                    , code: HomeSystemDomainErrorCodes.DataMissingWithIdentity
                    , logLevel: LogLevel.Error);
            }

            if (updatedEntity.Name != entity.Name)
            {
                entity.Name = updatedEntity.Name;
            }

            if (updatedEntity.IsPublic != entity.IsPublic)
            {
                entity.IsPublic = updatedEntity.IsPublic;
            }

            if (updatedEntity.FileClassificationId != entity.FileClassificationId)
            {
                entity.FileClassificationId = updatedEntity.FileClassificationId;
            }

            if (updatedEntity.BlobContainerName != entity.BlobContainerName)
            {
                switch (entity.BlobContainerName)
                {
                    default:
                    case "auto-save-temp":
                    case "cms-kit-media":
                        input.Blob.FileContent = await _blobContainer.GetAllBytesAsync(entity.Id.ToString());
                        break;
                    case "plugins":
                        input.Blob.FileContent = await _pluginContainer.GetAllBytesAsync(entity.Id.ToString());
                        break;
                }

                entity.BlobContainerName = updatedEntity.BlobContainerName;

                switch (entity.BlobContainerName)
                {
                    default:
                    case "auto-save-temp":
                    case "cms-kit-media":
                        if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                        {
                            await _blobContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent, overrideExisting: true);
                        }
                        break;

                    case "plugins":
                        if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                        {
                            await _pluginContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent, overrideExisting: true);
                        }
                        break;
                }
            }

            if (updatedEntity.MIMETypeId != entity.MIMETypeId)
            {
                updatedEntity.MIMETypeId = entity.MIMETypeId;
            }

            if (updatedEntity.Size != entity.Size)
            {
                if (input.Blob.FileContent != null)
                {
                    switch (entity.BlobContainerName)
                    {
                        default:
                        case "auto-save-temp":
                        case "cms-kit-media":
                            if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                            {
                                await _blobContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent, overrideExisting: true);
                            }
                            break;

                        case "plugins":
                            if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                            {
                                await _pluginContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent, overrideExisting: true);
                            }
                            break;
                    }

                    updatedEntity.Size = entity.Size;
                }
            }

            var addKeys = updatedEntity.ExtraProperties.Keys.Except(entity.ExtraProperties.Keys);
            var removeKeys = entity.ExtraProperties.Keys.Except(updatedEntity.ExtraProperties.Keys);
            var samekeys = updatedEntity.ExtraProperties.Keys.Intersect(entity.ExtraProperties.Keys);

            foreach (string removekey in removeKeys)
            {
                entity.ExtraProperties.Remove(removekey);
            }

            foreach (string addkey in addKeys)
            {
                entity.ExtraProperties.Add(addkey, updatedEntity.ExtraProperties[addkey]);
            }

            foreach (string key in samekeys)
            {
                if (entity.ExtraProperties[key] != updatedEntity.ExtraProperties[key])
                {
                    entity.ExtraProperties[key] = updatedEntity.ExtraProperties[key];
                }
            }


            entity.LastModifierId = updatedEntity.LastModifierId;
            entity.LastModificationTime = updatedEntity.LastModificationTime;

            entity = await Repository.UpdateAsync(entity);

            input = MapToGetOutputDto(entity);



            await _cache.SetAsync(id, input,
                options: new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                });


            return input;
        }

        public async override Task DeleteAsync(Guid id)
        {
            var entity = await Repository.FindAsync(id);

            if (entity != null)
            {
                switch (entity.BlobContainerName)
                {
                    default:
                    case "auto-save-temp":
                    case "cms-kit-media":
                        if (await _blobContainer.ExistsAsync(id.ToString()))
                        {
                            await _blobContainer.DeleteAsync(id.ToString());
                        }
                        else
                        {
                            Logger.LogError(L[HomeSystemDomainErrorCodes.FileNotFoundInContainer, id, "cms-kit-media"]);
                        }
                        break;

                    case "plugins":
                        if (await _pluginContainer.ExistsAsync(id.ToString()))
                        {
                            await _pluginContainer.DeleteAsync(id.ToString());
                        }
                        else
                        {
                            Logger.LogError(L[HomeSystemDomainErrorCodes.FileNotFoundInContainer, id, "plugins"]);
                        }
                        break;
                }
            }

            await base.DeleteAsync(id);

            if ((await _cache.GetAsync(id)) != null)
            {
                await _cache.RemoveAsync(id);
            }
        }

        public async Task<RemoteStreamContent> DownloadAsync(Guid id)
        {
            FileStoreDto entity = await _cache.GetOrAddAsync(
                        id,
                        async () =>
                        {
                            var entityF = await GetAsync(id);
                            Stream stream = null;
                            switch (entityF.Blob.BlobContainerName)
                            {
                                default:
                                case "auto-save-temp":
                                case "cms-kit-media":
                                    if (await _blobContainer.ExistsAsync(id.ToString()))
                                    {
                                        stream = await _blobContainer.GetAsync(id.ToString());
                                    }
                                    else
                                    {
                                        Logger.LogError(L[HomeSystemDomainErrorCodes.FileNotFoundInContainer, id, "cms-kit-media"]);
                                    }
                                    break;

                                case "plugins":
                                    if (await _pluginContainer.ExistsAsync(id.ToString()))
                                    {
                                        stream = await _pluginContainer.GetAsync(id.ToString());
                                    }
                                    else
                                    {
                                        Logger.LogError(L[HomeSystemDomainErrorCodes.FileNotFoundInContainer, id, "plugins"]);
                                    }
                                    break;
                            }
                            if (stream != null)
                            {
                                entityF.Blob.FileContent = stream.GetAllBytes();
                                stream.Close();
                                stream.Dispose();
                            }
                            else
                            {
                                entityF.Blob.FileContent = new byte[] { };
                                entityF.Size = 0;
                            }

                            return entityF;
                        },
                        () => new DistributedCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                        });

            if (entity == null)
                return null;

            return new RemoteStreamContent(new MemoryStream(entity.Blob.FileContent), $"{entity.Name}{entity.MIMETypes.RefenceExtName}", entity.MIMETypes.ContentType);

        }

        public async override Task<PagedResultDto<FileStoreDto>> GetListAsync(FileStoreSearchRequestDto input)
        {
            bool hasSucceededPolicy = (await AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Special)).Succeeded;

            var query = (await Repository.WithDetailsAsync(p => p.MIME))
                .WhereIf(!hasSucceededPolicy, p => p.CreatorId == CurrentUser.Id)
                .WhereIf(input.Classification_Id.HasValue, p => p.FileClassificationId == input.Classification_Id)
                .WhereIf(input.CurrentUser_Id.HasValue, p => p.CreatorId == input.CurrentUser_Id)
                .WhereIf(input.Search.IsNullOrWhiteSpace() == false,
                     w => (!w.Name.IsNullOrWhiteSpace() && w.Name.Contains(input.Search)) ||
                     w.MIME != null && ((!w.MIME.RefenceExtName.IsNullOrWhiteSpace() && w.MIME.RefenceExtName.Contains(input.Search))
                     || (!w.MIME.Description.IsNullOrWhiteSpace() & w.MIME.Description.Contains(input.Search))))
                .ToList();

            long totalCount = query.LongCount();

            var output = new PagedResultDto<FileStoreDto>(totalCount, await MapToGetListOutputDtosAsync(query.ToList()));

            if (output.Items.Any())
            {
                foreach (var item in output.Items)
                {
                    IdentityUser creatorData = await userManagerService.FindByIdAsync(item.CreatorId.ToString());

                    if (creatorData != null)
                        item.Creator = $"{creatorData.Surname}{creatorData.Name}";

                    if (item.ModifierId.HasValue)
                    {
                        IdentityUser modifierData = await userManagerService.FindByIdAsync(item.ModifierId.Value.ToString());

                        if (modifierData != null)
                            item.Modifier = $"{modifierData.Surname}{modifierData.Name}";
                    }
                }
            }
            //var output = await MapToGetListOutputDtosAsync(query);

            return output;
        }

        public async Task<Stream> GetStreamAsync(MediaDescriptorDto input)
        {
            var entityF = await GetAsync(input.Id);

            if (entityF == null)
                throw new AbpException(L[HomeSystemDomainErrorCodes.FileNotFoundInContainer, $"{input.Id}", "cms-kit-media"]);

            return new MemoryStream(entityF.Blob.FileContent);
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return (await Repository.FindAsync(id, includeDetails: false) != null);
        }

        public async Task<bool> IsExistsAsync(string name)
        {
            string fn = Path.GetFileNameWithoutExtension(name);
            string extName = Path.GetExtension(name);

            return (await Repository.FindAsync(p => p.Name == fn && p.MIME.RefenceExtName == extName, includeDetails: false) != null);
        }

        public async Task<IList<FileStoreDto>> GetPhotosAsync(FileStoreSearchRequestDto input)
        {
            var result = await MapToGetListOutputDtosAsync((await Repository.GetQueryableAsync())
                .Where(p => p.CreatorId == CurrentUser.Id && p.Classification.Name == "Photo")
                .ToList());

            if (result.Any())
            {
                foreach (var item in result)
                {
                    IdentityUser creatorData = await userManagerService.FindByIdAsync(item.CreatorId.ToString());

                    if (creatorData != null)
                        item.Creator = $"{creatorData.Surname}{creatorData.Name}";

                    if (item.ModifierId.HasValue)
                    {
                        IdentityUser modifierData = await userManagerService.FindByIdAsync(item.ModifierId.Value.ToString());

                        if (modifierData != null)
                            item.Modifier = $"{modifierData.Surname}{modifierData.Name}";
                    }
                }
            }
            return result;
        }

        public async Task<IList<FileStoreDto>> GetAllPublicPhotosAsync(int maxPhotoAmount)
        {
            var query = (await Repository.GetQueryableAsync())
                .Where(p => p.CreatorId == CurrentUser.Id && p.Classification.Name == "Photo" && p.IsPublic == true);

            if (maxPhotoAmount != -1 || maxPhotoAmount > 0)
            {
                query = query.OrderBy(p => p.CreationTime).Reverse().Take(maxPhotoAmount);
            }
            else
            {
                query = query.OrderBy(p => p.CreationTime).Reverse().Take(10);
            }

            var result = await MapToGetListOutputDtosAsync(query.ToList());

            if (result.Any())
            {
                foreach (var item in result)
                {
                    IdentityUser creatorData = await userManagerService.FindByIdAsync(item.CreatorId.ToString());

                    if (creatorData != null)
                        item.Creator = $"{creatorData.Surname}{creatorData.Name}";

                    if (item.ModifierId.HasValue)
                    {
                        IdentityUser modifierData = await userManagerService.FindByIdAsync(item.ModifierId.Value.ToString());

                        if (modifierData != null)
                            item.Modifier = $"{modifierData.Surname}{modifierData.Name}";
                    }
                }
            }
            return result;
        }
    }
}
