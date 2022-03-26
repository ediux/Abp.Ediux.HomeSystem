using Blazorise.RichTextEdit;

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

        async Task OnAddingNewAppointment(DateTime start, DateTime end)
        {
            NewEntity.Start = start;
            NewEntity.End = end;

            await CreateModal.Show();
        }

        protected override async Task CloseCreateModalAsync()
        {
            NewEntity.Description = contentAsHtml;
            await base.CloseCreateModalAsync();
           
        }

        Task HandleReschedule(PersonalCalendarDto appointment, DateTime newStart, DateTime newEnd)
        {
            appointment.Start = newStart;
            appointment.End = newEnd;

            return Task.CompletedTask;
        }

        async Task OnAppointmentClicked(PersonalCalendarDto app)
        {
            EditingEntity = app;
            EditingEntityId = app.Id;
            await EditModal.Show();
            await InvokeAsync(StateHasChanged);
        }

        async Task OnOverflowAppointmentClick(DateTime day)
        {
            //var dialog = DialogService.Show<OverflowAppointmentDialog>($"Appointments for {day.ToShortDateString()}", new DialogParameters
            //{
            //    ["Appointments"] = _appointments,
            //    ["SelectedDate"] = day,
            //});
            //await dialog.Result;

            await InvokeAsync(StateHasChanged);
        }

        protected RichTextEdit richTextEditRef;
        protected bool readOnly;
        protected string contentAsHtml;
        protected string savedContent;

        public async Task OnContentChanged()
        {
            contentAsHtml = await richTextEditRef.GetHtmlAsync();
        }

        public async Task OnSave()
        {
            //savedContent = await richTextEditRef.GetHtmlAsync();
            //EditingEntity.Description = savedContent;
            await richTextEditRef.ClearAsync();
        }
    }
}
