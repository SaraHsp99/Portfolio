﻿function showMessage(title, text, theme) {

    window.createNotification({
        closeOnClick: true,
        displayCloseButton: true,
        positionClass: "nfc-bottom-right",
        showDuration: 4000,
        theme: theme !== '' ? theme : 'success'
    })({
        title: title !== '' ? title : 'اعلان',
        message: decodeURI(text)
    })
}
