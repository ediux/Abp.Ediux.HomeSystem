import Editor from '@toast-ui/editor';

(function ($) {

    /* Trigger app shortcut menu on CTRL+Q press */
    $(document).keydown(function (event) {
        // CTRL + Q
        if (event.ctrlKey && event.which === 81)
            $("a[title*=Apps]").trigger("click");
    });

    /* Initialize basic datatable */
    $.fn.DataTableEdit = function ($options) {
        var options = $.extend({            
            dom: "<'row mb-3'<'col-sm-12 col-md-6 d-flex align-items-center justify-content-start'f><'col-sm-12 col-md-6 d-flex align-items-center justify-content-end'B>>" +
                "<'row'<'col-sm-12'tr>>" +
                "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
            responsive: true,
            serverSide: true,
            altEditor: true,
            pageLength: 10,
            select: { style: "single" },
            buttons: [
                {
                    extend: 'selected',
                    text: '<i class="fa fa-trash mr-1"></i> Delete',
                    name: 'delete',
                    className: 'btn-danger btn-sm mr-1'
                },
                {
                    extend: 'selected',
                    text: '<i class="fa fa-edit mr-1"></i> Edit',
                    name: 'edit',
                    className: 'btn-warning btn-sm mr-1'
                },
                {
                    text: '<i class="fa fa-plus mr-1"></i> Add',
                    name: 'add',
                    className: 'btn-info btn-sm mr-1'
                },
                {
                    text: '<i class="fa fa-sync mr-1"></i> Synchronize',
                    name: 'refresh',
                    className: 'btn-primary btn-sm'
                }
            ]
        }, $options);

        return $(this).DataTable(options).on('init.dt', function () {
            $("span[data-role=filter]").off().on("click", function () {
                const search = $(this).data("filter");
                if (table)
                    table.search(search).draw();
            });
        });
    };
}(jQuery));

var toastui = {};
toastui.Editor = Editor;

//ContentEditor




function initAllEditors() {
    $('.content-editor').each(function (i, item) {
        initEditor(item);
    });
}

function initEditor(element) {
    var $editorContainer = $(element);
    var inputName = $editorContainer.data('input-id');
    var $editorInput = $('#' + inputName);
    var initialValue = $editorInput.val();

    var editor = new Editor({
        el: $editorContainer[0],
        usageStatistics: false,
        useCommandShortcut: true,
        initialValue: initialValue,
        previewStyle: 'tab',
        height: "100%",
        minHeight: "25em",
        initialEditType: 'markdown',
        language: $editorContainer.data("language"),
        hooks: {
            addImageBlobHook: uploadFile,
        },
        events: {
            change: function (_val) {
                $editorInput.val(editor.getMarkdown());
                $editorInput.trigger("change");
            }
        }
    });
}

initAllEditors();