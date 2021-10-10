
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
                    text: '<i class="fa fa-trash mr-1"></i> ' + abp.localization.localize('Buttons:Delete') ,
                    name: 'delete',
                    className: 'btn-danger btn-sm mr-1'
                },
                {
                    extend: 'selected',
                    text: '<i class="fa fa-edit mr-1"></i> ' + abp.localization.localize('Buttons:Edit'),
                    name: 'edit',
                    className: 'btn-warning btn-sm mr-1'
                },
                {
                    text: '<i class="fa fa-plus mr-1"></i> ' + abp.localization.localize('Buttons:Add'),
                    name: 'add',
                    className: 'btn-info btn-sm mr-1'
                },
                {
                    text: '<i class="fa fa-sync mr-1"></i> ' + abp.localization.localize('Buttons:Synchronize'),
                    name: 'refresh',
                    className: 'btn-primary btn-sm'
                }
            ],
            language: {
                processing: abp.localization.localize('Common:DTFX.Processing'),
                search: abp.localization.localize('Common:DTFX.Search'),
                lengthMenu: abp.localization.localize('Common:DTFX.LengthMenu'),
                info: abp.localization.localize('Common:DTFX.Info'),
                infoEmpty: abp.localization.localize('Common:DTFX.InfoEmpty'),
                infoFiltered: abp.localization.localize('Common:DTFX.InfoFiltered'),
                infoPostFix: abp.localization.localize('Common:DTFX.InfoPostFix'),
                loadingRecords: abp.localization.localize('Common:DTFX.LoadingRecords'),
                zeroRecords: abp.localization.localize('Common:DTFX.ZeroRecords'),
                emptyTable: abp.localization.localize('Common:DTFX.EmptyTable'),
                paginate: {
                    first: abp.localization.localize('Common:DTFX.Paginate.First'),
                    previous: abp.localization.localize('Common:DTFX.Paginate.Previous'),
                    next: abp.localization.localize('Common:DTFX.Paginate.Next'),
                    last: abp.localization.localize('Common:DTFX.Paginate.Last')
                },
                aria: {
                    sortAscending: abp.localization.localize('Common:DTFX.Aria.SortAscending'),
                    sortDescending: abp.localization.localize('Common:DTFX.Aria.SortDescending')
                }
            }
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
