(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('bannerService', bannerService);

    bannerService.$inject = ['httpService'];

    function bannerService(http) {
        return {
            getAll: getAll,
            getById: getById,
            put: put,
            post: post
        };

        function getAll(filter) {
            return http.get('/api/banners', { params: filter });
        }

        function getById(id) {
            return http.get('/api/banners/' + id);
        }

        function put(id, model) {
            return http.put('/api/banners/' + id, model);
        }

        function post(model) {
            return http.post('/api/banners/', model);
        }
    }
})();
