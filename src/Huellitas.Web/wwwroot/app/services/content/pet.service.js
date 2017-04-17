(function () {
    angular.module('huellitasServices')
        .factory('petService', petService);

    petService.$inject = ['httpService'];

    function petService(http) {
        return {
            getAll: getAll,
            getById: getById,
            post: post,
            put: put
        };

        function getAll(filter) {
            return http.get('/api/pets', { params: filter });
        }

        function getById(id) {
            return http.get('/api/pets/' + id);
        }

        function post(model) {
            return http.post('/api/pets', model);
        }

        function put(model) {
            return http.put('/api/pets/' + model.id, model);
        }
    }
})();