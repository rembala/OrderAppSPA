(function (app) {

    app.controller("rootCtrl", rootCtrl);

    rootCtrl.$inject = ['$scope', '$location', '$rootScope', 'membershipService', ]

    function rootCtrl($scope, $location, $rootScope, membershipService) {
        $scope.userData = {};
        $scope.userData.displayUserInformation = displayUserInformation;
        $scope.logout = logout;

        function displayUserInformation() {
            $scope.userData.loggedIn = membershipService.IsUserLoggedIn();
            if ($scope.userData.loggedIn) {
                $scope.userData.UserName = $rootScope.repository.loggedUser.UserName;
            }
        }
        function logout() {
            membershipService.RemoveCredentials();
            $scope.userData.displayUserInformation();
            $location.path("/");
        }
        displayUserInformation();
    }

})(angular.module('Order'));