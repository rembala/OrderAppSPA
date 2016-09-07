(function (app) {

    app.controller('countriesCtrl', countriesCtrl);
    countriesCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location'];

    function countriesCtrl($scope, apiService, notificationService, $location) {

        $scope
    }

})(angular.module('Order'));