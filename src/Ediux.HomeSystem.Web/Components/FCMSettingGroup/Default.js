﻿$("#FCMSettingsForm").on('submit', function (event) {
    event.preventDefault();
    var l = abp.localization.getResource('HomeSystem');
    var form = $(this).serializeFormToObject();
    
    ediux.homeSystem.settingManagement.settingManagement.updateFCMSettings(form).then(function (result) {
        toastr.options.positionClass = 'toast-top-right';
        abp.notify.success(l('Common:Messages.Success'), l('Settings:FCMSettingsGroupComponents'));
    });

});