(function () {
    angular.module('app')
        .factory('petService', petService);

    petService.$inject = ['$http'];

    function petService($http)
    {
        return {
            getAll: getAll,
            getById : getById
        };

        function getAll(filter)
        {
            return $http.get('/api/pets', { params: filter })
            .then(getAllComplete)
            .catch(getAllError);

            function getAllComplete(response)
            {
                return response.data;
            }

            function getAllError()
            {
                console.log('Get all error');
            }
        }

        function getById(id)
        {
            return $http.get('/api/pets/' + id)
            .then(getByIdCompleted)
            .catch(getByIdError);

            function getByIdCompleted(response) {
                return response.data;
            }

            function getByIdError(response) {
                console.log('Get by id error');
                return response;
            }
        }
    }
})();