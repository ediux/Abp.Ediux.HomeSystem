using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.FeatureManagement;

namespace Ediux.HomeSystem.SystemManagement
{
    public class SystemMessageAppService : HomeSystemCrudAppService<InternalSystemMessages, SystemMessageDto, Guid, AbpSearchRequestDto>, ISystemMessageAppService
    {
        public SystemMessageAppService(IRepository<InternalSystemMessages, Guid> repository) : base(repository)
        {
        }

        public override async Task<PagedResultDto<SystemMessageDto>> GetListAsync(AbpSearchRequestDto input)
        {
            bool hasGrantBySpecial = false;
            try
            {
                await CheckPolicyAsync(HomeSystemPermissions.SystemMessages.Special);
                hasGrantBySpecial = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, L[HomeSystemDomainErrorCodes.NoPermissionByName, L[HomeSystemResource.Permissions.SystemMessages.Special].Value].Value);
                hasGrantBySpecial = false;
            }

            var query = (await Repository.WithDetailsAsync())
                .WhereIf(input.Search.IsNullOrWhiteSpace() == false, p => p.Subject.Contains(input.Search) || p.Message.Contains(input.Search))
                .WhereIf(hasGrantBySpecial == false, p => p.CreatorId == CurrentUser.Id);

            var result = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<SystemMessageDto>(result.Count, await MapToGetListOutputDtosAsync(result));
        }

        public override Task<SystemMessageDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        public async Task<SystemMessageDto> CreateSystemMessageAsync(Guid userId, string message, bool sendMail, bool push, ILogger logger)
        {
            return await CreateAsync(new SystemMessageDto() { 
                 Subject = ""
            });
        }

        public Task<PagedResultDto<SystemMessageDto>> GetListByUserAsync(Guid userId, DateTime? start, DateTime? end)
        {
            throw new NotImplementedException();
        }

        public Task MarkupReadByUserAysnc(Guid userId, Guid systemMessageId)
        {
            throw new NotImplementedException();
        }
    }
}
