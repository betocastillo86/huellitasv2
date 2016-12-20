(function () {
    angular.module('app')
        .directive('listPager', listPager);

    //listPager.$inject = ['$scope'];

    function listPager() {
        var directive = {
            restrict: 'E',
            templateUrl: '/app/components/pager/listPager.html',
            controller: 'ListPagerController',
            controllerAs: 'pager',
            //scope: {
            //    info: '@'   
            //},
            scope:false,
            //link: link
        };
        return directive;
    };
})();