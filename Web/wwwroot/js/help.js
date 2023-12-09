﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url": "/Product/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "description", "width": "15%" },
            {
                "data": "createdDate",
                "render": function (data) {
                    if (data == null) return "";
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return ('0' + date.getDate()).slice(-2) + "/" + ('0' + month).slice(-2) + "/" + date.getFullYear();
                },
                "width": "15%"
            },
            {
                "data": "active",
                render: function (data) {
                    return data === true ?
                        'Active' :
                        'Non Active';
                },
                "width": "15%"
            },
            { "data": "price", "width": "15%" },
            { "data": "category.name", "with": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="w-75 btn-group" role = "group">
                        <a href = "/Product/Upsert?id=${data}"
                    class="btn btn-primary me-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                        <button type="button" onclick=Delete('/Product/Delete/'+${data}) class="btn btn-danger me-2"> <i class="bi bi-trash-fill"></i> Delete</button>
                    </div > 
                    `
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}