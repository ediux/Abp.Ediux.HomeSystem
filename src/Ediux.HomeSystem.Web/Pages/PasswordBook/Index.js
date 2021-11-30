$(function () {
    var cmskitl = abp.localization.getResource("CmsKit");
    var l = abp.localization.getResource("HomeSystem");

    var pagesService = ediux.homeSystem.passworkBook.passworkBook;

    var getFilter = function () {
        return {
            search: $('#PasswordBookWrapper input.page-search-filter-text').val()
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
                            visible: abp.auth.isGranted('HomeSystem.PasswordBook.Modify'),
                            action: function (data) {
                                location.href = '/PasswordBook/Update/' + data.record.id;
                            }
                        },
                        {
                            text: l('Delete'),
                            visible: abp.auth.isGranted('HomeSystem.PasswordBook.Delete'),
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
                title: l("Features:PasswordBook.DTFX.Columns.SiteName"), data: "siteName"
            },
            {
                title: l("Features:PasswordBook.DTFX.Columns.SiteURL"), data: "siteURL",
            },
            {
                title: l("Features:PasswordBook.DTFX.Columns.LoginAccount"), data: "loginAccount",
            },
            {
                title: l("Features:PasswordBook.DTFX.Columns.Password"), data: "password",
                render: function (data, type) {
                    return `${abp.auth.isGranted("HomeSystem.PasswordBook.Special") ? data : "********"}`;
                }
            },
            {
                title: l("Features:PasswordBook.DTFX.Columns.ExtraProperties"), data: "extraProperties",
                render: function (data, type) {
                    var keys = Object.keys(data);
                    var o = "";
                    if (keys.length > 0) {
                        keys.forEach(element => o = o + element + ":" + data[element]+"<br/>");
                    }
                    return o;
                }
            },
            {
                title: l("Features:PasswordBook.DTFX.Columns.IsHistory"), data: "isHistory", visible: abp.auth.isGranted('HomeSystem.PasswordBook.Special'),
                render: function (data, type) {
                    return `${data ? "是" : "否"}`;
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

    $('#PasswordBookWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        _dataTable.ajax.reload();
    });

    $('#AbpContentToolbar button[name=AddPasswordBook]').on('click', function (e) {
        e.preventDefault();
        window.location.href = "/PasswordBook/Create"
    });
});