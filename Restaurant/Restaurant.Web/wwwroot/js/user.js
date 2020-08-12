var dataTable;

$(document).ready(function () {
    loadList();
});
function loadList() {
    dataTable = $('#Dt_Load').dataTable({
        "ajax": {
            "url": "/api/applicationUser",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "fullName", "width": "25%" },
            { "data": "email", "width": "25%" },
            { "data": "phoneNumber", "width": "25%" },
            {
                "data": { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date();
                    var lockout = new Date(data.lockoutEnd);
                    if (lockout > today) {
                        //currently user is locked
                        return `<div class="text-center">
                                <a class="btn btn-danger text-white" style="cussor:pointer; width:100px;" onclick=LockUnLock('${
                            data.id}')>
                                    <i class="fas fa-lock-open"></i> Unlock
                                </a><\div>`;
                    } else {
                        return `<div class="text-center">
                                <a class="btn btn-success text-white" style="cussor:pointer; width:100px;" onclick=LockUnLock('${
                            data.id}')>
                                    <i class="fas fa-lock"></i> Lock
                                </a><\div>`;
                    }
                }, "width": "25%"
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "width": "100%"
    });
}

function LockUnLock(id) {

    $.ajax({
        type: 'Post',
        url: '/api/ApplicationUser',
        data: JSON.stringify(id),
        contentType: "application/Json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.api().ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}