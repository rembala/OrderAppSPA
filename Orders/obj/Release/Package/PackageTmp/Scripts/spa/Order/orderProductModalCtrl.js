(function (app) {

    app.controller('OrderProductModalCtrl', OrderProductModalCtrl)

    OrderProductModalCtrl.$inject = ['$scope', 'apiService', '$modalInstance', 'notificationService']

    function OrderProductModalCtrl($scope, apiService, $modalInstance, notificationService) {

        $scope.loadOrderProducts = loadOrderProducts;
        $scope.OrderProducts = null;

        function loadOrderProducts() {
            apiService.get("api/Orders/" + $scope.OrderId, null, OrderProductsLoadedCompleted, OrderProductsLoadedFailed)
        }

        function OrderProductsLoadedCompleted(result) {
            $scope.OrderProducts = result.data;
            notificationService.displayInfo("Produktų kiekis " + result.data.length);
        }
        function OrderProductsLoadedFailed() {
            notificationService.displayInfo("Įvyko klaida");
        }

        loadOrderProducts();
    }

})(angular.module('Order'));