(function () {
    angular
        .module('huellitasServices')
        .factory('moduleService', moduleService);

    moduleService.$inject = ['$http'];

    function moduleService($http)
    {
        return {
            getAll: getAll
        };

        function getAll()
        {
            return $http.get('/api/modules')
                .then(getAllComplete)
                .catch(getAllError);

            function getAllComplete(response)
            {
                return response.data;
            }

            function getAllError()
            {
                console.log('Error consultando');
            }
        }
    }

})();