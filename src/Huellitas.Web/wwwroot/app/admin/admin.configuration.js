(function () {

    'use strict';

    angular.module('huellitasAdmin')
        .config(huellitasAdminConfiguration);

    huellitasAdminConfiguration.$inject = ['$locationProvider', '$httpProvider'];

    function huellitasAdminConfiguration($locationProvider, $httpProvider)
    {
        $httpProvider.interceptors.push('expiredSessionInterceptor');
        $httpProvider.interceptors.push('interceptorService');

        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix("!");
    }
})();