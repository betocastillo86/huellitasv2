(function () {
    angular.module('huellitasServices')
        .factory('petService', petService);

    petService.$inject = ['httpService'];

    function petService(http) {
        return {
            getAll: getAll,
            getById: getById,
            post: post,
            put: put,
            patch: patch,
            changeStatus: changeStatus,
            republish: republish,
            notify: notify
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

        function patch(id, jsonPatch) {
            return http.patch('/api/pets/' + id, jsonPatch);
        }

        function changeStatus(id, newStatus) {
            return patch(id, [{ op: 'replace', path: '/status', value: newStatus }]);
        }

        function republish(id) {
            return patch(id, [{ op: 'replace', path: '/closingDate', value: moment().format('YYYY/MM/DD') }]);
        }

        function notify(id, type) {
            return http.post('/api/pets/' + id + '/notify/' + type);
        }
    }
})();