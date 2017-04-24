(function () {
    'use strict';

    angular.module('huellitas')
        .config(huellitasConfig);

    huellitasConfig.$inject = ['$locationProvider', '$httpProvider'];

    function huellitasConfig($locationProvider, $httpProvider)
    {
        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix("!");
    }

})();