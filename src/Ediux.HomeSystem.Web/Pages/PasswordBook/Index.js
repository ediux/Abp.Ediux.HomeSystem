$(function () {
    var l = abp.localization.getResource('HomeSystem');
    var allow_create = abp.auth.isGranted('HomeSystem.PasswordBook.CreateNew');
    var allow_list = abp.auth.isGranted('HomeSystem.PasswordBook.List');
    var allow_modify = abp.auth.isGranted('HomeSystem.PasswordBook.Modify');
    var allow_delete = abp.auth.isGranted('HomeSystem.PasswordBook.Delete');
    var allow_special = abp.auth.isGranted('HomeSystem.PasswordBook.Special');

    var btns = [];

    if (allow_special) {
        btns.push({
            extend: 'selected',
            text: '<i class="fa fa-trash mr-1"></i> ' + l('Buttons:Delete'),
            name: 'delete',
            className: 'btn-danger btn-sm mr-1'
        });
        btns.push({
            extend: 'selected',
            text: '<i class="fa fa-edit mr-1"></i> ' + l('Buttons:Edit'),
            name: 'edit',
            className: 'btn-warning btn-sm mr-1'
        });
        btns.push({
            text: '<i class="fa fa-plus mr-1"></i> ' + l('Buttons:Add'),
            name: 'add',
            className: 'btn-info btn-sm mr-1'
        });
    } else {
        if (allow_delete) {
            btns.push({
                extend: 'selected',
                text: '<i class="fa fa-trash mr-1"></i> ' + l('Buttons:Delete'),
                name: 'delete',
                className: 'btn-danger btn-sm mr-1'
            });
        }
        if (allow_modify) {
            btns.push({
                extend: 'selected',
                text: '<i class="fa fa-edit mr-1"></i> ' + l('Buttons:Edit'),
                name: 'edit',
                className: 'btn-warning btn-sm mr-1'
            });
        }
        if (allow_create) {
            btns.push({
                text: '<i class="fa fa-plus mr-1"></i> ' + l('Buttons:Add'),
                name: 'add',
                className: 'btn-info btn-sm mr-1'
            });
        }

        if (btns.length == 0) {
            btns = null;

        }
    }

    abp.log.debug(btns);

    const endpoint = "/api/passwordbook";
    $('#dt-list').DataTableEdit({
        ajax: {
            url: endpoint + '/list',
            contentType: "application/json",
            type: "POST",
            data: function (d) {
                return JSON.stringify(d);
            }
        },
        buttons: btns,
        columns: [
            {
                title: "Id", data: "id", type: "hidden", visible: false
            },
            {
                title: l('Features:PasswordBook.DTFX.Columns.SiteName'), data: "siteName", type: "text"
            },
            {
                title: l('Features:PasswordBook.DTFX.Columns.SiteURL'), data: "siteURL", type: "text",
            },
            {
                title: l('Features:PasswordBook.DTFX.Columns.LoginAccount'), data: "loginAccount", type: "text",
            },
            {
                title: l('Features:PasswordBook.DTFX.Columns.Password'), data: "password", type: "text",
            },
            {
                title: l('Features:PasswordBook.DTFX.Columns.IsHistory'), data: "isHistory", type: "hidden", visible: false,
            },
            {
                title: "CreatorId", data: "creatorId", type: "hidden", visible: false
            },
            {
                title: "Creation Time", data: "creationTime", type: "hidden", visible: false
            }
        ],
        onAddRow: function (table, rowdata, success, error) {
            $.ajax({
                url: endpoint,
                contentType: "multipart/form-data",
                type: "POST",
                data: table.formData,
                processData: false,
                contentType: false,
                success: success,
                error: error
            });
        },
        onDeleteRow: function (table, rowdata, success, error) {
            $.ajax({ url: endpoint, type: 'DELETE', data: rowdata, success: success, error: error });
        },
        onEditRow: function (table, rowdata, success, error) {
            $.ajax({
                url: endpoint,
                contentType: "multipart/form-data",
                type: "PUT",
                data: table.formData,
                processData: false,
                contentType: false,
                success: success,
                error: error
            });
        }
    });
});