(function (app) {

    app.factory("membershipService", membershipService);

    membershipService.$inject = ["apiService", "notificationService", "$http", "$base64", "$cookieStore", "$rootScope"];

    function membershipService(apiService, notificationService, $http, $base64, $cookieStore, $rootScope) {

        var service = {
            login: login,
            Register: Register,
            SaveCredentials: SaveCredentials,
            RemoveCredentials: RemoveCredentials,
            IsUserLoggedIn: IsUserLoggedIn
        }

        function login(user, completed, failed) {
            apiService.post("api/account/login", user, completed, failed);
        }
        function Register(user, completed, failed) {
            apiService.post("api/account/login", user, completed, failed);
        }

        function SaveCredentials(user) {
            var membershipData = $base64.encode(user.UserName + ":" + user.password + ":" + user.UserID);

            $rootScope.repository = {
                loggedUser: {
                    UserName: user.UserName,
                    //Paskui server sid'e isparsinama ir identifikuojama
                    authData: membershipData
                }
            };
            $http.defaults.headers.common['Authorization'] = "Basic" + membershipData;
            $cookieStore.put("repository", $rootScope.repository, { 'expires': (new Date().getDate() + 1) });
        }

        function RemoveCredentials() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
            $http.defaults.headers.common.Authorization = '';
        }

        function IsUserLoggedIn() {
            //Tikrinama are buvo uzsetintas
            return ($rootScope.repository != null && $rootScope.repository.loggedUser != null);
        }

        return service;
    }

})(angular.module('common.core'));