(function () {
    angular.module('huellitasServices')
        .factory('roleService', roleService);

    roleService.$inject = ['httpService'];

    function roleService(http)
    {
        return {
            getAll : getAll
        };

        function getAll()
        {
            return http.get('/api/roles');
        }
    }
})();