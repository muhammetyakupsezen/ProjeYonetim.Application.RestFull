var AAplication = angular.module("OrnekApplication", ["CommonModule");


AAplication.controller("OrnekController", function ($scope, $http, $window, CommonFactory, CommonService) {
    //$scope.Welcome = 'Yakup';
    //$scope.TxtName = '';
    //$scope.TxtMetin = '';
    //$scope.BuyukMetin = '';


    //$scope.SayHello = function () {
    //    alert($scope.TxtName);
    //};

    //$scope.BuyukHarf = function () {
    //    $scope.BuyukMetin = MyService.ToUpper($scope.TxtMetin);
    //};


    $scope.ServerDateTime = '';
    $scope.Loading = false;

    $scope.LoadSettings = function (Version) {
        CommonService.LoadSettings();
        SiteUrl = $scope.SiteUrl;
        Lang = CommonService.GetBrowserLang();
        $scope.SiteUrl = CommonService.GetSiteUrl();
        SiteUrl = $scope.SiteUrl;
        $scope.Version = Version;
        alert($scope.SiteUrl);
    };

    $scope.GetServerDateTime = function () {
        $scope.Loading = true;
        CommonService.DoGetServerDateTime().then(function (Response) {
            if (Response.status === 200) {
                $scope.ServerDateTime = Response.data;
            }
        }).catch(function (e) {

        }).finally(function (e) {
            $scope.Loading = false;
        });

    };







});