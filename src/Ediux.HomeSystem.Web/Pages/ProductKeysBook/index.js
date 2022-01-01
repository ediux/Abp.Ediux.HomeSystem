$(function () {
    var cmskitl = abp.localization.getResource("CmsKit");
    var l = abp.localization.getResource("HomeSystem");

    var pagesService = ediux.homeSystem.productKeysBook.productKeysBook;

    var getFilter = function () {
        return {
            search: $('#ProductKeysWrapper input.page-search-filter-text').val()
        };
    };

    var _dataTable = $("#ProductKeysTable").DataTable(abp.libs.datatables.normalizeConfiguration({
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
                            visible: abp.auth.isGranted('HomeSystem.ProductKeysBook.Modify'),
                            action: function (data) {
                                location.href = '/ProductKeysBook/Update/' + data.record.id;
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: abp.auth.isGranted('HomeSystem.ProductKeysBook.Delete'),
                            confirmMessage: function (data) {
                                return l("Common:Messages.DeleteConfirm_Format", data.record.productname);
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
                title: l("Features:ProductKeysBook.DTFX.Columns.ProductName"), data: "productname"
            },
            {
                title: l("Features:ProductKeysBook.DTFX.Columns.ProductKey"), data: "productkey",
            },
            {
                title: l("Features:PasswordBook.DTFX.Columns.ExtraProperties"), data: "extraProperties",
                render: function (data, type) {
                    var keys = Object.keys(data);
                    var o = "";
                    if (keys.length > 0) {
                        keys.forEach(element => o = o + element + ":" + data[element] + "<br/>");
                    }
                    return o;
                }
            },
            {
                title: l("Features:ProductKeysBook.DTFX.Columns.Flag_Shared"),
                data: "shared",
                render: function (data, type) {
                    abp.log.info(type);
                    return `${data ? "公開" : "私人"}`;
                }
            },
            {
                title: "CreatorId", data: "creatorId", visible: false
            },
            {
                title: "Creation Time", data: "creationTime", visible: false, dataFormat: "datetime"
            }            
        ]
    }));

    $('#ProductKeysWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        _dataTable.ajax.reload();
    });

    $('#AbpContentToolbar button[name=AddProductKey]').on('click', function (e) {
        e.preventDefault();
        window.location.href = "/ProductKeysBook/Create"
    });
});