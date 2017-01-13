(function () {
    angular.module('app')
        .factory('userService', userService);

    userService.$inject = ['$http'];

    function userService($http)
    {
        return {
            getAll : getAll
        };

        function getAll(filter)
        {
            return $http.get('/api/users', { params: filter });
        }
    }
})();