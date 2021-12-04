$(function () {

    var l = abp.localization.getResource("CmsKit");

    var $formUpdate = $('#form-page-update');
    var $buttonSubmit = $('#button-page-update');
    var $buttonCancel = $('#button-page-cancel');
    $formUpdate.data('validator').settings.ignore = ":hidden, [contenteditable='true']:not([name]), .tui-popup-wrapper";

    $buttonSubmit.click(function (e) {
        e.preventDefault();
        $formUpdate.submit();
    });

    $buttonCancel.click(function (e) {
        e.preventDefault();
        window.location.href = '/CmsKit/Admins/Pages/Index';
    });

    // -----------------------------------
    function getUppyHeaders() {
        var headers = {};
        headers[abp.security.antiForgery.tokenHeaderName] = abp.security.antiForgery.getToken();

        return headers;
    }

    var fileUploadUri = "/api/cms-kit-admin/media/page";
    var fileUriPrefix = "/api/cms-kit/media/";

    function uploadFile(blob, callback, source) {
        var UPPY_OPTIONS = {
            endpoint: fileUploadUri,
            formData: true,
            fieldName: "file",
            method: "post",
            headers: getUppyHeaders()
        };

        var UPPY = Uppy.Core().use(Uppy.XHRUpload, UPPY_OPTIONS);

        UPPY.reset();

        UPPY.addFile({
            id: "content-file",
            name: blob.name,
            type: blob.type,
            data: blob,
        });

        UPPY.upload().then((result) => {
            if (result.failed.length > 0) {
                abp.message.error("File upload failed");
            } else {
                var mediaDto = result.successful[0].response.body;
                var fileUrl = (fileUriPrefix + mediaDto.id);

                callback(fileUrl, mediaDto.name);
            }
        });
    }
});