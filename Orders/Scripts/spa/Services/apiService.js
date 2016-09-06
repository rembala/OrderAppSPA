﻿(function (app) {

    app.factory("apiService", apiService);

    function apiService($http, $location, $rootScope) {
        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http.get(url, config)
                   .then(function (result) {
                       success(result);
                   }, function (error) {
                       console.log(error);
                   })
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                        .then(function (result) {
                            success(result);
                        }, function (error) {
                            console.log(error);
                            //if (error.status == '401') {
                            //    notificationService.displayError("authentication required");
                            //    $rootScope.previousState = $location.path();
                            //    $location.path("/login");
                            //}
                            //else if (failure != null) {
                            //    failure(error);
                            //}
                        })
        }

        return service;
    }
})(angular.module('common.core'));