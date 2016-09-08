(function (app) {

    app.controller('registerCtrl', registerCtrl);

    registerCtrl.$inject = ['$scope', 'membershipService', '$location', 'apiService', 'notificationService'];

    function registerCtrl($scope, membershipService, $location, apiService, notificationService) {
        $scope.user = {};
        $scope.register = register;
        $scope.selectedRole = null;
        $scope.roles = [];
        $scope.loadData = loadData;

        function loadData() {
            apiService.get('api/authentication/Roles', null, SuccessRolesGet, FailureRolesGet);
        }

        function SuccessRolesGet(result) {
            $scope.roles = result.data;
            $scope.selectedRole = $scope.roles[0].RoleID;
        }
        function FailureRolesGet() {
            console.log("Failure roles");
        }

        function register() {
            apiService.post('api/acount', user, successful, Failure);
        }

        function successful(result) {
            if (result.data.success) {
                membershipService.SaveCredentials($scope.user);
                notificationService.displaySuccess('Sveiki ', +$scope.user.userName);
                //$scope.userData =
            }
        }

        function Failure() {
            notificationService.displaySuccess("Įvyko klaida");
        }
        loadData();
    }

})(angular.module('Order'));