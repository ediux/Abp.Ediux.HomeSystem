﻿$(function () {
    var l = abp.localization.getResource("CmsKit");

    var $createForm = $('#form-page-create');
    var $title = $('#ViewModel_Title');
    var $slug = $('#ViewModel_Slug');
    var $buttonSubmit = $('#button-page-create');
    var $buttonCancel = $('#button-page-cancel');

    $createForm.data('validator').settings.ignore = ":hidden, [contenteditable='true']:not([name]), .tui-popup-wrapper";

    $buttonSubmit.click(function (e) {
        e.preventDefault();
        $createForm.submit();
    });

    $buttonCancel.click(function (e) {
        e.preventDefault();
        window.location.href = '/CmsKit/Admins/Pages/Index';
    });

    var slugEdited = false;

    $title.on('change paste keyup', function () {
        var title = $title.val();

        if (slugEdited) {
            title = $slug.val();
        }

        var slugified = slugify(title, {
            lower: true
        });

        if (slugified != $slug.val()) {
            reflectedChange = true;
            $slug.val(slugified);
            reflectedChange = false;
        }
    });

    $slug.change(function () {
        slugEdited = true;
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