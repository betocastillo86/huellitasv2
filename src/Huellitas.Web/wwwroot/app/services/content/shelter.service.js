(function () {
    angular.module('huellitasServices')
        .factory('shelterService', shelterService);

    shelterService.$inject = ['httpService'];

    function shelterService(http) {
        return {
            getAll: getAll,
            getById: getById,
            post: post,
            put: put
        };

        function getAll(filter) {
            return http.get('/api/shelters', { params: filter });
        }

        function getById(id)
        {
            return http.get('/api/shelters/' + id);
        }

        function post(model) {
            return http.post('/api/shelters', model);
        }

        function put(model) {
            return http.put('/api/shelters/' + model.id, model);
        }
    }

})();