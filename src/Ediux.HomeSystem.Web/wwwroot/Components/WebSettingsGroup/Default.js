(function ($) {

    $(function () {

        var l = abp.localization.getResource('HomeSystem');

        $("#WebSiteSettingsForm").on('submit', function (event) {
            event.preventDefault();
            var form = $(this).serializeFormToObject();

            ediux.homeSystem.settingManagement.webSiteSettings.update(form).then(function (result) {
                toastr.options.positionClass = 'toast-top-right';
                abp.notify.success(l('Common:Messages.Success'), l('Settings:WebSettingsGroupComponents'));
            });

        });       
    });

})(jQuery);