(function (app) {

    app.directive("sideBar", sideBar);

    function sideBar() {
        return {
            restict: "E",
            replace: true,
            templateUrl: "Scripts/spa/Layout/sideBar.html"
        }
    }

})(angular.module('common.ui'))