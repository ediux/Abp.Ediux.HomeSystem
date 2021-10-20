document.addEventListener('DOMContentLoaded', function () {
    //$('#CalendarEvent_AllDay').change(function () {
    //    if (this.checked) {
           
    //    }
    //});

    var productInfoModal = new abp.ModalManager({
       /* viewUrl: '/PersonalCalendar'*/
    });

    //$('#btnCancel').click(function () {
    //    $('#collapseExample').collapse('hide');
    //    $('#calendar').show();
    //    calendar.render();
    //});

    var l = abp.localization.getResource('HomeSystem');
    var currLCID = abp.localization.currentCulture;
    var loc = currLCID.cultureName;

    if (loc === "zh-Hant") {
        loc= 'zh-TW';
    } else {
        if (loc.cultureName === "zh-Hans") {
            loc= 'zh-CN';
        }        
    }

    var Calendar = FullCalendar.Calendar;

    var calendarEl = document.getElementById('calendar');

    var calendar = new Calendar(calendarEl, {
        headerToolbar: {
            left: 'prev,next today addEventButton',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        editable: true,
        droppable: true, // this allows things to be dropped onto the calendar
        dayMaxEvents: true,
        locale: loc,
        drop: function (info) {

            // is the "remove after drop" checkbox checked?
            //if (checkbox.checked) {
            //    // if so, remove the element from the "Draggable Events" list
            //    info.draggedEl.parentNode.removeChild(info.draggedEl);
            //}
        },
        events: {
            url: '/api/calendars/events',
            method: 'GET',
            failure: function () {
                abp.message.error('取得行事曆清單發生錯誤!');
            }
        },
        eventClick: function (info) {
            info.jsEvent.preventDefault();

            abp.message.info('Event: ' + info.event.title + '<br/>'
                + 'Coordinates: ' + info.jsEvent.pageX + ',' + info.jsEvent.pageY+'<br/>'
                + 'View: ' + info.view.type);

            // change the border color just for fun
            info.el.style.borderColor = 'red';
        },
        customButtons: {
            addEventButton: {
                text: l('Buttons:Add_Event'),
                click: function () {
                    productInfoModal.open();
                    //$('#collapseExample').collapse('show');
                    //$('#calendar').hide();
                }
            }
        }
    });

    calendar.render();
});