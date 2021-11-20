(function ($) {

    $(function () {

        var l = abp.localization.getResource('HomeSystem');
        $("#RemovePageToWidgetForm").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();
            abp.log.debug(form);

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
                            abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
                            window.location.reload();
                            $('#collapseTabViewWidgetPanel').collapse('show');
                        });
                });
        });
    });

})(jQuery);

var l = abp.localization.getResource('HomeSystem');

function deletepageslug(slug) {
    ediux.homeSystem.settingManagement.settingManagement.getGlobalOrNull('HomeSystem.TabViewGlobalSetting')
        .done(function (result) {
            var current = JSON.parse(result);
            var removeindex = -1;
            abp.log.debug(current);

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
                    abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
                    window.location.reload();
                    $('#collapseTabViewWidgetPanel').collapse('show');
                });
        });
}

function moveforwardpage(slug) {
    ediux.homeSystem.settingManagement.settingManagement.getGlobalOrNull('HomeSystem.TabViewGlobalSetting')
        .done(function (result) {
            var current = JSON.parse(result);
            var currentIndex = -1;
            var nextIndex = -1;
            abp.log.debug(current);

            for (var i = 0; i < current.length; i++) {
                if (current[i].slug == slug) {
                    currentIndex = i;
                    if (i == 0) {
                        nextIndex = current.length - 1;
                    } else {
                        nextIndex = i - 1;
                    }
                    var swapOrder = current[nextIndex].order;
                    current[nextIndex].order = current[currentIndex].order;
                    current[currentIndex].order = swapOrder;
                }
            }

            ediux.homeSystem.settingManagement.settingManagement.setGlobal('HomeSystem.TabViewGlobalSetting', JSON.stringify(current))
                .done(function (result2) {
                    abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
                });
        });
}

function movebackpage(slug) {
    ediux.homeSystem.settingManagement.settingManagement.getGlobalOrNull('HomeSystem.TabViewGlobalSetting')
        .done(function (result) {
            var current = JSON.parse(result);
            var currentIndex = -1;
            var nextIndex = -1;
            abp.log.debug(current);

            for (var i = 0; i < current.length; i++) {
                if (current[i].slug == slug) {
                    currentIndex = i;
                    if (i == (current.length - 1)) {
                        nextIndex = 0;
                    } else {
                        nextIndex = i + 1;
                    }
                    var swapOrder = current[nextIndex].order;
                    current[nextIndex].order = current[currentIndex].order;
                    current[currentIndex].order = swapOrder;
                }
            }

            ediux.homeSystem.settingManagement.settingManagement.setGlobal('HomeSystem.TabViewGlobalSetting', JSON.stringify(current))
                .done(function (result2) {
                    abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));                    
                });
        });
}