(function () {
    angular.module('huellitasServices')
        .factory('petService', petService);

    petService.$inject = ['$http'];

    function petService($http) {
        return {
            getAll: getAll,
            getById: getById,
            post: post,
            put: put
        };

        function getAll(filter) {
            return $http.get('/api/pets', { params: filter })
            .then(getAllComplete)
            .catch(getAllError);

            function getAllComplete(response) {
                return response.data;
            }

            function getAllError() {
                console.log('Get all error');
            }
        }

        function getById(id) {
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

        function post(model) {
            return $http.post('/api/pets', model);
        }

        function put(model) {
            return $http.put('/api/pets/' + model.id, model);
        }
    }
})();