$(function () {
    var cmskitl = abp.localization.getResource("CmsKit");
    var l = abp.localization.getResource("HomeSystem");

    var pagesService = ediux.homeSystem.mIMETypeManager.mIMETypeManager;

    var getFilter = function () {
        return {
            search: $('#MIMETypeManagerWrapper input.page-search-filter-text').val()
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
                            text: l('Edit'),
                            visible: abp.auth.isGranted('HomeSystem.MIMETypeManager.Modify'),
                            action: function (data) {
                                location.href = '/MIMETypeManager/Update/' + data.record.id;
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: abp.auth.isGranted('HomeSystem.MIMETypeManager.Delete'),
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
                title: l("Features:MIMETypesManager.DTFX.Columns.MIME"), data: "mime"
            },
            {
                title: l("Features:MIMETypesManager.DTFX.Columns.RefenceExtName"), data: "refenceExtName",
            },
            {
                title: l("Features:MIMETypesManager.DTFX.Columns.Description"), data: "description",
            },         
            {
                title: "CreatorId", data: "creatorId", visible: false
            },
            {
                title: "Creation Time", data: "creationTime", visible: false, dataFormat: "datetime"
            }
        ]
    }));

    $('#MIMETypeManagerWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        _dataTable.ajax.reload();
    });

    $('#AbpContentToolbar button[name=AddMIMEType]').on('click', function (e) {
        e.preventDefault();
        window.location.href = "/MIMETypeManager/Create"
    });    
});