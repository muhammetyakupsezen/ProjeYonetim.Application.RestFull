var common = angular.module("CommonModule", []);

common.factory('CommonFactory', function ($http, $q) {


    return {
        TestModu: function () {
            var deferred = $q.defer();
            return deferred.promise;
        },
    }


});