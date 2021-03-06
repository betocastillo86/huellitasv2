﻿(function () {
    angular.module('huellitasServices')
            .factory('adoptionFormAnswerService', adoptionFormAnswerService);

    adoptionFormAnswerService.$inject = ['httpService'];

    function adoptionFormAnswerService(http)
    {
        return {
            post: post,
            getByFormId: getByFormId
        };

        function post(model)
        {
            return http.post('/api/adoptionforms/' + model.adoptionFormId + '/answers', model);
        }

        function getByFormId(id) {
            return http.get('/api/adoptionforms/' + id + '/answers');
        }
    }
})();