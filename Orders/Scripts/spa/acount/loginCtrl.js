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
            if (result.data) {
                var UserCredentials = {
                    UserName: $scope.user.UserName,
                    password: $scope.user.Password,
                    UserID: result.data.UserID
                }

                membershipService.SaveCredentials(UserCredentials);
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
                notificationService.displayError(result.data);
            }
        }

        function FailureLogin(error) {
            notificationService.displayError(error.data);
        }
    }

})(angular.module('Order'))