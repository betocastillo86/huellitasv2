(function () {
    'use strict';

    angular.module('huellitas')
        .config(huellitasConfig);

    huellitasConfig.$inject = ['$locationProvider', '$httpProvider', '$compileProvider'];

    function huellitasConfig($locationProvider, $httpProvider, $compileProvider)
    {
        $httpProvider.interceptors.push('interceptorService');

        $compileProvider.debugInfoEnabled(false);
        $compileProvider.commentDirectivesEnabled(false);
        $compileProvider.cssClassDirectivesEnabled(false);

        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix("!");
    }

})();