using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.FeatureManagement;

namespace Ediux.HomeSystem.SystemManagement
{
    public class SystemMessageAppService : HomeSystemCrudAppService<InternalSystemMessages, SystemMessageDto, Guid, AbpSearchRequestDto>, ISystemMessageAppService
    {
        private UserManager<IdentityUser> _userManager;
        public SystemMessageAppService(IRepository<InternalSystemMessages, Guid> repository,
            UserManager<IdentityUser> userManager) : base(repository)
        {
            _userManager = userManager;
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

            var query = (await Repository.WithDetailsAsync(p => p.From, p => p.RefenceMessage))
                .WhereIf(input.Search.IsNullOrWhiteSpace() == false, p => p.Subject.Contains(input.Search) || p.Message.Contains(input.Search))
                .WhereIf(hasGrantBySpecial == false, p => p.CreatorId == CurrentUser.Id);

            var result = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<SystemMessageDto>(result.Count, await MapToGetListOutputDtosAsync(result));
        }

        public override async Task<SystemMessageDto> GetAsync(Guid id)
        {
            var query = (await Repository.WithDetailsAsync(p => p.AttachFiles, p => p.From, p => p.RefenceMessage))
               .Where(p => p.Id == id);

            return await MapToGetOutputDtoAsync(await AsyncExecuter.SingleOrDefaultAsync(query));
        }

        public async Task<SystemMessageDto> CreateSystemMessageAsync(Guid userId, string message, bool sendMail, bool push, ILogger logger)
        {
            var adminUser = await _userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.GeneralError].Value, code: HomeSystemDomainErrorCodes.GeneralError, logLevel: LogLevel.Error);
            }

            return await CreateAsync(new SystemMessageDto()
            {
                Subject = L[HomeSystemResource.Features.SystemMessage.InternalSubject].Value,
                Message = message,
                CreationTime = DateTime.Now,
                CreatorId = userId,
                From = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(adminUser),
                IsEMail = false,
                IsPush = false,
                SendTime = DateTime.Now
            });
        }

        public async Task<PagedResultDto<SystemMessageDto>> GetListByUserAsync(Guid userId, DateTime? start, DateTime? end)
        {
            var query = (await Repository.WithDetailsAsync(p => p.AttachFiles, p => p.From, p => p.RefenceMessage))
              .Where(p => p.CreatorId == userId)
              .WhereIf(start.HasValue, p => p.SendTime >= start)
              .WhereIf(end.HasValue, p => p.SendTime <= end);

            var result = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<SystemMessageDto>(result.Count, await MapToGetListOutputDtosAsync(result));

        }

        public async Task MarkupReadByUserAysnc(Guid userId, Guid systemMessageId)
        {
            var query = (await Repository.WithDetailsAsync(p => p.AttachFiles, p => p.From, p => p.RefenceMessage))
             .Where(p => p.CreatorId == userId && p.Id == systemMessageId);

            var sysMsg = await MapToGetOutputDtoAsync(await AsyncExecuter.SingleOrDefaultAsync(query));

            if (sysMsg.IsRead == false)
            {
                sysMsg.IsRead = true;
                await UpdateAsync(systemMessageId, sysMsg);
            }
        }

        public Task<SystemMessageDto> ReplyMessageAsync(string message, bool sendMail, bool push)
        {
            throw new NotImplementedException();
        }
    }
}
