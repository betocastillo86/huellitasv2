(function () {
    angular.module('app')
        .factory('notificationService', notificationService);

    notificationService.$inject = ['$http'];

    function notificationService($http) {
        return {
            getAll: getAll,
            getById: getById,
            put : put
        };

        function getAll(filter) {
            return $http.get('/api/notifications', { params: filter });
        }

        function getById(id) {
            return $http.get('/api/notifications/' + id);
        }

        function put(id, model) {
            return $http.put('/api/notifications/' + id, model);
        }
    }
})();