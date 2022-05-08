using Ediux.HomeSystem.AdditionalSystemFunctions4Users;

using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;

namespace Ediux.HomeSystem.ProductKeysBook
{
    public class ProductKeysBookAppService : HomeSystemCrudAppService<ProductKeys, ProductKeysBookDto, Guid, AbpSearchRequestDto>, IProductKeysBookAppService
    {
        private readonly IAuthorizationService authorizationService;

        public ProductKeysBookAppService(IRepository<ProductKeys, Guid> repository, IAuthorizationService authorizationService, IdentityUserManager identityUserManager)
            : base(repository, identityUserManager)
        {
            this.authorizationService = authorizationService;
        }

        public async override Task<ProductKeysBookDto> CreateAsync(ProductKeysBookDto input)
        {
            var entity = MapToEntity(input);
            input.MapExtraPropertiesTo(entity, MappingPropertyDefinitionChecks.None);
            return await MapToGetOutputDtoAsync(await Repository.InsertAsync(entity));
        }

        public async override Task<ProductKeysBookDto> UpdateAsync(Guid id, ProductKeysBookDto input)
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
                updated.MapExtraPropertiesTo(entity, MappingPropertyDefinitionChecks.None);
            }

            if (entity.ProductKey != updated.ProductKey)
            {
                entity.ProductKey = updated.ProductKey;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (entity.ProductName != updated.ProductName)
            {
                entity.ProductName = updated.ProductName;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            if (entity.Shared != updated.Shared)
            {
                entity.Shared = updated.Shared;
                entity.LastModificationTime = DateTime.UtcNow;
                entity.LastModifierId = CurrentUser.Id;
            }

            return await MapToGetOutputDtoAsync(await Repository.UpdateAsync(entity));
        }
        public async override Task<PagedResultDto<ProductKeysBookDto>> GetListAsync(AbpSearchRequestDto input)
        {

            var grantResult = await authorizationService.AuthorizeAsync(FeatureManagementPermissions.ManageHostFeatures);

            var shared_result = (await Repository.GetQueryableAsync())
                  .WhereIf(grantResult.Succeeded == false, p => p.Shared == true && p.CreatorId != CurrentUser.Id)
                  .Distinct();

            var result = (await Repository.GetQueryableAsync())
                .Union(shared_result)
                .WhereIf(grantResult.Succeeded == false, p => p.CreatorId == CurrentUser.Id)
                .WhereIf(!string.IsNullOrWhiteSpace(input.Search), p => p.ProductKey.Contains(input.Search) || p.ProductName.Contains(input.Search) || p.ProductName.Contains(input.Search))
                .Distinct();

            var output = result.Skip(input.SkipCount)
                    .Take(input.MaxResultCount)
                    .ToList();

            List<ProductKeysBookDto> finallyOut = await MapToGetListOutputDtosAsync(output);

            return new PagedResultDto<ProductKeysBookDto>(result.LongCount(), finallyOut);
        }
    }
}
