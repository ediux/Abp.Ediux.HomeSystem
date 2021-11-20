(function ($) {

    $(function () {

        var l = abp.localization.getResource('HomeSystem');

        $("#WebSiteSettingsForm").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();

            ediux.homeSystem.settingManagement.settingManagement.updateWebSetting(form).then(function (result) {
                abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
            });

        });

       
    });

})(jQuery);