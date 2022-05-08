using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem
{
    /* Inherit your application services from this class.
     */
    public abstract class HomeSystemAppService : ApplicationService
    {
        protected HomeSystemAppService()
        {
            LocalizationResource = typeof(HomeSystemResource);
        }
    }

    public abstract class HomeSystemCrudAppService<TEntity, TEntityDto, TKey, TGetListInput> : CrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>
        where TEntityDto : IEntityDto<TKey>
    {

        protected HomeSystemCrudAppService(IRepository<TEntity, TKey> repository, IdentityUserManager identityUserManager) : base(repository)
        {
            LocalizationResource = typeof(HomeSystemResource);

            userManagerService = identityUserManager;
        }

        protected readonly IdentityUserManager userManagerService;


        protected virtual async Task ApplyFullAuditedUserInformationAsync<TEntityWithUserDto>(TEntityWithUserDto data)
            where TEntityWithUserDto : FullAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            if (data == null)
                return;

            Guid? createdUserId = ((IMayHaveCreator)data).CreatorId;

            if (createdUserId.HasValue)
            {
                IdentityUser creatorData = await userManagerService.FindByIdAsync(createdUserId.Value.ToString());

                if (creatorData != null)
                    data.Creator = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(creatorData);

                if (data.LastModifierId.HasValue)
                {
                    IdentityUser modifierData = await userManagerService.FindByIdAsync(data.LastModifierId.Value.ToString());

                    if (modifierData != null)
                        data.LastModifier = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(modifierData);
                }

                if (data.DeleterId.HasValue)
                {
                    IdentityUser deleterData = await userManagerService.FindByIdAsync(data.DeleterId.Value.ToString());

                    if (deleterData != null)
                        data.Deleter = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(deleterData);
                }
            }

        }

        protected virtual async Task ApplyFullAuditedUserInformationToDTOsAsync<TEntityWithUserDto>(IListResult<TEntityWithUserDto> data)
            where TEntityWithUserDto : FullAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            foreach (var entity in data.Items)
            {
                await ApplyFullAuditedUserInformationAsync(entity);
            }
        }

        protected virtual async Task ApplyUserInformationAsync<TEntityWithUserDto>(TEntityWithUserDto data)
            where TEntityWithUserDto : AuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            if (data == null)
                return;

            Guid? createdUserId = ((IMayHaveCreator)data).CreatorId;

            if (createdUserId.HasValue)
            {
                IdentityUser creatorData = await userManagerService.FindByIdAsync(createdUserId.Value.ToString());

                if (creatorData != null)
                    data.Creator = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(creatorData);

                if (data.LastModifierId.HasValue)
                {
                    IdentityUser modifierData = await userManagerService.FindByIdAsync(data.LastModifierId.Value.ToString());

                    if (modifierData != null)
                        data.LastModifier = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(modifierData);
                }


            }

        }

        protected virtual async Task ApplyAuditedUserInformationToDTOsAsync<TEntityWithUserDto>(IListResult<TEntityWithUserDto> data)
            where TEntityWithUserDto : AuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            foreach (var entity in data.Items)
            {
                await ApplyUserInformationAsync(entity);
            }
        }

        protected virtual async Task ApplyCreatedUserInformationAsync<TEntityWithUserDto>(TEntityWithUserDto data)
            where TEntityWithUserDto : CreationAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            if (data == null)
                return;

            Guid? createdUserId = ((IMayHaveCreator)data).CreatorId;

            if (createdUserId.HasValue)
            {
                IdentityUser creatorData = await userManagerService.FindByIdAsync(createdUserId.Value.ToString());

                if (creatorData != null)
                    data.Creator = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(creatorData);
            }
        }

        protected virtual async Task ApplyCreatedUserInformationToDTOsAsync<TEntityWithUserDto>(IListResult<TEntityWithUserDto> data)
            where TEntityWithUserDto : CreationAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            foreach (var entity in data.Items)
            {
                await ApplyCreatedUserInformationAsync(entity);
            }
        }


        protected virtual async Task ApplyExtensibleFullAuditedUserInformationAsync<TEntityWithUserDto>(TEntityWithUserDto data)
           where TEntityWithUserDto : ExtensibleFullAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            if (data == null)
                return;

            Guid? createdUserId = ((IMayHaveCreator)data).CreatorId;

            if (createdUserId.HasValue)
            {
                IdentityUser creatorData = await userManagerService.FindByIdAsync(createdUserId.Value.ToString());

                if (creatorData != null)
                    data.Creator = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(creatorData);

                if (data.LastModifierId.HasValue)
                {
                    IdentityUser modifierData = await userManagerService.FindByIdAsync(data.LastModifierId.Value.ToString());

                    if (modifierData != null)
                        data.LastModifier = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(modifierData);
                }

                if (data.DeleterId.HasValue)
                {
                    IdentityUser deleterData = await userManagerService.FindByIdAsync(data.DeleterId.Value.ToString());

                    if (deleterData != null)
                        data.Deleter = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(deleterData);
                }
            }

        }

        protected virtual async Task ApplyExtensibleUserInformationAsync<TEntityWithUserDto>(TEntityWithUserDto data)
            where TEntityWithUserDto : ExtensibleAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            if (data == null)
                return;

            Guid? createdUserId = ((IMayHaveCreator)data).CreatorId;

            if (createdUserId.HasValue)
            {
                IdentityUser creatorData = await userManagerService.FindByIdAsync(createdUserId.Value.ToString());

                if (creatorData != null)
                    data.Creator = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(creatorData);

                if (data.LastModifierId.HasValue)
                {
                    IdentityUser modifierData = await userManagerService.FindByIdAsync(data.LastModifierId.Value.ToString());

                    if (modifierData != null)
                        data.LastModifier = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(modifierData);
                }
            }
        }

        protected virtual async Task ApplyExtensibleCreatedUserInformationAsync<TEntityWithUserDto>(TEntityWithUserDto data)
            where TEntityWithUserDto : ExtensibleCreationAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            if (data == null)
                return;

            Guid? createdUserId = ((IMayHaveCreator)data).CreatorId;

            if (createdUserId.HasValue)
            {
                IdentityUser creatorData = await userManagerService.FindByIdAsync(createdUserId.Value.ToString());

                if (creatorData != null)
                    data.Creator = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(creatorData);
            }

        }

        protected virtual async Task ApplyExtensibleFullAuditedUserInformationToDTOsAsync<TEntityWithUserDto>(IListResult<TEntityWithUserDto> data)
           where TEntityWithUserDto : ExtensibleFullAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            foreach (var entity in data.Items)
            {
                await ApplyExtensibleFullAuditedUserInformationAsync(entity);
            }
        }

        protected virtual async Task ApplyExtensibleAuditedUserInformationToDTOsAsync<TEntityWithUserDto>(IListResult<TEntityWithUserDto> data)
           where TEntityWithUserDto : ExtensibleAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            foreach (var entity in data.Items)
            {
                await ApplyExtensibleUserInformationAsync(entity);
            }
        }

        protected virtual async Task ApplyExtensibleCreatedUserInformationToDTOsAsync<TEntityWithUserDto>(IListResult<TEntityWithUserDto> data)
            where TEntityWithUserDto : ExtensibleCreationAuditedEntityWithUserDto<TKey, UserInforamtionDto>
        {
            foreach (var entity in data.Items)
            {
                await ApplyExtensibleCreatedUserInformationAsync(entity);
            }
        }

    }
}
