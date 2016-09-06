(function (app) {

    app.directive('topBar', topBar);

    function topBar() {
        return {
            restrict: "E",
            replace: true,
            templateUrl: "Scripts/spa/Layout/topBar.html"
        }
    }

})(angular.module('common.ui'))