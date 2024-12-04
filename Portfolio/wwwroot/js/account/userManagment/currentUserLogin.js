datePersian = document.querySelector('#datePersian');
if (datePersian) {
    datePersian.flatpickr({
        monthSelectorType: 'static',
        locale: 'fa',
        altInput: true,
        altFormat: 'Y/m/d',
        disableMobile: true,
        //maxDate: "today",
    });
}

function getCurrentUserLogin() {
    datePersian = $("#datePersian").val();
    window.location.href = '/User/UserLoginAttemp?datePersian=' + datePersian;
}
