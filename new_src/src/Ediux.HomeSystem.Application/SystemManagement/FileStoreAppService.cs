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
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.CmsKit.MediaDescriptors;

namespace Ediux.HomeSystem.SystemManagement
{
    public class FileStoreAppService : HomeSystemCrudAppService<File_Store, FileStoreDto, Guid, FileStoreSearchRequestDto>, IFileStoreAppService
    {
        protected readonly IBlobContainer<MediaContainer> _blobContainer;
        protected readonly IBlobContainer<PluginsContainer> _pluginContainer;

        private readonly IDistributedCache<FileStoreDto, Guid> _cache;
        private readonly IMIMETypeManagerAppService _mIMETypeManager;
        private readonly IIdentityUserRepository _identityUserRepository;

        public FileStoreAppService(IRepository<File_Store, Guid> repository,
            IBlobContainer<MediaContainer> blobContainer,
            IBlobContainer<PluginsContainer> pluginContainer,
            IMIMETypeManagerAppService mIMETypeManager,
            IIdentityUserRepository identityUserRepository,
            IDistributedCache<FileStoreDto, Guid> cache) : base(repository)
        {
            _blobContainer = blobContainer;
            _pluginContainer = pluginContainer;

            _mIMETypeManager = mIMETypeManager;
            _identityUserRepository = identityUserRepository;
            _cache = cache;

        }

        public async override Task<FileStoreDto> GetAsync(Guid id)
        {
            var entity = (await Repository.WithDetailsAsync()).FirstOrDefault(p => p.Id == id);

            if (entity != null)
            {
                FileStoreDto fileStoreDto = await MapToGetOutputDtoAsync(entity);
                switch (fileStoreDto.Blob.BlobContainerName)
                {
                    case "":
                        break;
                }
                return fileStoreDto;
                //var stream = await _blobContainer.GetAsync(id.ToString());
                //entity.FileContent = await stream.GetAllBytesAsync();
                //var mimeMapping = await _mIMETypeManager.GetByExtNameAsync(entity.ExtName);

                //entity.ContentType = (await _mimeTypeRepository.FindAsync(p => p.RefenceExtName == entity.ExtName))?.TypeName ?? HomeSystemConsts.DefaultContentType;
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
                if (input.MIMETypes == null)
                {
                    input.MIMETypes = new MIMETypesDto()
                    {
                        ContentType = HomeSystemConsts.DefaultContentType,
                        RefenceExtName = input.ExtName,
                        Description = $"{input.ExtName} File"
                    };
                }

                mIMETypesDTO = await _mIMETypeManager.CreateAsync(input.MIMETypes);
            }

            input.MIMETypes = mIMETypesDTO;

            //else
            //{
            //    entity.MIMETypeId = mIMETypesDTO.Id;
            //    entity.MIME = mIMETypesDTO;
            //}

            var entity = MapToEntity(input);
            entity = await Repository.InsertAsync(entity);

            switch (input.Blob.BlobContainerName)
            {
                default:
                case "cms-kit-media":
                    if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                    {
                        await _blobContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent, overrideExisting: true);
                    }

                    break;
                case "auto-save-temp":
                case "plugins":
                    if (input.Blob != null && input.Blob.FileContent != null && input.Blob.FileContent.LongLength > 0)
                    {
                        await _pluginContainer.SaveAsync(entity.Id.ToString(), input.Blob.FileContent, overrideExisting: true);
                    }
                    break;
            }

            return input;
        }

