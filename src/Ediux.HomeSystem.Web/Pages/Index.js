$(function () {
    var myWidgetManager = new abp.WidgetManager('#MyDashboardWidgetsArea');
    myWidgetManager.init();

    ediux.homeSystem.settingManagement.webSiteSettings.getDashboardWidgetLists()
        .then(function (result) {
            result.forEach(function (e, i) {
                var widget_loadFunc = eval('typeof ' + e + '_onLoad');

                if (widget_loadFunc === 'function') {
                    eval(e + '_onLoad()');
                }
            });
        });
});
