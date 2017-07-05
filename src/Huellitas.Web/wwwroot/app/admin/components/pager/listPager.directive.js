(function () {
    angular.module('huellitasAdmin')
        .directive('listPager', listPager);

    function listPager() {
        var directive = {
            restrict: 'E',
            templateUrl: '/app/admin/components/pager/listPager.html?' + app.Settings.general.configJavascriptCacheKey,
            controller: 'ListPagerController',
            controllerAs: 'pager',
            scope: false
        };
        return directive;
    };
})();