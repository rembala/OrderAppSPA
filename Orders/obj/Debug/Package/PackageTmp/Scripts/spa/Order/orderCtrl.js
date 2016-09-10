(function (app) {

    app.controller("orderCtrl", orderCtrl);

    orderCtrl.$inject = ['$scope', 'apiService', "notificationService", '$modal']

    function orderCtrl($scope, apiService, notificationService, $modal) {

        $scope.loadData = loadData;
        $scope.Orders = [];
        $scope.loadingOrders = true;
        $scope.OrderId = null;
        $scope.page = 0;
        $scope.clearSearch = clearSearch;

        function loadData(page) {
            page = page || 0;

            var config = {
                params: {
                    page: page,
                    pagesize: 4,
                    filter: $scope.FilterOrderTxt
                }
            };

            apiService.get("api/Orders/", config, OrderLoadedCompleted, OrderLoadedFailure);
        }

        function OrderLoadedCompleted(result) {
            $scope.loadingOrders = false;
            $scope.Orders = result.data.Items;

            $scope.totalCount = result.data.TotalItemsCount;
            $scope.pagesCount = result.data.TotalPages;
            $scope.page = result.data.Page;
            if ($scope.FilterOrderTxt) {
                notificationService.displaySuccess("Rasta duomenų: " + result.data.Items.length);
            } else {
                notificationService.displaySuccess("Duomenys atsivaizdavo")
            }
        }

        function OrderLoadedFailure(Failure) {
            var error = Failure;
        }

        function clearSearch() {
            $scope.FilterOrderTxt = '';
            loadData();
        }

        $scope.OpenOrderProductsTableModal = function (orderId) {
            $scope.OrderId = orderId;
            $modal.open({
                templateUrl: "Scripts/spa/Order/orderProductsModal.html",
                controller: "OrderProductModalCtrl",
                scope: $scope
            }).result.then(function ($scope) {
                //loadData();
            }, function () {
                //Eroras
            })
        }
        loadData();
    }

})(angular.module('Order'));