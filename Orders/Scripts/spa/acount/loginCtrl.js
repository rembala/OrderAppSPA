(function (app) {

    app.controller('LoginrCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$scope', 'membershipService', '$location', 'apiService', 'notificationService'];
    
    function LoginCtrl() {

    }

})(angular.module('Order'))