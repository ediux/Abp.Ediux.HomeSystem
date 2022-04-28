
using Ediux.HomeSystem.SystemManagement;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class PersonalCalendarAppService : CrudAppService<PersonalCalendar, PersonalCalendarDto, Guid, PersonalCalendarRequestDto>, IPersonalCalendarAppService
    {
        private readonly IRepository<InternalSystemMessages, Guid> _systemMessageRepository;
        private readonly IIdentityUserRepository _identityUserRepository;
        public PersonalCalendarAppService(IRepository<PersonalCalendar, Guid> repository,
            IRepository<InternalSystemMessages, Guid> systemMessageRepository,
            IIdentityUserRepository identityUserRepository) : base(repository)
        {
            _systemMessageRepository = systemMessageRepository;
            _identityUserRepository = identityUserRepository;
        }

        public async override Task<PagedResultDto<PersonalCalendarDto>> GetListAsync(PersonalCalendarRequestDto input)
        {
            var result = (await base.Repository.WithDetailsAsync(p => p.SystemMessages, p => p.RefenceEvent))
                .WhereIf(CurrentUser != null, w => w.CreatorId == CurrentUser.Id)
                .WhereIf(input.Start.HasValue, p => p.StartTime >= input.Start)
                .WhereIf(input.End.HasValue, p => p.EndTime <= input.End)
                .ToList();

            return new PagedResultDto<PersonalCalendarDto>(result.Count(), await MapToGetListOutputDtosAsync(result));
        }


        public override async Task<PersonalCalendarDto> CreateAsync(PersonalCalendarDto input)
        {
            input.Id = GuidGenerator.Create();

            if (input.IsAllDay)
            {
                input.Start = new DateTime(input.Start.Year, input.Start.Month, input.Start.Day, 0, 0, 0, 0);
                input.End = new DateTime(input.End.Year, input.End.Month, input.End.Day, 23, 59, 59, 999);
            }

            var calander = await MapToEntityAsync(input);

            DateTime systemTime = DateTime.Now;

            if (calander.CreatorId.HasValue == false)
            {
                calander.CreationTime = systemTime;
                calander.CreatorId = CurrentUser.Id;
            }

            if (calander.SystemMessages != null)
            {

                //InternalSystemMessages msg = new InternalSystemMessages(GuidGenerator.Create());
                InternalSystemMessages msg = calander.SystemMessages;

                if (msg.CreatorId.HasValue == false)
                {
                    if (CurrentUser.Id.HasValue)
                    {
                        msg.FromUserId = CurrentUser.Id.Value;
                    }
                    else
                    {
                        msg.FromUserId = (await _identityUserRepository.FindByNormalizedUserNameAsync("ADMIN")).Id;
                    }
                    
                    msg.CreationTime = systemTime;
                    msg.CreatorId = CurrentUser.Id;
                }

                //msg.Subject = calander.SystemMessages.Subject;
                //msg.Message = calander.SystemMessages.Message;
                //msg.ActionCallbackURL = calander.SystemMessages.ActionCallbackURL;
                msg = await _systemMessageRepository.InsertAsync(msg);
                calander.SystemMessageId = msg.Id;
            }

            calander.SystemMessages = null;
            calander = await Repository.InsertAsync(calander);

            return await MapToGetOutputDtoAsync(calander);
        }


        public async override Task<PersonalCalendarDto> UpdateAsync(Guid id, PersonalCalendarDto input)
        {
            var oldData = (await Repository.WithDetailsAsync(p => p.SystemMessages, p => p.RefenceEvent))
                .SingleOrDefault(p => p.Id == id);

            if (oldData == null)
            {
                throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.DataNotFound],
                    code: HomeSystemDomainErrorCodes.DataNotFound,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }

            if (input.Title != oldData.SystemMessages.Subject)
            {
                oldData.SystemMessages.Subject = input.Title;
            }

            if (input.Description != oldData.SystemMessages.Message)
            {
                oldData.SystemMessages.Message = input.Description;
            }

            if (input.Start != oldData.StartTime)
            {
                oldData.StartTime = input.Start;
            }

            if (input.End != oldData.EndTime)
            {
                oldData.EndTime = input.End;
            }

            if (input.UIAction != oldData.SystemMessages.ActionCallbackURL)
            {
                oldData.SystemMessages.ActionCallbackURL = input.UIAction;
            }

            if (input.Color != oldData.Color)
            {
                oldData.Color = input.Color;
            }

            if (input.IsAllDay != oldData.IsAllDay)
            {
                oldData.IsAllDay = input.IsAllDay;
            }

            oldData = await Repository.UpdateAsync(oldData);

            return await MapToGetOutputDtoAsync(oldData);
        }

        public async Task<ListResultDto<PersonalCalendarDto>> GetRemindAsync(PersonalCalendarRequestDto input)
        {
            var scandata = (await MapToGetListOutputDtosAsync((await Repository.GetQueryableAsync()).ToList()))
                .Where(w => w.Start >= input.Start && w.Start <= input.End)
                .ToList();

            return new ListResultDto<PersonalCalendarDto>(scandata);

        }
    }
}
