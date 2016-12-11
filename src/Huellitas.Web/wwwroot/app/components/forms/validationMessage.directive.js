(function () {
    angular.module('app')
        .directive('validationMessage', validationMessage);

    function validationMessage() {
        var directive = {
            restrict: 'E',
            template: '<ul class="parsley-errors-list filled" ng-if="!form.$submitted && !field.$valid"><li class="parsley-required">Campo invalido.</li></ul>',
            scope: {
                form: '=',
                field:'='
            },
            link: link
        };

        return directive;

        function link(scope, element, attrs)
        {
            
        }
    }

})();