/// <reference path="../../wwwroot/libs/jquery-form/jquery.form.min.js" />

$(function () {
    var l = abp.localization.getResource("HomeSystem");

    var $createForm = $('#form-page-update');
    var $buttonSubmit = $('#button-page-update');
    var $buttonCancel = $('#button-page-cancel');
    $buttonSubmit.click(function (e) {
        e.preventDefault();
        abp.ui.setBusy();
        var form = $createForm[0];
        var formData = new FormData(form);
        abp.ajax({
            type: 'POST',
            url: $createForm.prop('action'),
            data: formData,
            contentType: false,
            cache: false,
            processData: false
        }).then(function (result) {
            abp.message.info("更新成功!系統正在重新啟動，5秒後將跳轉到首頁...");

            abp.ajax({
                url: '/api/reboot',
                contentType: "application/json",
                type: "GET"
            })
                .then(function (data) {
                    setTimeout(function () {
                        abp.ui.clearBusy();
                        window.location.href = '/';
                    }, 5000);
                })
                .catch(function () {
                    abp.ui.clearBusy();
                    abp.message.error("重新啟動失敗!");
                    window.location.href = '/';
                });
        }).catch(function () {
            abp.ui.clearBusy();
            abp.message.error("更新失敗!!");
        });


    });
    $buttonCancel.click(function (e) {
        e.preventDefault();
        window.location.href = '/PluginsManager/Index';
    });
});