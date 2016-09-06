(function (app) {

    app.controller("rootCtrl", rootCtrl);

    rootCtrl.$inject = ['$scope']
    function rootCtrl($scope) {
        $scope.testValueCtrl = 'Hello from root controller';
    }

})(angular.module('Order'));