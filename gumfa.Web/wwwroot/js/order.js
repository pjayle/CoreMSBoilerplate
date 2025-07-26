var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("approved")) {
        loadDataTable("approved");
    }
    else {
        if (url.includes("readyforpickup")) {
            loadDataTable("readyforpickup");
        }
        else {
            if (url.includes("cancelled")) {
                loadDataTable("cancelled");
            }
            else {
                loadDataTable("created");
            }
        }
    }
});

function loadDataTable(status) {
    debugger;
    dataTable = $('#tblData').DataTable({
        order: [[0, 'desc']],
        "ajax": { url: "/order/getall?status=" + status },
        "columns": [
            //{ data: 'OrderID', "width": "5%" },
            { data: 'orderName', "width": "5%" },
            { data: 'status', "width": "25%" },
            {
                data: 'orderID',
                "render": function (data) {

                    //return `<div class="w-75 btn-group" role="group">
                    //<a href="/order/Edit/${data}" class="btn btn-primary mx-2"><i class="bi bi-pencil-square"></i></a>
                    //</div>`

                    return `<a class="btn btn-success" href="/Order/Edit?orderId=${data}"><i class="bi bi-pencil-square"></i></a>
                            <a class="btn btn-danger"  href="/Order/Delete?orderId=${data}"><i class="bi bi-trash"></i></a>`
                },
                "width": "10%"
            }
        ],
    })
}