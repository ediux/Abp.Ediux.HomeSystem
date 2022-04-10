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

        protected override async Task CreateEntityAsync()
        {
            await base.CreateEntityAsync();
            NewEntity = new PersonalCalendarDto();
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task UpdateEntityAsync()
        {
            await base.UpdateEntityAsync();
            await InvokeAsync(StateHasChanged);
        }

        async Task HandleReschedule(PersonalCalendarDto appointment, DateTime newStart, DateTime newEnd)
        {
            appointment.Start = newStart;
            appointment.End = newEnd;
            await AppService.UpdateAsync(appointment.Id, appointment);
        }

        async Task OnAppointmentClicked(PersonalCalendarDto app)
        {
            EditingEntity = app;
            EditingEntityId = app.Id;
            await richTextEditRef.SetHtmlAsync(EditingEntity.Description);
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

        protected RichTextEdit richTextCreateRef;
        protected RichTextEdit richTextEditRef;
        protected bool readOnly;
        protected string contentAsHtml;
        protected string savedContent;

        public async Task OnContentChanged()
        {
            NewEntity.Description = await richTextCreateRef.GetHtmlAsync();
        }

        public async Task OnContentChanged_Edit()
        {
            EditingEntity.Description = await richTextEditRef.GetHtmlAsync();
        }

        public async Task OnSave()
        {
            //savedContent = await richTextEditRef.GetHtmlAsync();
            //EditingEntity.Description = savedContent;
            await richTextEditRef.ClearAsync();
        }
    }
}
