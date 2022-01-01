$(function () {
    var cmskitl = abp.localization.getResource("CmsKit");
    var l = abp.localization.getResource("HomeSystem");

    var pagesService = ediux.homeSystem.applicationPluginsManager.applicationPluginsManager;

    var getFilter = function () {
        return {
            search: $('#PluginsManagerWrapper input.page-search-filter-text').val()
        };
    };

    var _dataTable = $("#PluginsManagerKeyTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollCollapse: true,
        scrollX: true,
        ordering: true,
        order: [[1, "desc"]],
        ajax: abp.libs.datatables.createAjax(pagesService.getList, getFilter),
        columnDefs: [
            {
                title: cmskitl("Details"),
                targets: 0,
                rowAction: {
                    items: [
                        {
                            text: l('Buttons:Update'),
                            visible: abp.auth.isGranted('HomeSystem.PluginsManager.Modify'),
                            action: function (data) {
                                location.href = '/PluginsManager/Update/' + data.record.id;
                            }
                        },
                        {
                            text: l('Buttons:UnInstall'),
                            visible: abp.auth.isGranted('HomeSystem.PluginsManager.Delete'),
                            confirmMessage: function (data) {
                                return l("Common:Messages.DeleteConfirm_Format", data.record.name);
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
                title: l("Features:PluginsManager.DTFX.Columns.Name"), data: "name"
            },
            {
                title: l("Features:PluginsManager.DTFX.Columns.AssemblyPath"), data: "pluginPath",
            },
            {
                title: l("Features:PluginsManager.DTFX.Columns.Disabled"), data: "disabled",
                render: function (data, type) {
                    return `${data ? "停用" : "啟用"}`;
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

    $('#PluginsManagerWrapper form.page-search-form').submit(function (e) {
        e.preventDefault();
        _dataTable.ajax.reload();
    });

    $('#AbpContentToolbar button[name=AddPlugin]').on('click', function (e) {
        e.preventDefault();
        window.location.href = "/PluginsManager/Create"
    });
});