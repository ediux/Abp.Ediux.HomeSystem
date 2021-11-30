$(function () {
    var l = abp.localization.getResource("HomeSystem");

    var $createForm = $('#form-page-update');
    var $buttonSubmit = $('#button-page-update');
    var $buttonCancel = $('#button-page-cancel');
    $buttonSubmit.click(function (e) {
        e.preventDefault();
        $createForm.submit();
    });
    $buttonCancel.click(function (e) {
        e.preventDefault();
        window.location.href = '/PasswordBook/Index';
    });
});