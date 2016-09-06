(function (app) {
    app.controller('productCtrl', productCtrl);

    productCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$modal', '$location'];

    function productCtrl($scope, apiService, notificationService, $modal, $location) {
        $scope.loadProductData = loadProductData;
        $scope.Products = [];
        $scope.AddNewProduct = AddNewProduct;
        $scope.SelectedProductType = null;
        $scope.product = {};
        $scope.selectProductTypeFn = selectProductTypeFn;
        function loadProductData() {
            apiService.get('api/Product', null, ProductDataSuccesfull, ProductDataFailure);
        }

        function ProductDataSuccesfull(result) {
            $scope.Products = result.data;
        }

        function ProductDataFailure() {
            notificationService.displaySuccess("Įvyko klaida");
        }

        function AddNewProduct() {
            apiService.post('api/Product/add', { ProductName: $scope.product.ProductName, ProductType: $scope.SelectedProductType }
                , ProductAddedSuccesfull, ProductAddedFailure);
        }
        function ProductAddedSuccesfull(result) {
            $scope.Products = result.data;
            notificationService.displaySuccess("Produktas sėkmingai sukurtas");
            $scope.$broadcast('angucomplete-alt:clearInput');
        }
        function ProductAddedFailure() {
            notificationService.displaySuccess("Įvyko klaida");
        }

        function selectProductTypeFn($item) {
            if ($item) {
                $scope.SelectedProductType = $item.originalObject;      
            } else {
                $scope.SelectedProductType = null;
            }
        }
        loadProductData();
    }

})(angular.module('Order'))