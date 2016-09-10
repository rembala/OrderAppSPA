(function (app) {

    app.controller("rootCtrl", rootCtrl);

    rootCtrl.$inject = ['$scope', '$location', '$rootScope', 'membershipService', ]

    function rootCtrl($scope, $location, $rootScope, membershipService) {
        $scope.userData = {};
        $scope.userData.displayUserInformation = displayUserInformation;
        $scope.logout = logout;
        $scope.UserIsUndefined = UserIsUndefined;

        function displayUserInformation() {
            $scope.userData.loggedIn = membershipService.IsUserLoggedIn();
            if ($scope.userData.loggedIn) {
                $scope.userData.UserName = $rootScope.repository.loggedUser.UserName;
            }
        }
        function UserIsUndefined() {
            return $scope.userData.loggedIn == false;
        }

        function logout() {
            membershipService.RemoveCredentials();
            $location.path('#/');
            $scope.userData.displayUserInformation();
        }
        displayUserInformation();
    }

})(angular.module('Order'));