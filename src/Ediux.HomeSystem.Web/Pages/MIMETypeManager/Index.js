$(function () {
    var l = abp.localization.getResource('HomeSystem');
    var allow_create = abp.auth.isGranted('HomeSystem.MIMETypeManager.CreateNew');
    var allow_list = abp.auth.isGranted('HomeSystem.MIMETypeManager.List');
    var allow_modify = abp.auth.isGranted('HomeSystem.MIMETypeManager.Modify');
    var allow_delete = abp.auth.isGranted('HomeSystem.MIMETypeManager.Delete');
    var allow_special = abp.auth.isGranted('HomeSystem.MIMETypeManager.Special');

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

    const endpoint = "/api/mimetypemanager";
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
                title: l('Features:MIMETypesManager.DTFX.Columns.MIME'), data: "mime", type: "text"
            },
            {
                title: l('Features:MIMETypesManager.DTFX.Columns.RefenceExtName'), data: "refenceExtName", type: "text",
            },
            {
                title: l('Features:MIMETypesManager.DTFX.Columns.Description'), data: "description", type: "text",
            },
            {
                title: "CreatorId", data: "creatorId", type: "hidden", visible: false
            },
            {
                title: "Creation Time", data: "creationTime", type: "hidden", visible: false
            }
        ],
        onAddRow: function (table, rowdata, success, error) {
            abp.ajax({
                url: endpoint,
                contentType: "multipart/form-data",
                type: "POST",
                data: table.formData,
                processData: false,
                contentType: false
            }).then(result => {
                success(result);
            }).catch(err => { error(err); });
        },
        onDeleteRow: function (table, rowdata, success, error) {
            abp.ajax({ url: endpoint, type: 'DELETE', data: rowdata }).then(result => {
                success(result);
            }).catch(err => { error(err); });
        },
        onEditRow: function (table, rowdata, success, error) {
            abp.ajax({
                url: endpoint,
                contentType: "multipart/form-data",
                type: "PUT",
                data: table.formData,
                processData: false,
                contentType: false                
            }).then(result => {
                success(result);
            }).catch(err => { error(err); });
        }
    });
});