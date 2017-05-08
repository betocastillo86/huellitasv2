(function () {
    angular.module('huellitasServices')
        .factory('adoptionFormService', adoptionFormService);

    adoptionFormService.$inject = ['httpService'];

    function adoptionFormService(http) {

        return {
            getAll: getAll,
            getById: getById,
            post: post,
            postUser: postUser,
            sendByEmail : sendByEmail
        };

        function getAll(filter)
        {
            return http.get('/api/adoptionforms', { params: filter });
        }

        function getById(id)
        {
            return http.get('/api/adoptionforms/' + id);
        }

        function postUser(formId, userId)
        {
            return http.post('/api/adoptionforms/' + formId + '/users', { userId : userId })
        }

        function sendByEmail(formId, email)
        {
            return http.post('/api/adoptionforms/' + formId + '/send', { email: email })
        }

        function post(model)
        {
            return http.post('/api/adoptionforms', model);
        }
    }

})();