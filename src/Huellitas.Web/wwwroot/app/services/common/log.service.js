(function () {

    angular.module('huellitasServices')
        .factory('logService', logService);

    logService.$inject = ['httpService'];

    function logService(http) {
        return {
            getAll: getAll,
            post:post,
            clean: clean
        };

        function getAll(filter) {
            return http.get('/api/logs', { params: filter });
        }

        function post(model)
        {
            return http.post('/api/logs', model);
        }

        function clean()
        {
            return http.post('/api/logs/clean');
        }
    }
})();