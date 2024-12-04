$(document).on("click", "#fpNumberValudate", function () {
    $.ajax({
        type: 'POST',
        url: "/User/GetPhoneNumberForgetPass",
        // contentType: 'application/json',
        aync: false,
        data: { phoneNumber: $("#fpMobileNumber").val() },
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
                $("#divPhoneNumber #lblLoginErrorMsg").text(resualt.message);
                $("#divPhoneNumber .invalid-feedback").css("display", "block");
            }
        },
        error: function (data) {

        }
    });
})

$(document).on("click", "#fpCodeValidate", function () {
    $.ajax({
        type: 'POST',
        url: "/User/ValidateCodeForgetPass",
        // contentType: 'application/json',
        aync: false,
        data: { code: $("#fpCodePhoneNumber").val(), phoneNumber: $("#fpMobileNumber").val() },
        success: function (resualt) {
            if (resualt.rsl) {
                $("#divCode").addClass("dspnone");
                $("#divPass").removeClass("dspnone");
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
$(document).on("click", "#resetPass", function () {
    $.ajax({
        type: 'POST',
        url: "/User/ResetPass",
        // contentType: 'application/json',
        aync: false,
        data: { pass: $("#fpPass").val(), repass: $("#repass").val() },
        success: function (resualt) {
            if (resualt.rsl) {
                $("#modal1").children(":first").modal('hide');
                showMessage("پیام سیستم", resualt.message);

            } else {
                $("#divPass #lblLoginErrorMsg").text(resualt.message);
                $("#divPass .invalid-feedback").css("display", "block");
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