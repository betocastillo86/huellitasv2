(function () {
    angular.module('app')
        .directive('listPager', listPager);

    function listPager() {
        var directive = {
            restrict: 'E',
            templateUrl: '/app/components/pager/listPager.html',
            controller: 'ListPagerController',
            controllerAs: 'pager',
            scope: false
        };
        return directive;
    };
})();