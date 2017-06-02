(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('cacheService', cacheService);

    cacheService.$inject = ['httpService'];

    function cacheService(http) {
        return {
            clear: clear
        };

        function clear() {
            return http.get('/cache/clear');
        }
    }
})();

