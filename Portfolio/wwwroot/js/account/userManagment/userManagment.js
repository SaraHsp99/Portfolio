function editUserModal(id) {
    $.ajax({
        type: "get",
        url: "/user/EditUserModal?id=" + id,
        success: function (data) {
            $('#offcanvasScroll').empty();
            $('#offcanvasScroll').html(data);


        }
    });
}
$(document).ready(function () {
    $(document).on("click", "#btn-insertFormEditUser", function () {
        if (!CheckForm("insertFormeditUser")) {
            return;
        }
        else {
            var data1 = $("#insertFormeditUser").serialize();
            /*        var formValid = $("#insertFormeditUser").validate();*/
            $.ajax({
                type: "post",
                url: "/user/EditUser",
                data: data1,
                success: function (data) {

                    // $("#insertFormeditUser").validate();
                    //if (!$("#insertFormeditUser").valid()) {
                    //    $('#offcanvasScroll').empty();
                    //    $('#offcanvasScroll').html(data);
                    //}
                    /*                else {*/
                    //$('#getAllUser').empty();
                    //$('#getAllUser').html(data);
                    window.location.href = '/user/GetUserManagment';
                    /*                }*/
                }
            });
        }
    })
})
function statusActiveUser(id) {
    $.ajax({
        type: "post",
        url: "/user/StatusActiveUser?id=" + id,
        success: function (data) {
            $('#getAllUser').empty();
            $('#getAllUser').html(data);

        }
    });
}
function insertPermission(userPermissionId, userId) {
    $.ajax({
        type: "post",
        url: "/user/InsertUserPermission?userPermissionId=" + userPermissionId + "&userId=" + userId,
        success: function (data) {
            $('#jstree').jstree(true).settings.core.data = data;
            $('#jstree').jstree(true).refresh();

            $(".flashing-Light").show();

        }
    });
}
function setTree(userId) {
    $('#jstree').jstree({
        "core": {
            "themes": {
                "variant": "large"
            },
            "data": {
                "url": "/user/GetUserPermissionTree",
                "dataType": "json"
            }
        },

    });


}
setTree(1);
function GetUserPermissionByUserId(userId) {
    $.ajax({
        type: "get",
        url: "/user/GetUserPermission?userId= " + userId,
        success: function (data) {
            $("#userRole").empty();
            $('#jstree').jstree(true).settings.core.data = data.userPermissionDtos;
            $('#jstree').jstree(true).refresh();
            getUserRole(userId, data);
        }
    });
}
$(document).ready(function () {
    $(document).on('click', '.role-info', function () {
        var dataId = $(this).attr("data-id");
        var userId = $(this).attr("data-userid")
        var userRoleId = $(this).attr("data-userroleid")
        var input = {
            roleDto: {
                userId: userId,
                id: dataId,
                userRoleId: userRoleId
            }
        }
        $.ajax({
            type: "post",
            url: "/user/InsertUserRole",
            data: input,
            success: function (data) {
                $("#userRole").empty();
                getUserRole(userId, data);
                showMessage("موفق", data.userRole.message, "success");
                $('#jstree').jstree(true).refresh();
            }
        });
    });
})
function getUserRole(userId, data) {
    $("#userRole").empty();
    for (var i = 0; i < data.roleDtos.length; i++) {
        if (data.roleDtos[i].isGranted == true) {
            $("#userRole").append(`
                                    <li class="item" style="height:32px;">

                                        <div class="product-info">

                                            <div class="product-description" style="font-size:12px">                                            

                                                        <span style="font-size:medium" class="col-md-1 role-info" id="role`+ data.roleDtos[i].id + `" 
                     data-id=` + data.roleDtos[i].id +
                ` data-userId=` + userId + ` data-userRoleId=` + data.roleDtos[i].userRoleId + `>
                                                            <input  type="checkbox" checked/>`
                + data.roleDtos[i].description +
                `</span>                                             
                                            </div>
                                        </div>
                                    </li>`);
        }
        else {
            $("#userRole").append(`
                                    <li class="item" style="height:32px;">

                                        <div class="product-info">

                                            <div class="product-description" style="font-size:12px">                                            

                                                        <span style="font-size:medium" class="col-md-1 role-info" id="role`+ data.roleDtos[i].id + `" 
                     data-id=` + data.roleDtos[i].id +
                ` data-userId=` + userId + ` data-userRoleId=` + data.roleDtos[i].userRoleId + `>
                                                            <input  type="checkbox"/>`
                + data.roleDtos[i].description +
                `</span>                                             
                                            </div>
                                        </div>
                                    </li>`);

        }

    }

}
function setPermissionAllUser() {
    $.ajax({
        type: "post",
        url: "/user/SetPermissionAllUser",
        success: function (data) {
            if (data.rsl) {
                showMessage("موفق", data.message, "success");
            }
        }
    });
}
function ReStart() {
    $.ajax({
        type: "get",
        url: "/user/ReStart",
        success: function (data) {
            $(".flashing-Light").hide();
            $('#messageModel').empty();
            $('#messageModel').html(data);

        }
    });

}
