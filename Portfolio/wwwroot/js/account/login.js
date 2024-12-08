//function loginForm() {
//    var data = $("#loginForm").serialize();
//    $.ajax({
//        type: "post",
//        url: "/Account/Login",
//        data: data,
//        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
//        success: function (data) {
//            var result = data.result;
//            if (result.rsl) {
//                showMessage("موفق", result.message, "success");
//                window.location.href = data.url;
//            }
//            else {
//                showMessage("خطا", result.message, "error");
//            }
//        }
//    });
//}

function loginForm() {
    var data = $("#loginForm").serialize(); 
    $.ajax({
        type: "POST", 
        url: "/Account/Login",
        data: data, 
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (response) {
      
            if (response.success) {
          
                localStorage.setItem('token', response.token); 
              // sessionStorage.setItem('token', response.token); // ذخیره توکن در sessionStorage
                console.log("Token retrieved:", response.token);
                showMessage("موفق", response.message, "success"); 
                window.location.href = '/';
            } else {
   
                showMessage("خطا", response.message, "error");
            }
        },
        error: function (xhr, status, error) {
           
            showMessage("خطا", "مشکلی در برقراری ارتباط پیش آمده است", "error");
        }
    });
}


