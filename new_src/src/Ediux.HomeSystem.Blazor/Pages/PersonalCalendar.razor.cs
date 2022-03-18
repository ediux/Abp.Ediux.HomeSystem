using Ediux.HomeSystem.AdditionalSystemFunctions4Users;

using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ediux.HomeSystem.Blazor.Pages
{
    public partial class PersonalCalendar
    {
        [Inject] public IPersonalCalendarAppService CalendarService { get; set; }
        List<PersonalCalendarDto> _appointments = new();

        async Task OnRequestNewData(DateTime start, DateTime end)
        {
            _appointments = (await CalendarService.GetListAsync(new PersonalCalendarRequestDto() { Start = start, End = end })).Items.ToList();
        }

        Task OnAddingNewAppointment(DateTime start, DateTime end)
        {
            _appointments.Add(new PersonalCalendarDto { Start = start, End = end, Title = "A newly added appointment!", Color = "aqua" });
            return Task.CompletedTask;
        }

        Task HandleReschedule(PersonalCalendarDto appointment, DateTime newStart, DateTime newEnd)
        {
            appointment.Start = newStart;
            appointment.End = newEnd;

            return Task.CompletedTask;
        }
    }
}
