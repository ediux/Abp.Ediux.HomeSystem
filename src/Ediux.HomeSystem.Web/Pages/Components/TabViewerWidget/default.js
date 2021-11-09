(function ($) {

    $(function () {

        var l = abp.localization.getResource('HomeSystem');
        $("#RemovePageToWidgetForm").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();
            console.log(form);
           
        });

        $("#AddPageToWidgetForm").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();

            ediux.homeSystem.settingManagement.settingManagement.getGlobalOrNull('HomeSystem.TabViewGlobalSetting')
                .done(function (result) {
                    var current = JSON.parse(result);
                    delete form.__RequestVerificationToken;
                    form.order = Number(form.order);
                    current.push(form);
                    ediux.homeSystem.settingManagement.settingManagement.setGlobal('HomeSystem.TabViewGlobalSetting', JSON.stringify(current))
                        .done(function (result2) {
                            toastr.options.positionClass = 'toast-top-right';
                            abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
                            window.location.reload();
                        });
                });
        });
    });

})(jQuery);

function deletepageslug(slug) {
    var l = abp.localization.getResource('HomeSystem');
    ediux.homeSystem.settingManagement.settingManagement.getGlobalOrNull('HomeSystem.TabViewGlobalSetting')
        .done(function (result) {
            var current = JSON.parse(result);
            var removeindex = -1;
            console.log(current);

            for (var i = 0; i < current.length; i++) {
                if (current[i].slug == slug) {
                    removeindex = i;
                }
            }

            if (removeindex != -1) {
                current.splice(removeindex, 1);
            }

            ediux.homeSystem.settingManagement.settingManagement.setGlobal('HomeSystem.TabViewGlobalSetting', JSON.stringify(current))
                .done(function (result2) {
                    toastr.options.positionClass = 'toast-top-right';
                    abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
                    window.location.reload();
                });
        });
}