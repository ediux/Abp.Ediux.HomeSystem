(function ($) {
    var currLCID = abp.localization.currentCulture;
    var loc = currLCID.cultureName;

    if (loc === "zh-Hant") {
        loc = 'zh';
    } else {
        if (loc.cultureName === "zh-Hans") {
            loc = 'zh-cn';
        }
    }

    $.fn.CKEditor = function ($options) {
        var l = abp.localization.getResource('HomeSystem');
        var pagetype = $options.pageType;
        if (typeof (pagetype) === 'undefined') {
            pagetype = 'Default';
        }
        var options = $.extend({
            toolbar: {
                items: [
                    'sourceEditing',
                    '|',
                    'heading',
                    'findAndReplace',
                    'undo',
                    'redo',
                    '|',
                    'bold',
                    'italic',
                    'underline',
                    'strikethrough',
                    'link',
                    'subscript',
                    'horizontalLine',
                    '|',
                    'highlight',
                    'fontBackgroundColor',
                    'fontColor',
                    'fontFamily',
                    'fontSize',
                    '|',
                    'bulletedList',
                    'numberedList',
                    'todoList',
                    '|',
                    'outdent',
                    'indent',
                    'alignment',
                    '|',
                    'imageUpload',
                    'imageInsert',
                    'blockQuote',
                    'insertTable',
                    'mediaEmbed',
                    '|',
                    'code',
                    'codeBlock',
                    'htmlEmbed'
                ]
            },
            language: loc,
            image: {
                toolbar: [
                    'imageTextAlternative',
                    'imageStyle:inline',
                    'imageStyle:block',
                    'imageStyle:side'
                ]
            },
            table: {
                contentToolbar: [
                    'tableColumn',
                    'tableRow',
                    'mergeTableCells'
                ]
            },
            licenseKey: '',
            htmlSupport: {
                allow: [
                    {
                        name: /.*/,
                        attributes: true,
                        classes: true,
                        styles: true,
                    }
                ]
            },
            simpleUpload: {
                // The URL that the images are uploaded to.
                uploadUrl: '/api/simpleupload/upload/' + pagetype,
            }
        }, $options);

        var id = '#' + $(this).prop('id');
        ClassicEditor
            .create(document.querySelector(id), options)
            .then(editor => {
                editor.model.document.on('change:data', () => {
                    $(id).val(editor.getData());
                });
            })
            .catch(error => {
                abp.notify.error(error);
            });
    };
})(jQuery);

initAllEditors();

function initAllEditors() {
    $('textarea').each(function (i, item) {
        initEditor(item);
    });
}

function initEditor(element) {
    var $editorContainer = $(element);
    var pagetype = $editorContainer.data('pagetype') || 'Default';

    if (($editorContainer.data('autosave') || 'N') == 'Y') {
        $editorContainer.CKEditor({
            pageType: pagetype,
            autosave: {
                waitingTime: 5000,
                save(editor) {
                    var id_ctr = $('#ViewModel_id');
                    if (typeof (id_ctr) !== 'undefined') {
                        return saveData({
                            entityType: pagetype,
                            id: id_ctr.val(),
                            slug: $('#ViewModel_Slug').val(),
                            title: $('#ViewModel_Title').val(),
                            data: editor.getData()
                        });
                    } else {
                        return saveData({
                            entityType: pagetype,
                            id: '',
                            slug: $('#ViewModel_Slug').val(),
                            title: $('#ViewModel_Title').val(),
                            data: editor.getData()
                        });
                    }
                }
            }
        });
    } else {
        $editorContainer.CKEditor({ pageType: pagetype });
    }



    var flag = true;
    element.addEventListener('compositionstart', function (e) {
        flag = true;
    }, false);
    element.addEventListener('compositionend', function (e) {
        flag = false;
    }, false);

    element.addEventListener('input', function (e) {
        if (flag) return;
    }, false);
}

function saveData(data) {
    return new Promise(resolve => {
        setTimeout(() => {
            console.log('Saved', data);
            ediux.homeSystem.miscellaneous.miscellaneous.autoSave(data);
            resolve();
        }, 9000);
    });
}