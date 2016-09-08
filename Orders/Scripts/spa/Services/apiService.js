(function (app) {

    app.factory("apiService", apiService);

    function apiService($http, $location, notificationService, $rootScope) {
        var service = {
            get: get,
            post: post
        };

        function get(url, config, success, failure) {
            return $http.get(url, config)
                   .then(function (result) {
                       success(result);
                   }, function (error) {
                       if (error.status == '401') {
                           notificationService.displayError('Atsiprašome, bet šita veiksma gali atlikti adminas');
                           $rootScope.previousState = $location.path();
                           $location.path("/Login");
                       }
                       else if (failure != null) {
                           failure(error);
                       }
                   })
        }

        function post(url, data, success, failure) {
            return $http.post(url, data)
                        .then(function (result) {
                            success(result);
                        }, function (error) {
                            if (error.status == '401') {
                                notificationService.displayError('Atsiprašome, bet šita veiksma gali atlikti adminas');
                                $rootScope.previousState = $location.path();
                                $location.path("/Login");
                            }
                            else if (failure != null) {
                                failure(error);
                            }
                        })
        }

        return service;
    }
})(angular.module('common.core'));