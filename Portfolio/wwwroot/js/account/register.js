function registerForm() {
    var data = $("#registerForm").serialize();
    $.ajax({
        type: "post",
        url: "/Account/RegisterUser",
        data: data,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            var result = data;

            if (result.rsl) {
                showMessage("موفق", data.message, "success");
                window.location.href = '/account/login';
            }
            else {
                showMessage("خطا", result.message, "error");
            }
        }
    });
}
