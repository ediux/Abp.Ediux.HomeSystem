$(function () {
    var myWidgetManager = new abp.WidgetManager('#MyDashboardWidgetsArea');
    myWidgetManager.init();

    ediux.homeSystem.dashBoard.dashBoardManagement.getDashboardWidgetLists()
        .then(function (result) {
            result.forEach(function (e, i) {
                var widget_loadFunc = eval('typeof ' + e + '_onLoad');

                if (widget_loadFunc === 'function') {
                    eval(e + '_onLoad()');
                }
            });
        });

    nextYearCountDown();
});

function nextYearCountDown() {
    var systemTime = new Date();
    var currentYear = systemTime.getFullYear();
    var nextYear = new Date(currentYear + 1, 1, 1, 0, 0, 0);
    /*  let elapsed = nextYear - systemTime;*/
    var diff = nextYear.getTime() - systemTime.getTime() - (1000 * 60 * 60 * 24 * 30);
    var days = Math.floor(diff / 1000 / 60 / 60 / 24);
    diff -= (days * 1000 * 60 * 60 * 24);
    var hours = Math.floor(diff / 1000 / 60 / 60);
    diff -= hours * 1000 * 60 * 60;
    var minutes = Math.floor(diff / 1000 / 60);
    diff -= minutes * 1000 * 60;
    var seconds = Math.floor(diff / 1000);
    resultTime = days + "天" + hours + "時" + minutes + "分" + seconds + "秒";

    $('#nextyear_countdown').html("<p class=\"fs-2 text-center\">" + currentYear + ":" + resultTime + "</p>");
    setTimeout(nextYearCountDown, 1000);
}