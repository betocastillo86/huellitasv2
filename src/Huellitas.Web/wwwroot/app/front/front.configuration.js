(function () {
    'use strict';

    angular.module('huellitas')
        .config(huellitasConfig);

    huellitasConfig.$inject = ['$locationProvider', '$httpProvider'];

    function huellitasConfig($locationProvider, $httpProvider)
    {
        $httpProvider.interceptors.push('interceptorService');

        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix("!");
    }

})();