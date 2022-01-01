$(function () {
    var cmskitl = abp.localization.getResource("CmsKit");
    var l = abp.localization.getResource("HomeSystem");

    var pagesService = ediux.homeSystem.files.fileStore;

    var getFilter = function () {
        return {
            search: $('#MyFilesWrapper input.page-search-filter-text').val()
        };
    };

    var _dataTable = $("#dt-list").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: true,
        order: [[2, "desc"]],
        ajax: abp.libs.datatables.createAjax(pagesService.getList, getFilter),
        columnDefs: [
            {
                title: cmskitl("Details"),
                targets: 0,
                rowAction: {
                    items: [
                        {
                            text: l('Buttons:ReUpload'),
                            visible: abp.auth.isGranted('HomeSystem.Files.Modify'),
                            action: function (data) {
                                location.href = '/Files/Update/' + data.record.id;
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: abp.auth.isGranted('HomeSystem.Files.Delete'),
                            confirmMessage: function (data) {
                                return l("PageDeletionConfirmationMessage")
                            },
                            action: function (data) {
                                pagesService
                                    .delete(data.record.id)
                                    .then(function () {
                                        _dataTable.ajax.reload();
                                    });
                            }
                        }
                    ]
                }
            },
            {
                title: "Id", data: "id", type: "hidden", visible: false,
                orderable: true
            },
            {
                title: l("Features:Files.DTFX.Columns.Name"), data: "name"
            },
            {
                title: l("Features:Files.DTFX.Columns.ExtName"), data: "extName",
            },
            {
                title: l("Features:Files.DTFX.Columns.Description"), data: "description",
            },
            {
                title: l("Features:Files.DTFX.Columns.Size"), data: "size",
            },
            {
                title: l("Features:Files.DTFX.Columns.OriginFullPath"), data: "originFullPath",
            },
            {
                title: l("Features:Files.DTFX.Columns.Creator"), data: "creator"
            },
            {
                title: l("Features:Files.DTFX.Columns.CreatorDate"), data: "creatorDate",  dataFormat: "datetime"
            },
            {
                title: l("Features:Files.DTFX.Columns.Modifier"), data: "modifier"
            },
            {
                title: l("Features:Files.DTFX.Columns.ModifierDate"), data: "modifierDate", dataFormat: "datetime"
            }
        ]
    }));

    $('#MyFilesWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        _dataTable.ajax.reload();
    });

    $('#AbpContentToolbar button[name=UploadFile]').on('click', function (e) {
        e.preventDefault();
        window.location.href = "/Files/Create"
    });    
});