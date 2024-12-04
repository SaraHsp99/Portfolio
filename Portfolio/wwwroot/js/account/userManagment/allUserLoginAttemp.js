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

function getAllUserLogin() {
    datePersian = $("#datePersian").val();
    window.location.href = '/User/AllUserLoginAttemp?datePersian=' + datePersian;
}