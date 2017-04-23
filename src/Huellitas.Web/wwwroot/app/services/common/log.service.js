(function () {

    angular.module('huellitasServices')
        .factory('logService', logService);

    logService.$inject = ['httpService'];

    function logService(http) {
        return {
            getAll: getAll
        };

        function getAll(filter) {
            return http.get('/api/logs', { params: filter });
        }
    }
})();