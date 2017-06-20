(function () {

    'use strict';

    angular.module('huellitasAdmin')
        .config(huellitasAdminConfiguration);

    huellitasAdminConfiguration.$inject = ['$locationProvider', '$httpProvider', '$compileProvider'];

    function huellitasAdminConfiguration($locationProvider, $httpProvider, $compileProvider)
    {
        $httpProvider.interceptors.push('expiredSessionInterceptor');
        $httpProvider.interceptors.push('interceptorService');

        $compileProvider.debugInfoEnabled(false);
        $compileProvider.commentDirectivesEnabled(false);
        $compileProvider.cssClassDirectivesEnabled(false);

        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix("!");
    }
})();