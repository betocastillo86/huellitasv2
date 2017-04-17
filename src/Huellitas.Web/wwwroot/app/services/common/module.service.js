(function () {
    angular
        .module('huellitasServices')
        .factory('moduleService', moduleService);

    moduleService.$inject = ['httpService'];

    function moduleService(http)
    {
        return {
            getAll: getAll
        };

        function getAll()
        {
            return http.get('/api/modules');
        }
    }

})();