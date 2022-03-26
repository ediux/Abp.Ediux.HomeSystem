
using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{

    public class PasswordStoreAppService : CrudAppService<UserPasswordStore, PasswordStoreDto, long, AbpSearchRequestDto>, IPasswordStoreAppService
    {
        private readonly IAuthorizationService authorizationService;

        public PasswordStoreAppService(IRepository<UserPasswordStore, long> repository, IAuthorizationService authorizationService) : base(repository)
        {
            this.authorizationService = authorizationService;            
        }

        public async override Task<PagedResultDto<PasswordStoreDto>> GetListAsync(AbpSearchRequestDto input)
        {

            var result = (await Repository.GetQueryableAsync())
                    .WhereIf(CurrentUser.IsAuthenticated, p => p.CreatorId == CurrentUser.Id)
                    .WhereIf(!string.IsNullOrEmpty(input.Search), p => p.Account.Contains(input.Search) || p.SiteName.Contains(input.Search) || p.Site.Contains(input.Search))
                    .Distinct()
                    .OrderBy(o => o.SiteName)
                    .ThenBy(o => o.CreationTime);

            int totalCount = result.Count();

            var output = result
                .PageBy(input.SkipCount, input.MaxResultCount)
                .ToList();

            return new PagedResultDto<PasswordStoreDto>(result.LongCount(), await MapToGetListOutputDtosAsync(output));

        }
        public async override Task<PasswordStoreDto> CreateAsync(PasswordStoreDto input)
        {
            var entity = await MapToEntityAsync(input);
            input.MapExtraPropertiesTo(entity, MappingPropertyDefinitionChecks.None);
            return await MapToGetOutputDtoAsync(await Repository.InsertAsync(entity));
        }

        public async override Task<PasswordStoreDto> UpdateAsync(long id, PasswordStoreDto input)
        {
            var entity = await Repository.FindAsync(id, includeDetails: true);// MapToEntity(input);

            if (entity == null) return input;

            var updated = await MapToEntityAsync(input);

            input.MapExtraPropertiesTo(updated, MappingPropertyDefinitionChecks.None);

            if (entity.ExtraProperties != null)
            {
                var removeExtraProperties = entity.ExtraProperties.Keys.Except(updated.ExtraProperties.Keys);

                if (removeExtraProperties.Any())
                {
                    foreach (var removeKey in removeExtraProperties)
                    {
                        entity.RemoveProperty(removeKey);
                    }
                }

                updated.MapExtraPropertiesTo(entity, MappingPropertyDefinitionChecks.None);
            }
            else
            {
                entity.ExtraProperties.Clear();
                updated.MapExtraPropertiesTo(entity, MappingPropertyDefinitionChecks.None);
            }

            if (entity.Account != updated.Account)
            {
                entity.Account = updated.Account;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (entity.IsHistory != updated.IsHistory)
            {
                entity.IsHistory = updated.IsHistory;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (entity.Site != updated.Site)
            {
                entity.Site = updated.Site;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (entity.SiteName != updated.SiteName)
            {
                entity.SiteName = updated.SiteName;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (updated.Password.IsNullOrWhiteSpace() == false && entity.Password != updated.Password)
            {
                entity.Password = updated.Password;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            return await MapToGetOutputDtoAsync(await Repository.UpdateAsync(entity));
        }
    }
}
