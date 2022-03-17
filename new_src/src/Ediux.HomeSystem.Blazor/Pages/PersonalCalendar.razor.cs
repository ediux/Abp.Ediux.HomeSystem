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
        [Inject] public IPersonalCalendarAppService AppointmentService { get; set;}
        List<PersonalCalendarDto> _appointments = new();
       
        async  Task OnRequestNewData(DateTime start, DateTime end)
        {
            _appointments = (await AppointmentService.GetListAsync(new AbpSearchRequestDto())).Items.ToList();
           
        }

        Task OnAddingNewAppointment(DateTime start, DateTime end)
        {
            _appointments.Add(new PersonalCalendarDto { t_start = start, t_end = end, title = "A newly added appointment!", Color = "aqua" });
            return Task.CompletedTask;
        }

        Task HandleReschedule(PersonalCalendarDto appointment, DateTime newStart, DateTime newEnd)
        {
            appointment.t_start = newStart;
            appointment.t_end = newEnd;

            return Task.CompletedTask;
        }
    }
}
