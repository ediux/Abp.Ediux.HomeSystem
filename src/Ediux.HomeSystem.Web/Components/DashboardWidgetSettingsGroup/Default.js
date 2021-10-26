$("#SelectedDefaultWidgets").on('submit', function (event) {
    event.preventDefault();
    var l = abp.localization.getResource('HomeSystem');
    var form = $(this).serializeFormToObject();
    
    form.selectedDefaultWidgets = $('select#SelectedDefaultWidgets').val();
    ediux.homeSystem.settingManagement.webSiteSettings.updateDashboardGlobal(form).then(function (result) {
        toastr.options.positionClass = 'toast-top-right';
        abp.notify.success(l('Common:Messages.Success'), l('Features:Dashboard'));
    });

});