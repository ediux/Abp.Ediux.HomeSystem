using Ediux.HomeSystem.Data;
using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.MediaDescriptors;
using Ediux.HomeSystem.Models.DTOs.Files;
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
using Volo.CmsKit.Admin.MediaDescriptors;
using Volo.CmsKit.MediaDescriptors;

namespace Ediux.HomeSystem.Files
{
    public class FileStoreAppService : CrudAppService<File_Store, FileStoreDTO, Guid, FileStoreRequestDTO>, IFileStoreAppService
    {
        protected readonly IBlobContainer<MediaContainer> _blobContainer;
        protected readonly IBlobContainer<AutoSaveContainer> _autoSaveContainer;
        private readonly IDistributedCache<FileStoreDTO, Guid> _cache;
        private readonly IRepository<MIMEType> _mimeTypeRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public FileStoreAppService(IRepository<File_Store, Guid> repository,
            IBlobContainer<MediaContainer> blobContainer,
            IBlobContainer<AutoSaveContainer> autoSaveContainer,
            IRepository<MIMEType> mimeTypeRepository,
            IIdentityUserRepository identityUserRepository,
            IDistributedCache<FileStoreDTO, Guid> cache) : base(repository)
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
            var entity = await Repository.GetAsync(id);

            if (entity == null)
            {
                return null;
            }

            if (input.FileContent != null)
            {
                entity.Size = input.FileContent.LongLength;

                if (input.IsAutoSaveFile)
                {
                    await _autoSaveContainer.SaveAsync(id.ToString(), input.FileContent, overrideExisting: true);
                }
                else
                {
                    await _blobContainer.SaveAsync(id.ToString(), input.FileContent, overrideExisting: true);
                }
            }

            MIMEType mIMETypesDTO = await _mimeTypeRepository.FindAsync(p => p.RefenceExtName == input.ExtName);

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

            if (entity.Name != input.Name)
            {
                entity.Name = input.Name;
            }

            if (entity.ExtName != input.ExtName)
            {
                entity.ExtName = input.ExtName;
            }

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

            if (entity.ExtraProperties.ContainsKey(HomeSystemConsts.IsAutoSaveFile))
            {
                if (((bool)entity.ExtraProperties[HomeSystemConsts.IsAutoSaveFile]) != input.IsAutoSaveFile)
                {
                    entity.ExtraProperties[HomeSystemConsts.IsAutoSaveFile] = input.IsAutoSaveFile;
                }
            }
            else
            {
                entity.ExtraProperties.Add(HomeSystemConsts.IsAutoSaveFile, input.IsAutoSaveFile);
            }

            if (entity.OriginFullPath != input.OriginFullPath)
            {
                entity.OriginFullPath = input.OriginFullPath;
            }

            entity = await Repository.UpdateAsync(entity, autoSave: true);

            input = MapToGetOutputDto(entity);

            if ((await _cache.GetAsync(id)) != null)
            {
                if (input.IsAutoSaveFile)
                {
                    input.FileContent = (await _autoSaveContainer.GetAsync(id.ToString())).GetAllBytes();
                }
                else
                {
                    input.FileContent = (await _blobContainer.GetAsync(id.ToString())).GetAllBytes();
                }

                await _cache.SetAsync(id, input,
                    options: new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                    });
            }

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
            FileStoreDTO entity = await _cache.GetOrAddAsync(
                        id,
                        async () =>
                        {
                            var entityF = await GetAsync(id);
                            Stream stream = null;
                            if (entityF.IsAutoSaveFile)
                            {
                                stream = await _autoSaveContainer.GetAsync(entityF.Id.ToString());
                                //entityF.FileContent = (await _autoSaveContainer.GetAsync(entityF.Id.ToString())).GetAllBytes();
                            }
                            else
                            {
                                stream = await _blobContainer.GetAsync(entityF.Id.ToString());
                            }

                            entityF.FileContent = stream.GetAllBytes();
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

            return new RemoteStreamContent(new MemoryStream(entity.FileContent), $"{entity.Name}{entity.ExtName}")
            {
                ContentType = entity.ContentType
            };

        }

        public async override Task<PagedResultDto<FileStoreDTO>> GetListAsync(FileStoreRequestDTO input)
        {
            bool hasSucceededPolicy = (await AuthorizationService.AuthorizeAsync(HomeSystemPermissions.Files.Special)).Succeeded;

            var result = (await Repository.GetQueryableAsync())
                .WhereIf(!hasSucceededPolicy, p => p.CreatorId == CurrentUser.Id)
                 .ToList();
            
            int totalCount = result.Count();

            var output = await MapToGetListOutputDtosAsync(result);

            if (result.Any())
            {
                var output2 = output.AsQueryable()
                     .WhereIf(input.Search.IsNullOrWhiteSpace() == false,
                     w => w.Name.Contains(input.Search) ||
                     w.ExtName.Contains(input.Search) ||
                     w.OriginFullPath.Contains(input.Search) ||
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

        public async Task<IList<FileStoreDTO>> GetPhotosAsync(FileStoreRequestDTO input)
        {
            var result = await MapToGetListOutputDtosAsync((await Repository.GetQueryableAsync())
                .Where(p => p.CreatorId == CurrentUser.Id && p.MIME.MIME.Contains("image/"))
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
