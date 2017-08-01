(function () {

    angular.module('huellitasServices')
        .factory('logService', logService);

    logService.$inject = ['httpService'];

    function logService(http) {
        return {
            getAll: getAll,
            clean: clean
        };

        function getAll(filter) {
            return http.get('/api/logs', { params: filter });
        }

        function clean()
        {
            return http.post('/api/logs/clean');
        }
    }
})();