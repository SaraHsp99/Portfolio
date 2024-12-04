function editRoleModal(id) {
   /* $("#RoleDto_Id").val() = id*/

    $.ajax({
        type: "get",
        url: "/Role/EditRoleModal?id=" + id,
        success: function (data) {
            $('#editRole').empty();
            $('#editRole').html(data);
            $('#editRole').modal('toggle');
        }
    });
}

function insertPermission(rolePermissionId, roleId) {
     
    $.ajax({
        type: "post",
        url: "/role/InsertRolePermission?rolePermissionId=" + rolePermissionId + "&roleId=" + roleId,
        success: function (data) {    
            var result = data;
            GetRolePermissionByRoleId(roleId);
            if (result.rsl) {
                showMessage("موفق", result.message, "success");
            }
            else {
               
                showMessage("خطا", result.message, "error");
            }
        }
    });
}

function setTree(roleId) {
    $('#jstree').jstree({
        "core": {
            "themes": {
                "variant": "large"
            },
            "data": {
                "url": "/role/GetRolePermission?roleId= " + roleId + "",
                "dataType": "json"
            }
        },

    });


}
setTree(1);

function GetRolePermissionByRoleId(roleId) {
    $.ajax({
        type: "get",
        url: "/role/GetRolePermission?roleId= " + roleId,
        success: function (data) {
            $('#jstree').jstree(true).settings.core.data = data;
            $('#jstree').jstree(true).refresh();
        }
    });
}
function statusActiveRole(roleId) {
    $.ajax({
        type: "post",
        url: "/role/StatusActiveRole?roleId= " + roleId,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            $('#getAllRole').empty();
            $('#getAllRole').html(data);  
        }
    });
}



