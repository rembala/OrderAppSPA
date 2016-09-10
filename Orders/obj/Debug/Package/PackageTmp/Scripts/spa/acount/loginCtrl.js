(function (app) {

    app.controller('LoginrCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$scope', 'membershipService', '$location', 'apiService', 'notificationService', '$rootScope'];

    function LoginCtrl($scope, membershipService, $location, apiService, notificationService, $rootScope) {
        $scope.login = login;
        $scope.user = {};

        function login() {
            apiService.post("api/authentication/Login", $scope.user, successfullLogin, FailureLogin)
        }

        function successfullLogin(result) {
            if (result.data.success) {
                membershipService.SaveCredentials($scope.user);
                notificationService.displaySuccess('Sveiki, ' + $scope.user.UserName);
                $scope.userData.displayUserInformation();
                if ($rootScope.previousState) {
                    $location.path($rootScope.previousState);
                }
                else {
                    $location.path("/");
                }
            }
            else {
                notificationService.displayError("Toks vartotojas neegzistuoja arba neteisiklingas slaptazodis");
            }
        }

        function FailureLogin() {
            notificationService.displayError("Įvykot klaida");
        }
    }

})(angular.module('Order'))