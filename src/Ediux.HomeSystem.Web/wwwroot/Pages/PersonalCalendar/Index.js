document.addEventListener('DOMContentLoaded', function () {
    var l = abp.localization.getResource('HomeSystem');

    var currLCID = abp.localization.currentCulture;
    var loc = currLCID.cultureName;

    if (loc === "zh-Hant") {
        loc = 'zh-TW';
    } else {
        if (loc.cultureName === "zh-Hans") {
            loc = 'zh-CN';
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
        events: {
            url: '/api/calendars/events',
            method: 'GET',
            failure: function () {
                abp.message.error('取得行事曆清單發生錯誤!');
            }
        },
        eventClick: function (info) {
            info.jsEvent.preventDefault();

            var editEventModal = new abp.ModalManager({
                viewUrl: '/PersonalCalendar/EditEvent'
            });

            editEventModal.open({ id: info.event.id });

            editEventModal.onOpen(function () {

                ClassicEditor
                    .create(document.querySelector('#CalendarEvent_description'))
                    .then(editor => {
                        editor.model.document.on('change:data', () => {
                            $('#CalendarEvent_description').val(editor.getData());
                            //  console.log('The data has changed!');
                        });
                    })
                    .catch(error => {
                        abp.notify.error(error, l('Features:PersonalCalendar.Title.EditEvent'));
                        //console.error(error);
                    });

                if (info.event.allDay == true) {
                    $('#CalendarEvent_EndTime').parent().hide();
                } else {
                    $('#CalendarEvent_EndTime').parent().show();
                }

                $('#CalendarEvent_AllDay').change(function () {
                    if (this.checked) {
                        $('#CalendarEvent_EndTime').parent().hide();
                    }
                    else {
                        $('#CalendarEvent_EndTime').parent().show();
                    }
                });

                $('#btnDelete').click(function () {
                    ediux.homeSystem.personalCalendar.personalCalendar.delete(info.event.id)
                        .then(function () {
                            calendar.refetchEvents();
                            editEventModal.close();
                            abp.notify.success(l('Common:Messages.Success'), l('Features:PersonalCalendar.Title.DeleteEvent'));
                        });
                });
            });

            editEventModal.onResult(function () {
                toastr.options.positionClass = 'toast-top-right';
                abp.notify.success(l('Common:Messages.Success'), l('Features:PersonalCalendar.Title.EditEvent'));
                calendar.refetchEvents();
                //Calendar.fullCalendar('rerenderEvents');
            });
            // change the border color just for fun
            info.el.style.borderColor = 'red';
        },
        customButtons: {
            addEventButton: {
                text: l('Buttons:Add_Event'),
                click: function () {
                    productInfoModal.open();
                }
            }
        }
    });
    var productInfoModal = new abp.ModalManager({
        viewUrl: '/PersonalCalendar/CreateEvent'
    });

    productInfoModal.onResult(function () {
        toastr.options.positionClass = 'toast-top-right';
        abp.notify.success(l('Common:Messages.Success'), l('Features:PersonalCalendar.Title.CreateEvent'));
        calendar.render();
        //Calendar.fullCalendar('rerenderEvents');
    });


    productInfoModal.onOpen(function () {

        ClassicEditor
            .create(document.querySelector('#CalendarEvent_description'))
            .then(editor => {
                editor.model.document.on('change:data', () => {
                    $('#CalendarEvent_description').val(editor.getData());
                    console.log('The data has changed!');
                });
            })
            .catch(error => {
                abp.notify.error(error);
                console.error(error);
            });

        $('#CalendarEvent_AllDay').change(function () {
            //abp.message.info('test!');
            if (this.checked) {
                $('#CalendarEvent_EndTime').parent().hide();
            }
            else {
                $('#CalendarEvent_EndTime').parent().show();
            }
        });
    });
    calendar.render();
});