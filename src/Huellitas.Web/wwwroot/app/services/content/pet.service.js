(function () {
    angular.module('app')
        .factory('petService', petService);

    petService.$inject = ['$http'];

    function petService($http)
    {
        return {
            getAll : getAll
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
    }
})();