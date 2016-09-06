(function () {

    angular.module("Order", ['common.core', 'common.ui'])
           .config(config)
           .run(run);

    config.$inject = ['$routeProvider', '$locationProvider'];

    function config($routeProvider, $locationProvider) {
        $routeProvider
        .when("/", {
            //Esami uzsakymai
            templateUrl: "scripts/spa/Order/Order.html",
            controller: "orderCtrl"
        }).when("/order/create", {
            templateUrl: "scripts/spa/Order/createOrder.html",
            controller: "createOrderCtrl"
        })
        .otherwise({ redirectTo: "/" })
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http']

    function run($rootScope, $location, $cookieStore, $http) {
        console.log("app is started");
        $rootScope.repository = $cookieStore.get('repository') || {};
        //if ($rootScope.repository.l) {

        //}
    }

})();