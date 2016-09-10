(function (app) {

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'membershipService', '$location', 'apiService', 'notificationService', '$rootScope'];

    function registerCtrl($scope, membershipService, $location, apiService, notificationService, $rootScope) {
        $scope.user = {};
        $scope.register = register;
        $scope.selectedRole = null;
        $scope.roles = [];
        $scope.loadData = loadData;

        function loadData() {
            apiService.get('api/authentication/Roles', null, successRolesGet, failureRolesGet);
        }

        function successRolesGet(result) {
            $scope.roles = result.data;
        }
        function failureRolesGet() {
            console.log("Failure roles");
        }

        function register() {
            apiService.post('api/authentication/Register', $scope.user, successful, Failure);
        }

        function successful(result) {
            //Gaunamas vartotojas ir identifuojama ar buvo issaugotas
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
                notificationService.displayError("Nepavyko išsaugoti vartotoją");
            }
        }

        function Failure(te) {
            notificationService.displaySuccess("Įvyko klaida");
        }
        loadData();
    }

})(angular.module('Order'));