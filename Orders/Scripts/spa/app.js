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
        }).when("/Products", {
            templateUrl: "scripts/spa/Product/Product.html",
            controller: "productCtrl"
        }).when("/Countries", {
            templateUrl: "scripts/spa/Countries/countries.html",
            controller: "countriesCtrl"
        }).when("/Clients", {
            templateUrl: "scripts/spa/Client/client.html",
            controller: "clientCtrl"
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