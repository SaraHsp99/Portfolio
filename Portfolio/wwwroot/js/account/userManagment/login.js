function clinicLogin() {
    var dataForm = $("#partialLoginForm").serialize();
    $.ajax({
        type: "post",
        url: "/Account/login",
        data: dataForm,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            var result = data;
            if (data.url != undefined) {
               // window.location.href = data.url;
                if (data.url == "/Profile/GetProfileUser?tabName=userInfo") {
                    showMessage("هشدار", "لطفا اطلاعات پزشک را تکمیل نمایید", "warning");
                }
                else if (data.url == "/") {
                    window.location = data.url; 
                }
                
            }
            if (result.rsl != undefined) {
                showMessage("خطا", result.message, "error");
            }
            else {
                $('#clinicLogin').empty();
                $('#clinicLogin').html(data);
                $('#clinicLogin').modal('toggle');
            }
        }
    });
}


