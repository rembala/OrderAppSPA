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
            //Sukurti uzsakyma
        }).when("/create", {
            templateUrl: "scripts/spa/Order/createOrder.html",
            controller: "createOrderCtrl"
            //perziureti esamuosius produktus/prideti
        }).when("/Products", {
            templateUrl: "scripts/spa/Product/Product.html",
            controller: "productCtrl"
            //perziureti salys,trinti
        }).when("/Countries", {
            templateUrl: "scripts/spa/Countries/countries.html",
            controller: "countriesCtrl"
            //perziureti klientus,trinti
        }).when("/Clients", {
            templateUrl: "scripts/spa/Client/client.html",
            controller: "clientCtrl"
        }).when("/Register", {
            templateUrl: "scripts/spa/acount/register.html",
            controller: "registerCtrl"
        }).when("/Login", {
            templateUrl: "scripts/spa/acount/Login.html",
            controller: "LoginrCtrl"
        })
        .otherwise({ redirectTo: "/" });

        //TODO: Perkrovimo metu luzta

        //$locationProvider.html5Mode({
        //    enabled: true,
        //    requireBase: false
        //});
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http', '$rootScope']

    function run($rootScope, $location, $cookieStore, $http, $rootScope) {
        console.log("app is started");
        //refresho metu uzsetinama sesija
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = "Basic" + $rootScope.repository.loggedUser.authData;
        }
    }
})();