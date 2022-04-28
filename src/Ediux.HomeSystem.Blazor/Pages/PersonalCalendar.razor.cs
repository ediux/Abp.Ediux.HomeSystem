using BlazorContextMenu;

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

        protected BlazorScheduler.Scheduler scheduler;

        List<PersonalCalendarDto> _appointments = new();
        List<PersonalCalendarDto> _allAppointments;

        protected async Task OnContextMenuItemClick(ItemClickEventArgs e)
        {
            await DeleteEntityAsync((PersonalCalendarDto)e.Data);
            _allAppointments = (await CalendarService.GetListAsync(new PersonalCalendarRequestDto())).Items.ToList();
            await InvokeAsync(StateHasChanged);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (_allAppointments == null)
                {
                    _allAppointments = (await CalendarService.GetListAsync(new PersonalCalendarRequestDto())).Items.ToList();
                }
            }

            if (_allAppointments != null)
            {
                _appointments = _allAppointments.Where(x => x.Start >= scheduler.CurrentRange.Start && x.End <= scheduler.CurrentRange.End).ToList();
            }
            else
            {
                _appointments = new();
            }
        }



        protected async Task OnRequestNewData(DateTime start, DateTime end)
        {
            if (_allAppointments == null)
            {
                _allAppointments = (await CalendarService.GetListAsync(new PersonalCalendarRequestDto())).Items.ToList();
            }

            if (_allAppointments != null)
            {
                _appointments = _allAppointments.Where(x => x.Start >= start && x.End <= end).ToList();
            }
            else
            {
                _appointments = new();
            }

        }

        async Task OnAddingNewAppointment(DateTime start, DateTime end)
        {
            NewEntity = new PersonalCalendarDto() { Title = "New Event", Description = "New Event", Start = start, End = end, Color = "#CDDC39E6" };
            await AppService.CreateAsync(NewEntity);
            _allAppointments = (await CalendarService.GetListAsync(new PersonalCalendarRequestDto())).Items.ToList();

            await InvokeAsync(StateHasChanged);
        }

        protected override async Task CreateEntityAsync()
        {
            await AppService.CreateAsync(NewEntity);
            _allAppointments = (await CalendarService.GetListAsync(new PersonalCalendarRequestDto())).Items.ToList();
            await InvokeAsync(StateHasChanged);
            await CloseCreateModalAsync();
        }

        protected override async Task UpdateEntityAsync()
        {
            await AppService.UpdateAsync(EditingEntityId, EditingEntity);
            _allAppointments = (await CalendarService.GetListAsync(new PersonalCalendarRequestDto())).Items.ToList();
            await InvokeAsync(StateHasChanged);
            await CloseEditModalAsync();
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
