using Ediux.HomeSystem.Localization;
using Ediux.HomeSystem.Permissions;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.SystemManagement
{
    public class SystemMessageAppService : HomeSystemCrudAppService<InternalSystemMessages, SystemMessageDto, Guid, AbpSearchRequestDto>, ISystemMessageAppService
    {
        //private UserManager<IdentityUser> _userManager;
        public SystemMessageAppService(IRepository<InternalSystemMessages, Guid> repository,
            IdentityUserManager identityUserManager) : base(repository, identityUserManager)
        {
            //_userManager = userManager;
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
                .WhereIf(input.Search.IsNullOrWhiteSpace() == false, p => p.Subject.Contains(input.Search) || p.Message.Contains(input.Search)
                || (p.RefenceMessage != null && p.RefenceMessage.Subject.Contains(input.Search) || p.RefenceMessage.Message.Contains(input.Search)))
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

        public async Task<SystemMessageDto> CreateSystemMessageAsync(Guid userId, string message, bool sendMail = false, bool push = false, ILogger logger = null, LogLevel logLevel = LogLevel.None)
        {
            var adminUser = await userManagerService.FindByNameAsync("admin");

            if (adminUser == null)
            {
                throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.GeneralError].Value, code: HomeSystemDomainErrorCodes.GeneralError, logLevel: LogLevel.Error);
            }

            if (logger != null)
            {
                switch (logLevel)
                {
                    default:
                    case LogLevel.None:
                        logger.Log(LogLevel.None, message);
                        break;
                    case LogLevel.Information:
                        logger.LogInformation(message);
                        break;
                    case LogLevel.Error:
                        logger.LogError(message);
                        break;
                    case LogLevel.Warning:
                        logger.LogWarning(message);
                        break;
                    case LogLevel.Debug:
                        logger.LogDebug(message);
                        break;
                    case LogLevel.Critical:
                        logger.LogCritical(message);
                        break;
                    case LogLevel.Trace:
                        logger.LogTrace(message);
                        break;
                }
            }

            return await CreateAsync(new SystemMessageDto()
            {
                Subject = L[HomeSystemResource.Features.SystemMessage.InternalSubject].Value,
                Message = message,
                CreationTime = DateTime.Now,
                CreatorId = userId,
                From = ObjectMapper.Map<IdentityUser, UserInforamtionDto>(adminUser),
                IsEMail = sendMail,
                IsPush = push,
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
                sysMsg.ReceiveTime = DateTime.Now;
                await UpdateAsync(systemMessageId, sysMsg);
            }
        }

        public async Task<SystemMessageDto> ReplyMessageAsync(SystemMessageDto originalMessage, string message, bool sendMail, bool push)
        {
            if (originalMessage != null && originalMessage.IsRead == false)
            {
                originalMessage.IsRead = true;
                originalMessage.ReceiveTime = DateTime.Now;

                await UpdateAsync(originalMessage.Id, originalMessage);
            }

            SystemMessageDto systemMessageDto = new SystemMessageDto()
            {
                CreatorId = originalMessage.From.Id,
                CreationTime = DateTime.Now,
                From = new UserInforamtionDto() { Id = originalMessage.CreatorId.Value },
                IsReply = true,
                Message = message,
                Subject = L[HomeSystemResource.Features.SystemMessage.ReSubject, originalMessage.Subject].Value,
                IsEMail = sendMail,
                IsPush = push,
                SendTime = DateTime.Now,
                RefenceMessage = originalMessage
            };

            systemMessageDto = await CreateAsync(systemMessageDto);

            return systemMessageDto;
        }
    }
}
