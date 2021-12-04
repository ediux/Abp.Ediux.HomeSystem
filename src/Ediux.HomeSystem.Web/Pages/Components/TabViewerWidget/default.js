(function ($) {

    $(function () {
        
        var l = abp.localization.getResource('HomeSystem');
        $("#RemovePageToWidgetForm").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();
            abp.log.debug(form);
            window.location.replace('?showwidget=collapseTabViewWidgetPanel');
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
                            window.location.replace('?showwidget=collapseTabViewWidgetPanel');
                        });
                });
        });
    });

})(jQuery);

var l = abp.localization.getResource('HomeSystem');
function getUrlParameter(name) {
    name = name.replace(/[\[]/, '\\[').replace(/[\]]/, '\\]');
    var regex = new RegExp('[\\?&]' + name + '=([^&#]*)');
    var results = regex.exec(location.search);
    return results === null ? '' : decodeURIComponent(results[1].replace(/\+/g, ' '));
}
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
                    //window.location.reload();                    
                    window.location.replace('?showwidget=collapseTabViewWidgetPanel');
                });
        });
}

function moveforwardpage(slug) {
    ediux.homeSystem.settingManagement.settingManagement.getGlobalOrNull('HomeSystem.TabViewGlobalSetting')
        .done(function (result) {
            var current = JSON.parse(result);            
            var currentPos = -1;
            var nextPos = -1;
            abp.log.debug(current);

            for (var i = 0; i < current.length; i++) {
                if (current[i].slug == slug) {
                    currentPos = i;
                    if (i == 0) {
                        nextPos = (current.length - 1);
                    } else {
                        nextPos = i - 1;
                    }
                    var swapTemp = current[currentPos];
                    current[currentPos] = current[nextPos];
                    current[nextPos] = swapTemp;
                    break;
                }                
            }

            for (var i = 0; i < current.length; i++) {
                current[i].order = i;
            }

            ediux.homeSystem.settingManagement.settingManagement.setGlobal('HomeSystem.TabViewGlobalSetting', JSON.stringify(current))
                .done(function (result2) {
                    abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
                    window.location.replace('?showwidget=collapseTabViewWidgetPanel');
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
                    var swapTemp = current[currentIndex];
                    current[currentIndex] = current[nextIndex];
                    current[nextIndex] = swapTemp;
                    break;
                }
            }

            for (var i = 0; i < current.length; i++) {
                current[i].order = i;
            }

            ediux.homeSystem.settingManagement.settingManagement.setGlobal('HomeSystem.TabViewGlobalSetting', JSON.stringify(current))
                .done(function (result2) {
                    abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
                    window.location.replace('?showwidget=collapseTabViewWidgetPanel');
                });
        });
}

var widgetshow = getUrlParameter('showwidget');
if (widgetshow) {
    $('#' + widgetshow).collapse('show');
    $('#' + widgetshow).on('hidden.bs.collapse', function () {
        window.location.replace('./');
    })
}
