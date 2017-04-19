(function () {
    angular.module('huellitasServices')
        .factory('contentService', contentService);

    contentService.$inject = ['httpService'];

    function contentService(http) {
        return {
            postUser: postUser,
            getUsers: getUsers,
            deleteUser: deleteUser
        };

        function deleteUser(contentId, userId) {
            return http.delete('/api/contents/' + contentId + '/users/' + userId);
        }

        function postUser(contentId, contentUser) {
            return http.post('/api/contents/' + contentId + '/users', contentUser);
        }

        function getUsers(contentId, filter) {
            return http.get('/api/contents/' + contentId + '/users', { params: filter });
        }
    }
})();