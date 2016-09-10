(function (app) {

    app.controller('countriesCtrl', countriesCtrl);
    countriesCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location'];

    function countriesCtrl($scope, apiService, notificationService, $location) {

        $scope.AddNewcountry = AddNewcountry;
        $scope.Country = {};
        $scope.loadData = loadData;
        $scope.countries = [];
        $scope.Loadingcountries = true;

        function loadData() {
            apiService.get("api/Country", '', showcountrydata, FailureSaveCountry);
        }

        function showcountrydata(result) {
            $scope.countries = result.data;
            notificationService.displaySuccess("Duomenys atsivaizdavo")
            $scope.Loadingcountries = false;
        }

        function AddNewcountry() {
            $scope.Loadingcountries = true;
            apiService.post('api/country/add', { CountryName: $scope.Country.name }, SuccessfullSaveCountry, FailureSaveCountry);
        }
        function SuccessfullSaveCountry(result) {
            $scope.countries = result.data;
            notificationService.displaySuccess("Sėkmingai sukurtas");
            $scope.Loadingcountries = false;
        }
        function FailureSaveCountry() {
            notificationService.displaySuccess("Įvyko klaida");
        }

        loadData();
    }

})(angular.module('Order'));