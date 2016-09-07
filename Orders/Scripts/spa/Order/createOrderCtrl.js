(function (app) {

    app.controller('createOrderCtrl', createOrderCtrl);

    createOrderCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$modal', '$location'];

    function createOrderCtrl($scope, apiService, notificationService, $modal, $location) {

        $scope.selectProductFun = selectProductFun;
        $scope.addProductToOrder = addProductToOrder;
        $scope.Products = [];
        $scope.CountryId = null;
        $scope.ClientId = null;
        $scope.order = {};
        $scope.selectCountryFn = selectCountryFn;
        $scope.selectCustomerFn = selectCustomerFn;
        $scope.AddNewOrder = AddNewOrder;
        $scope.DisabledButton = DisabledButton;

        function AddNewOrder() {
            apiService.post("api/Orders/add",
               {
                   Products: $scope.Products,
                   CountryId: $scope.CountryId,
                   ClientId: $scope.ClientId,
                   OrderNo: $scope.order.OrderNo,
                   PlannedDate: $scope.order.PlannedDate
               }
            , OrderLoadedCompleted, Orderfailure)
        }

        function DisabledButton() {
            return $scope.addOrderForm.$valid == true &&
                $scope.Products.length > 0;
        }

        function OrderLoadedCompleted(result) {
            notificationService.displaySuccess("Sukurtas užsakymas ");
            $location.path('/');
        }
        function Orderfailure() {

        }

        function selectProductFun($item) {
            if ($item) {
                $scope.selectedProduct = $item.originalObject;
                $scope.addOrderForm.$setPristine()
            } else {
                $scope.selectedProduct = null;
            }
        }

        function selectCountryFn($item) {
            if ($item) {
                $scope.CountryId = $item.originalObject.CountryID;
            } else {
                $scope.CountryId = null;
            }
        }

        function selectCustomerFn($item) {
            if ($item) {
                $scope.ClientId = $item.originalObject.ClientID;
            } else {
                $scope.ClientId = null;
            }
        }

        function addProductToOrder() {
            $scope.Products.push($scope.selectedProduct);
            $scope.selectedProduct = null;

        }

    }

})(angular.module('Order'))