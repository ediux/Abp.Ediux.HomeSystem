(function ($) {
    var currLCID = abp.localization.currentCulture;
    var loc = currLCID.cultureName;
    var l = abp.localization.getResource('HomeSystem');

    if (loc === "zh-Hant") {
        loc = 'zh';
    } else {
        if (loc.cultureName === "zh-Hans") {
            loc = 'zh-cn';
        }
    }

    $.extend({
        IMEfix: {
            flag: true,
            start: function (e) {
                this.flag = true;
            },
            end: function (e) {
                this.flag = false;
            },
            keyin: function (e) {
                if (this.flag == true) {
                    return;
                }
            }
        }
    });

    $.extend({
        CKEditorAutoSave: {
            flag: [],
            tempsave: [],
            interval: 600000,   
            formsubmit: function (formtarget,timerEvent,success,error) {
                var form_target_id = formtarget.prop('id');
                if (formtarget.valid()) {
                    formtarget.ajaxSubmit({
                        success: function (result) {
                            console.log(result);

                            if (typeof (result.id) !== 'undefined' && result.id != null) {
                                abp.notify.success(l('SuccessfullySaved'));

                                if ($.CKEditorAutoSave.tempsave[form_target_id] != true) {
                                    $.CKEditorAutoSave.tempsave.push(form_target_id);
                                    $.CKEditorAutoSave.tempsave[form_target_id] = true;
                                }
                            }

                            if (typeof (success) === 'function') {
                                success(result);
                            }
                        },
                        error: function (result) {
                            abp.notify.error(result.responseJSON.error.message);
                            if (typeof (error) === 'function') {
                                error(result);
                            }
                        }
                    });
                }
                if (typeof (timerEvent) !== 'undefined') {
                    if (timerEvent) {
                        setTimeout(() => this.formsubmit(formtarget, timerEvent), this.interval);
                    }                    
                }
            },
            saveData: function (formtarget) {                               
                return new Promise(resolve => {
                    this.formsubmit(formtarget);                    
                });
            }
        }
    });

    $.fn.CKEditor = function ($options) {
        var element = $(this);
        var dom = element.get(0);
        var pagetype = element.data('pagetype') || 'Default';
        var autosave_enabled = element.data('autosave') || 'N';
        var nosubmitbutton_enabled = element.data('no-submit-button') || 'N';
        
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
            },

        }, $options);

        if (autosave_enabled == 'Y') {
            //啟用auto-save功能

            var form_target_id = element.data('form-id') || '';
            var done_url = element.data('return-url') || '';

            if (form_target_id != '') {
                var $createForm = $('#' + form_target_id);
                if (typeof ($createForm) !== 'undefined') {
                    //攔截form的submit動作
                    $createForm.on('submit', function (e) {
                        e.preventDefault();

                        if ($.CKEditorAutoSave.tempsave[form_target_id] != true) {
                            abp.ui.setBusy();
                            $.CKEditorAutoSave.formsubmit($createForm, false, (r) => {
                                abp.ui.clearBusy();
                                if (done_url != '') {
                                    window.location.href = done_url;
                                }
                            }, (err) => {
                                abp.ui.clearBusy();
                                abp.notify.error(err.responseJSON.error.message);
                            });
                           
                        } else {
                            if (done_url != '') {
                                window.location.href = done_url;
                            }
                        }

                    });

                    $.CKEditorAutoSave.formsubmit($createForm, true);

                    if ($.CKEditorAutoSave.flag[form_target_id] != true) {
                        $.CKEditorAutoSave.flag.push(form_target_id);
                        $.CKEditorAutoSave.flag[form_target_id] = true;
                        console.log($.CKEditorAutoSave.flag);
                    }
                }
            }
        }

        if (nosubmitbutton_enabled == 'Y') {
            options = $.extend({
                autosave: {
                    waitingTime: 5000,
                    save(editor) {
                        return $.CKEditorAutoSave.saveData($createForm);
                    }
                }
            }, options);

            console.log(options);
        }

        ClassicEditor
            .create(element.get(0), options)
            .then(editor => {
                editor.model.document.on('change:data', () => {
                    element.val(editor.getData());
                });

                dom.addEventListener('compositionstart', $.IMEfix.start, false);
                dom.addEventListener('compositionend', $.IMEfix.end, false);
                dom.addEventListener('input', $.IMEfix.keyin, false);
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
    $editorContainer.CKEditor();
}