(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('commentService', commentService);

    function commentService(httpService) {
        return {
            getAll: getAll,
            post: post
        };

        function getAll(filter) {
            return httpService.get('/api/comments', { params: filter });
        }

        function post(model) {
            return httpService.post('/api/comments', model);
        }
    }
})();
