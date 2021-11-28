$(function () {
    var l = abp.localization.getResource("HomeSystem");

    var $createForm = $('#form-page-create');
    var $buttonSubmit = $('#button-page-create');
    var $buttonCancel = $('#button-page-cancel');

    $buttonSubmit.click(function (e) {
        e.preventDefault();
        $createForm.submit();
    });
    $buttonCancel.click(function (e) {
        e.preventDefault();
        window.location.href = '../Index';
    });
});