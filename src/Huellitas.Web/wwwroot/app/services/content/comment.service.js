(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('commentService', commentService);

    commentService.$inject = ['httpService'];

    function commentService(httpService) {
        return {
            getAll: getAll,
            post: post,
            delete: del
        };

        function getAll(filter) {
            return httpService.get('/api/comments', { params: filter });
        }

        function post(model) {
            return httpService.post('/api/comments', model);
        }

        function del(id)
        {
            return httpService.delete('/api/comments/'+id);
        }
    }
})();
