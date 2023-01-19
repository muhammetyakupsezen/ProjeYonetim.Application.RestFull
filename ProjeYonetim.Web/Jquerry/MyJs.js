

  function  DoTest(){

    $.ajax({
        url: "/Home/GetTest",
        success: function (result, e, f) {
            if (f.status === 200) {
                $(alert(result));
                document.getElementById('LblMesaj').innerText = result;
            } else {
                alert("bir hata oluştu");
            }

        },
        method: "GET"

    }).done(function (result, e, f) {

        var f = result;

    }).fail(function (result, e, f) {
        var g = result;

    }).catch(function (result, e, f) {
        var h = result;

    }).progress(function (result, e, f) {
        var y = result;
    });


}