(function (app) {
    app.controller('productCtrl', productCtrl);

    productCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$modal', '$location'];

    function productCtrl($scope, apiService, notificationService, $modal, $location) {
        $scope.loadProductData = loadProductData;
        $scope.Products = [];
        $scope.AddNewProduct = AddNewProduct;
        $scope.SelectedProductType = null;
        $scope.product = {};
        $scope.DissableButton = DissableButton;
        $scope.selectProductTypeFn = selectProductTypeFn;
        $scope.loadingProducts = true;

        function loadProductData() {
            apiService.get('api/Product', '', ProductDataSuccesfull, ProductDataFailure);
        }

        function ProductDataSuccesfull(result) {
            $scope.Products = result.data;
            $scope.loadingProducts = false;
        }

        function ProductDataFailure() {
            notificationService.displaySuccess("Įvyko klaida");
        }

        function AddNewProduct() {
            $scope.loadingProducts = true;
            apiService.post('api/Product/add', { ProductName: $scope.product.ProductName, ProductType: $scope.SelectedProductType }
                , ProductAddedSuccesfull, ProductAddedFailure);
        }
        function ProductAddedSuccesfull(result) {
            $scope.Products = result.data;
            notificationService.displaySuccess("Produktas sėkmingai sukurtas");
            $scope.$broadcast('angucomplete-alt:clearInput');
            $scope.ProductName = null;
            $scope.addProductForm.$setValidity('ProductName', true);
            $scope.loadingProducts = false;
        }
        function ProductAddedFailure() {
            notificationService.displaySuccess("Įvyko klaida");
        }

        function DissableButton() {
            return ( $scope.SelectedProductType == null);
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