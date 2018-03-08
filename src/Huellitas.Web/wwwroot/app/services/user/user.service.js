(function () {
    angular.module('huellitasServices')
        .factory('userService', userService);

    userService.$inject = ['httpService'];

    function userService(http)
    {
        return {
            getAll: getAll,
            getById: getById,
            post: post,
            put: put,
            contact: contact,
            delete: deleteUser
        };

        function getAll(filter)
        {
            return http.get('/api/users', { params: filter });
        }

        function getById(id)
        {
            return http.get('/api/users/' + id);
        }

        function post(model) {
            return http.post('/api/users' , model);
        }

        function put(model) {
            return http.put('/api/users/' + model.id, model);
        }

        function contact(id, model)
        {
            return http.post('/api/users/' + id +'/contact', model);
        }

        function deleteUser(id)
        {
            return http.delete('/api/users/' + id);
        }
    }
})();