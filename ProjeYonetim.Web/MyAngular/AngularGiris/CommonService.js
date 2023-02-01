// var ServiceModule = angular.module('MyServiceModule', []);

common.service('CommonService', function ($http, $sce, $compile, $q) {

    this.CapitalizeCase = function (obj) {
        var deger = obj;
        var yeniDeger = '';
        deger = deger.split(' ');
        for (var i = 0; i < deger.length; i++) {
            yeniDeger += deger[i].substring(0, 1).toUpperCase() + deger[i].substring(1, deger[i].length).toLowerCase() + ' ';
            obj = yeniDeger;
        }
        return obj;
    };

    this.WriteCookie = function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toGMTString();
        }
        else expires = "";
        document.cookie = name + "=" + value + expires + "; path=/";
    };

    this.ReadCookie = function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    };

    this.DeleteCookie = function (name) {
        this.WriteCookie(name, "", -1);
    };

    this.RandString = function (x) {
        var s = "";
        while (s.length < x && x > 0) {
            var r = Math.random();
            s += (r < 0.1 ? Math.floor(r * 100) : String.fromCharCode(Math.floor(r * 26) + (r > 0.5 ? 97 : 65)));
        }
        return s;
    };

    this.ShowPage = function (iPageName, id) {

        var pageName = "";
        if (iPageName !== "" || iPageName === null) {
            pageName = iPageName;
        }
        pageName += "?dt=" + (new Date()).getTime();

        if (id !== null) {
            pageName += "&id=" + id;
        }
        return pageName;
    };

    this.GetBrowserLang = function () {

        return navigator.language || navigator.userLanguage;

    };

    this.GetBrowserName = function () {
        if ((navigator.userAgent.indexOf("Opera") || navigator.userAgent.indexOf('OPR')) !== -1) {
            return 'Opera';
        }
        else if (navigator.userAgent.indexOf("Chrome") !== -1) {
            return 'Chrome';
        }
        else if (navigator.userAgent.indexOf("Safari") !== -1) {
            return 'Safari';
        }
        else if (navigator.userAgent.indexOf("Firefox") !== -1) {
            return 'Firefox';
        }
        else if ((navigator.userAgent.indexOf("MSIE") !== -1) || (!!document.documentMode === true)) //IF IE > 10
        {
            return 'IE';
        }
        else {
            return 'unknown';
        }
    };

    this.GetParameterByName = function (name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results === null)
            return "";
        else
            return decodeURIComponent(results[1].replace(/\+/g, " "));
    };

    this.GetSiteUrl = function () {
        return document.location.origin;
    };

    this.GetWebApiUrl = function () {
        return WebApiUrl;
    };

    this.GetSiteFullUrl = function () {
        return document.location.href;
    };

    this.LoadSettings = function () {

        Lang = this.GetBrowserLang();
        BrowserName = this.GetBrowserName();
        SiteName = this.GetSiteUrl();


    };

    this.ShowSimpleMessage = function (Message) {
        swal(Message);
    };

    this.ShowSimpleMessageByTitle = function (Title, Message) {
        swal(Title, Message);
    };

    this.ShowErrorMessage = function (Title, Message) {

        Swal.fire(
            Title,
            Message,
            'error'
        )

    };

    this.ShowSuccessMessage = function (Title, Message) {

        swal(Title, Message, "success");

    };

    this.ShowMessageByButton = function (Title, Message, Success, ButtonText) {

        swal({
            title: Title,
            text: Message,
            icon: Success ? "success" : "error",
            button: ButtonText,
        });

    };

    this.ShowSuccessRedirect = function (Title, Message, Image, RedirectUrl) {

        swal({
            html: '<br><b>' + Title + '</b><br><br>' + Message + '<p>',
            confirmButtonClass: "btn-member",
            confirmButtonText: 'Tamam',
            imageUrl: Image
        }).then((value) => {
            window.location.href = "/" + RedirectUrl;
        });

    };

    this.ShowErrorRedirect = function (Title, Message, RedirectUrl) {

        swal({
            html: '<br><b>' + Title + '</b><br><br>' + Message + '<p>',
            confirmButtonClass: "btn-member",
            confirmButtonText: 'Tamam'
        }).then((value) => {
            window.location.href = "/" + RedirectUrl;
        });

    };

    this.ShowConfirm = function () {

    };

    this.TestAlert = function () {

    };

    this.setRadioValue = function (RadioName) {
        var radios = document.getElementsByName(RadioName);
        for (var i = 0, length = radios.length; i < length; i++) {

            radios[i].checked;
            break;
        }
        return "";

    };

    this.getRadioValue = function (RadioName) {
        var radios = document.getElementsByName(RadioName);
        for (var i = 0, length = radios.length; i < length; i++) {
            if (radios[i].checked) {
                return radios[i].value;
            }
        }
        return "";

    };

    this.getSelectedText = function (elementId) {
        try {

            var elt = document.getElementById(elementId);
            if (elt.selectedIndex === -1)
                return null;

            return elt.options[elt.selectedIndex].text;

        } catch (e) {
            return "";
        }


    };

    this.getXmlValue = function (XmlString, TagName) {

        var parser, xmlDoc, result = "";
        if (window.DOMParser) {
            parser = new DOMParser();
            xmlDoc = parser.parseFromString(XmlString, "text/xml");
        } else {
            xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
            xmlDoc.async = false;
            xmlDoc.loadXML(XmlString);
        }

        try {

            if (xmlDoc.getElementsByTagName(TagName)[0].childNodes[0].nodeValue !== undefined && xmlDoc.getElementsByTagName(TagName)[0].childNodes[0].nodeValue !== null) {
                result = xmlDoc.getElementsByTagName(TagName)[0].childNodes[0].nodeValue;
            }

        } catch (e) {
            result = "";
        }

        return result;

    };

    this.daysBetween = function (startDate, endDate) {
        var millisecondsPerDay = 24 * 60 * 60 * 1000;
        return (this.treatAsUTC(endDate) - this.treatAsUTC(startDate)) / millisecondsPerDay;
    };

    this.treatAsUTC = function treatAsUTC(date) {
        var result = new Date(date);
        result.setMinutes(result.getMinutes() - result.getTimezoneOffset());
        return result;
    };

    this.ShowNotification = function (NotificationType, NotificationPosition, Title, Message, Icon, Image, Size) {
        //NotificationType = default info warning error success
        //Positions = center top, top left, top rigth, bottom left vb
        Lobibox.notify(NotificationType, {
            pauseDelayOnHover: true,
            continueDelayOnInactiveTab: false,
            position: NotificationPosition,
            msg: Message,
            title: Title,
            icon: Icon,
            img: Image,
            size: Size,
            sound: false
        });
    }

    this.ShowNotificationWithAnimate = function (NotificationType, NotificationPosition, Title, Message, Icon, Image, Width, ShowClass, HideClass) {
        //NotificationType = default info warning error success
        //Positions = center top, top left, top rigth, bottom left vb
        //showclass =fadeInDown,bounceIn,zoomIn,lightSpeedIn,rollIn
        //hideclass = fadeOutDown,bounceOut,zoomOut,lightSpeedOut,rollOut
        Lobibox.notify(NotificationType, {
            pauseDelayOnHover: true,
            continueDelayOnInactiveTab: false,
            position: NotificationPosition,
            msg: Message,
            title: Title,
            icon: Icon,
            img: Image,
            showClass: ShowClass,
            hideClass: HideClass,
            width: Width,
        });
    }

    this.DoGetServerDateTime = function () {

        var deferred = $q.defer();
        var url = SiteUrl;
        url += '/Api/GetServerDatetime';

        $http.get(url, {


        }).then(function (returnData) {

            deferred.resolve(returnData);
            return deferred.promise;

        }).catch(function (e) {

            console.log(e.status + " " + e.statusText);
            deferred.resolve(e.status);
            deferred.reject(e);
            return deferred.promise;

        }).finally(function (e) {

        });
        return deferred.promise;

    };

});