﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ediux.HomeSystem.AdditionalSystemFunctions4Users
{
    public class PersonalCalendarAppService : CrudAppService<PersonalCalendar, PersonalCalendarDto, Guid, PersonalCalendarRequestDto>, IPersonalCalendarAppService
    {
        public PersonalCalendarAppService(IRepository<PersonalCalendar, Guid> repository) : base(repository)
        {
        }

        public async override Task<PagedResultDto<PersonalCalendarDto>> GetListAsync(PersonalCalendarRequestDto input)
        {
            var result = (await base.Repository.WithDetailsAsync(p => p.SystemMessages, p => p.RefenceEvent))
                .WhereIf(input.Start.HasValue, p => p.StartTime == input.Start)
                .WhereIf(input.End.HasValue, p => p.EndTime == input.End)
                .Where(w => w.CreatorId == CurrentUser.Id)
                .ToList();

            return new PagedResultDto<PersonalCalendarDto>(result.Count(), await MapToGetListOutputDtosAsync(result));
        }


        public override Task<PersonalCalendarDto> CreateAsync(PersonalCalendarDto input)
        {
            input.Id = GuidGenerator.Create();

            if (input.IsAllDay)
            {
                input.Start = new DateTime(input.Start.Year, input.Start.Month, input.Start.Day, 0, 0, 0, 0);
                input.End = new DateTime(input.End.Year, input.End.Month, input.End.Day, 23, 59, 59, 999);
            }

            return base.CreateAsync(input);
        }


        public async override Task<PersonalCalendarDto> UpdateAsync(Guid id, PersonalCalendarDto input)
        {
            var oldData = await GetAsync(id);

            if (oldData == null)
            {
                throw new UserFriendlyException(L[HomeSystemDomainErrorCodes.DataNotFound],
                    code: HomeSystemDomainErrorCodes.DataNotFound,
                    logLevel: Microsoft.Extensions.Logging.LogLevel.Error);
            }

            if (input.Title != oldData.Title)
            {
                oldData.Title = input.Title;
            }

            if (input.Description != oldData.Description)
            {
                oldData.Description = input.Description;
            }

            if (input.Start != oldData.Start)
            {
                oldData.Start = input.Start;
            }

            if (input.End != oldData.End)
            {
                oldData.End = input.End;
            }

            if (input.UIAction != oldData.UIAction)
            {
                oldData.UIAction = input.UIAction;
            }

            if (input.Color != oldData.Color)
            {
                oldData.Color = input.Color;
            }

            if (input.IsAllDay != oldData.IsAllDay)
            {
                oldData.IsAllDay = input.IsAllDay;
            }

            return await base.UpdateAsync(id, input);
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