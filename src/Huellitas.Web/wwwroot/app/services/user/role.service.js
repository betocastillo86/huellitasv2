(function () {
    angular.module('app')
        .factory('roleService', roleService);

    roleService.$inject = ['$http'];

    function roleService($http)
    {
        return {
            getAll : getAll
        };

        function getAll()
        {
            return $http.get('/api/roles');
        }
    }
})();