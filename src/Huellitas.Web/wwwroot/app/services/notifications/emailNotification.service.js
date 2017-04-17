(function () {
    angular.module('huellitasServices')
        .factory('emailNotificationService', emailNotificationService);

    emailNotificationService.$inject = ['$http'];

    function emailNotificationService($http) {
        return {
            getAll: getAll,
            put: put,
            post: post,
            getById: getById
        };

        function getAll(filter) {
            return $http.get('/api/emailnotifications', { params: filter });
        }

        function getById(id) {
            return $http.get('/api/emailnotifications/' + id);
        }

        function put(id, model) {
            return $http.put('/api/emailnotifications/' + id, model);
        }

        function post(model) {
            return $http.post('/api/emailnotifications/', model);
        }

    }
})();