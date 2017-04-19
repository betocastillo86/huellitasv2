(function () {

    angular.module('huellitasServices')
        .factory('textResourceService', textResourceService);

    textResourceService.$inject = ['httpService'];

    function textResourceService(http) {
        return {
            get: get,
            put: put
        };

        function get(filter) {
            return http.get('/api/textresources', { params: filter });
        }

        function put(model) {
            return http.put('/api/textresources/' + model.id, model);
        }
    }
})();