function getCitiesClinic() {

    var intVal = $("#Orgn_StateValue").val();
    $.ajax({
        type: "Get",
        url: "/user/GetCity?intVal=" + intVal,
        success: function (data) {
            $("#CityClinicDropdown").empty();
            $("#CityClinicDropdown").html(data);
        }
    });
}
function RegisterPhyClinicForm() {
    var data = $("#registerPhyClinicForm").serialize();
    $.ajax({
        type: "post",
        url: "/user/RegisterPhyClinic",
        data: data,
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        success: function (data) {
            var result = data;

            if (result.rsl) {
                showMessage("موفق", data.message, "success");
                window.location.href = '/user/login';
            }
            else {
                showMessage("خطا", result.message, "error");
            }
        }
    });
}


var timer = 60 * 2;
function EndTime() {
    $('#backToPhoneNumber').attr("disabled", false);
    $('#backToPhoneNumber').html("برگشت");
}
$(document).on("click", "#phoneNumberValudate", function () {
    $.ajax({
        type: 'POST',
        url: "/User/GetPhoneNumberRegister",
        // contentType: 'application/json',
        aync: false,
        data: { phoneNumber: $("#loginMobileNumber").val() },
        success: function (resualt) {
            if (resualt.rsl) {
                $("#divPhoneNumber").addClass("dspnone");
                $("#divCode").removeClass("dspnone");
                var display = document.querySelector('#backToPhoneNumber');

                if (resualt.message != null) {
                    $("#divCode #lblLoginErrorMsg").text(resualt.message);
                    $("#divCode .invalid-feedback").css("display", "block");
                }
                timer = Number(resualt.data);
                startTimer(timer, display, EndTime);
            } else {
                if (resualt.data == "UserIsExist") {
                    $("#modal1").children(":first").modal('hide');
                    showMessage("پیام سیستم", resualt.message);
                }
                $("#divPhoneNumber #lblLoginErrorMsg").text(resualt.message);
                $("#divPhoneNumber .invalid-feedback").css("display", "block");
            }
        },
        error: function (data) {

        }
    });
})

$(document).on("click", "#codeValidate", function () {
     
    $.ajax({
        type: 'POST',
        url: "/User/ValidateCodeRegister",
        // contentType: 'application/json',
        //aync: false,
        data: { code: $("#loginCodePhoneNumber").val(), phoneNumber: $("#loginMobileNumber").val() },
        success: function (resualt) {
            console.log(resualt);
            if (resualt.rsl) {
                $("#modal1").children(":first").modal('hide');
                if (!resualt.data) {
                    window.location.href = "/User/Register";
                    window.location.replace("/User/Register");
                }
                else {
                    showMessage("پیام سیستم", resualt.message);
                }
            }
            else {
                $("#divCode #lblLoginErrorMsg").text(resualt.message);
                $("#divCode .invalid-feedback").css("display", "block");
            }
        },
        error: function (data) {

        }
    });
})
$(document).on("click", "#backToPhoneNumber", function () {
    $("#divPhoneNumber").removeClass("dspnone");
    $("#divCode").addClass("dspnone");
    $('#backToPhoneNumber').attr("disabled", true);
    $("#divCode #lblLoginErrorMsg").text();
    $("#divCode .invalid-feedback").css("display", "none");
    $("#divPhoneNumber #lblLoginErrorMsg").text();
    $("#divPhoneNumber .invalid-feedback").css("display", "none");
});

