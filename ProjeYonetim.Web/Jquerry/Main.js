//function Login() {
//    var Username = document.getElementById('TxtUsername').value;
//    var Password = document.getElementById('TxtPassword').value;

//    if (Username == '' || Password == '') {
//        ShowAlert('info', 'Hata', 'Kullanıcı adı veya şifre boş olamaz');
//    } else {
//        $.ajax({


//            url: "/Api/Login",
//            success: function (result, e, f) {
//                if (f.status === 200) {
//                    if (result != "") {
//                        window.location.href = "/Panel/IndexPanel";
//                    }

//                } else {
//                    alert("bir hata oluştu");
//                }

//            },
//            data: { "Username": Username, "Password": Password },
//            method: "POST"

//        }).done(function (result, e, f) {

//            var f = result;

//        }).fail(function (result, e, f) {
//            if (result.status === 500) {
//                Swal.fire({
//                    icon: 'error',
//                    title: 'Oops... oO',
//                    text: 'Something went wrong!',
//                })
//            }


//        }).catch(function (result, e, f) {
//            var h = result;

//        }).progress(function (result, e, f) {
//            var y = result;
//        });
//    }



//}



//function ShowAlert(picon, ptitle, ptext) {


//    Swal.fire({
//        icon: picon,
//        title: ptitle,
//        text: ptext,

//    });
//}




function Login() {
    var Username = document.getElementById('TxtUsername').value;
    var Password = document.getElementById('TxtPassword').value;
    if (Username === '' || Password === '') {
        ShowAlert('info', 'Hata', 'Her iki alanı doldurun');
    }
    else {
        $.ajax({
            url: "/Api/Login",
            success: function (result, e, f) {
                if (f.status === 200) {
                    if (result != "") {
                        window.location.href = "/Panel/IndexPanel";
                    }
                }
                else {
                    ShowAlert('error', 'Hata', 'Hatalı bilgi girişi');
                }
            },
            data: { "Username": Username, "Password": Password },
            method: "POST"
        }).done(function (result, e, f) {
            var f = result;
        }).fail(function (result, e, f) {
            if (result.status === 500) {
                ShowAlert('error', 'Hata', 'Hatalı bilgi girişi');
            }
        }).catch(function (result, e, f) {
            var h = result;
        }).progress(function (result, e, f) {
            var y = result;
        });

    }

}

//success - error - warning - info - question
function ShowAlert(picon, ptitle, ptext) {

    Swal.fire({
        icon: picon,
        title: ptitle,
        text: ptext
    });

}


function GetLoginUserDetail() {

    $.ajax({
        url: "/Api/GetLoginUserDetail",
        success: function (result, e, f) {
            if (f.status === 200) {
                if (result != "") {
                    var LblNameSurname = document.getElementById('LblNameSurname');
                    LblNameSurname.innerText = result.Ad + " " + result.Soyad;
                    var LblTitle = document.getElementById('LblTitle');
                    LblTitle.innerText = result.Unvan;
                    var LblWelcome = document.getElementById('LblWelcome');
                    LblWelcome.innerText = "Merhaba" + result.Ad + " " + result.Soyad;
                }
                else {
                    ShowAlert('error', 'Hata', 'Hatalı bilgi girişi');
                }
            }
        },
        //  data: { "Username": Username, "Password": Password },
        method: "GET"
    }).done(function (result, e, f) {
        var f = result;
    }).fail(function (result, e, f) {
        if (result.status === 500) {
            ShowAlert('error', 'Hata', 'Hatalı bilgi girişi');
        }
    }).catch(function (result, e, f) {
        var h = result;
    }).progress(function (result, e, f) {
        var y = result;
    });



}