        public async override Task<FileStoreDto> UpdateAsync(Guid id, FileStoreDto input)
        {
            var entity = await Repository.GetAsync(id);

            if (entity == null)
            {
                throw new Volo.Abp.UserFriendlyException("File instance is missing.");
            }

            if (input.Blob != null)
            {
                if (input.Blob.FileContent != null)
                {
                    if (entity.Size != input.Blob.FileContent.LongLength)
                    {
                        entity.Size = input.Blob.FileContent.LongLength;
                    }
                }


                //if (input.IsAutoSaveFile)
                //{
                //    await _autoSaveContainer.SaveAsync(id.ToString(), input.FileContent, overrideExisting: true);
                //}
                //else
                //{
                //    await _blobContainer.SaveAsync(id.ToString(), input.FileContent, overrideExisting: true);
                //}
            }

            var mIMETypesDTO = await _mIMETypeManager.GetByExtNameAsync(input.ExtName);

            if (mIMETypesDTO == null)
            {
                mIMETypesDTO = input.MIMETypes;
                mIMETypesDTO.ContentType = HomeSystemConsts.DefaultContentType;

                input.MIMETypes = await _mIMETypeManager.CreateAsync(mIMETypesDTO);
            }

            if (entity.Name != input.Name)
            {
                entity.Name = input.Name;
            }

            //if (entity.ExtName != input.ExtName)
            //{
            //    entity.ExtName = input.ExtName;
            //}

            if (entity.ExtraProperties.ContainsKey(HomeSystemConsts.Description))
            {
                string desc = ((string)entity.ExtraProperties[HomeSystemConsts.Description]);

                if (desc.IsNullOrWhiteSpace() && !input.Description.IsNullOrWhiteSpace())
                {
                    entity.ExtraProperties[HomeSystemConsts.Description] = input.Description;
                }
                else
                {
                    if (!input.Description.IsNullOrWhiteSpace()
                        && !((string)entity.ExtraProperties[HomeSystemConsts.Description]).Equals(input.Description, StringComparison.OrdinalIgnoreCase))
                    {
                        entity.ExtraProperties[HomeSystemConsts.Description] = input.Description;
                    }
                }
            }
            else
            {
                entity.ExtraProperties.Add(HomeSystemConsts.Description, input.Description);
            }

            //if (entity.ExtraProperties.ContainsKey(HomeSystemConsts.IsAutoSaveFile))
            //{
            //    if (((bool)entity.ExtraProperties[HomeSystemConsts.IsAutoSaveFile]) != input.IsAutoSaveFile)
            //    {
            //        entity.ExtraProperties[HomeSystemConsts.IsAutoSaveFile] = input.IsAutoSaveFile;
            //    }
            //}
            //else
            //{
            //    entity.ExtraProperties.Add(HomeSystemConsts.IsAutoSaveFile, input.IsAutoSaveFile);
            //}

            //if (entity.OriginFullPath != input.OriginFullPath)
            //{
            //    entity.OriginFullPath = input.OriginFullPath;
            //}

            entity = await Repository.UpdateAsync(entity, autoSave: true);

            input = MapToGetOutputDto(entity);

            //if ((await _cache.GetAsync(id)) != null)
            //{
            //    if (input.IsAutoSaveFile)
            //    {
            //        input.FileContent = (await _autoSaveContainer.GetAsync(id.ToString())).GetAllBytes();
            //    }
            //    else
            //    {
            //        input.FileContent = (await _blobContainer.GetAsync(id.ToString())).GetAllBytes();
            //    }

            await _cache.SetAsync(id, input,
                options: new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                });
            //}

            return input;
        }

        public async override Task DeleteAsync(Guid id)
        {
            if (await _blobContainer.DeleteAsync(id.ToString()))
            {
                await base.DeleteAsync(id);
            }

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
                            //if (entityF.IsAutoSaveFile)
                            //{
                            //    stream = await _autoSaveContainer.GetAsync(entityF.Id.ToString());
                            //    //entityF.FileContent = (await _autoSaveContainer.GetAsync(entityF.Id.ToString())).GetAllBytes();
                            //}
                            //else
                            //{
                            //    stream = await _blobContainer.GetAsync(entityF.Id.ToString());
                            //}

                            entityF.Blob.FileContent = stream.GetAllBytes();
                            stream.Close();
                            stream.Dispose();
                            return entityF;
                        },
                        () => new DistributedCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                        });

            if (entity == null)
                return null;

            return new RemoteStreamContent(new MemoryStream(entity.Blob.FileContent), $"{entity.Name}{entity.ExtName}")
            {
                ContentType = entity.MIMETypes.ContentType
            };

        }

        public async override Task<PagedResultDto<FileStoreDto>> GetListAsync(FileStoreSearchRequestDto input)
        {
            bool hasSucceededPolicy = (await AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Special)).Succeeded;

            var result = (await Repository.WithDetailsAsync(p=>p.MIME))
                .WhereIf(!hasSucceededPolicy, p => p.CreatorId == CurrentUser.Id)
                .WhereIf(input.Classification_Id.HasValue, p => p.FileClassificationId == input.Classification_Id)
                .WhereIf(input.CurrentUser_Id.HasValue, p => p.CreatorId == input.CurrentUser_Id)
                .ToList();

            int totalCount = result.Count();

            var output = await MapToGetListOutputDtosAsync(result);

            if (result.Any())
            {
                var output2 = output.AsQueryable()
                     .WhereIf(input.Search.IsNullOrWhiteSpace() == false,
                     w => w.Name.Contains(input.Search) ||
                     w.ExtName.Contains(input.Search) ||
                     (w.Description != null && w.Description.Contains(input.Search)));

                totalCount = output2.Count();

                output = output2
                    .PageBy(input.SkipCount, input.MaxResultCount)
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

            return new PagedResultDto<FileStoreDto>(result.Count, output);
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

            return (await Repository.FindAsync(p => p.Name == fn && p.MIME.RefenceExtName == extName, includeDetails: false) != null);
        }

        public async Task<IList<FileStoreDto>> GetPhotosAsync(FileStoreSearchRequestDto input)
        {
            var result = await MapToGetListOutputDtosAsync((await Repository.GetQueryableAsync())
                .Where(p => p.CreatorId == CurrentUser.Id && p.MIME.TypeName.Contains("image/"))
                .ToList());

            if (result.Any())
            {
                foreach (var item in result)
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
            return result;
        }
    }
}
