function loginForm() {
    var data = $("#registerForm").serialize();
    $.ajax({
        type: "post",
        url: "/Account/Login",
        data: data,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            var result = data;

            if (result.rsl) {
                showMessage("success", data.message, "success");
                window.location.href = '/account/home';
            }
            else {
                showMessage("error", result.message, "error");
            }
        }
    });
}
