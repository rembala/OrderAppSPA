(function (app) {

    app.controller('clientCtrl', clientCtrl);
    clientCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location'];

    function clientCtrl($scope, apiService, notificationService, $location) {

        $scope.AddNewclient = AddNewclient;
        $scope.Client = {};
        $scope.loadData = loadData;
        $scope.clients = [];
        $scope.Loadingclients = true;

        function loadData() {
            apiService.get("api/Client", '', showClientDataSuccess, FailureClientResponse);
        }

        function showClientDataSuccess(result) {
            $scope.clients = result.data;
            notificationService.displaySuccess("Duomenys atsivaizdavo")
            $scope.Loadingclients = false;
        }

        function AddNewclient() {
            $scope.Loadingclients = true;
            apiService.post('api/Client/add', { ClientName: $scope.Client.name }, SucesfullClientSave, FailureClientResponse);
        }
        function SucesfullClientSave(result) {
            $scope.clients = result.data;
            notificationService.displaySuccess("Sėkmingai sukurtas");
            $scope.Loadingclients = false;
        }
        function FailureClientResponse() {
            notificationService.displaySuccess("Įvyko klaida");
        }

        loadData();
    }

})(angular.module('Order'));