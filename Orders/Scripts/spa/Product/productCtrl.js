(function (app) {
    app.controller('productCtrl', productCtrl);

    productCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$modal', '$location'];

    function productCtrl($scope, apiService, notificationService, $modal, $location) {
        $scope.loadProductData = loadProductData;
        $scope.Products = [];

        function loadProductData() {
            apiService.get('api/Product', null, ProductDataSuccesfull, ProductDataFailure);
        }

        function ProductDataSuccesfull(result) {
            $scope.Products = result.data;
        }

        function ProductDataFailure() {

        }

        function selectProductTypeFn($item) {
            if ($item) {
                $scope.CountryId = $item.originalObject.CountryID;
            } else {
                $scope.CountryId = null;
            }
        }
        loadProductData();
    }

})(angular.module('Order'))