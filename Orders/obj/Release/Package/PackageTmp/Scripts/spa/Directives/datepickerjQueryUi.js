(function (app) {

    app.directive('jqdatepicker', jqdatepicker)

    function jqdatepicker() {
        return {
            restrict: "A",
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                element.datepicker({
                    dateFormat: "yy-mm-dd",
                    onSelect: function (dateValue) {
                        scope.order.PlannedDate = dateValue;
                        scope.$apply();
                    }
                })
            }
        }
    }

})(angular.module('common.ui'))