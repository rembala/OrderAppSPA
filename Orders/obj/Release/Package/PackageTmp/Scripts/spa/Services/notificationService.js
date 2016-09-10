(function (app) {

    app.factory("notificationService", notificationService)

    function notificationService() {
        toastr.options = {
            "Debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeout": 300,
            "timeout": 300,
            "extendedTimout": 300
        };

        var service = {
            displaySuccess: displaySucess,
            displayError: displayError,
            displayInfo: displayInfo,
            displayWarning: displayWarning
        };

        return service;

        function displayWarning(message) {
            toastr.warning(message);
        }

        function displaySucess(message) {
            toastr.success(message);
        }

        function displayError(error) {
            toastr.error(error);
        }
        function displayInfo(message) {
            toastr.info(message);
        }
    }

})(angular.module('common.core'));