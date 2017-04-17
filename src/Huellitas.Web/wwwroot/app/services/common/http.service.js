(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('httpService', httpService);

    httpService.$inject = [
        '$http',
        '$q',
        '$window',
        'helperService'];

    function httpService(
        $http,
        $q,
        $window,
        helperService) {

        var service = {
            post: post,
            get: get,
            put: put,
            delete: del
        };

        return service;

        function post(url, model, params) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.post(helperService.configServiceUrl(url), model, params)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function get(url, model) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.get(helperService.configServiceUrl(url), model)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function put(url, model, params) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.put(helperService.configServiceUrl(url), model, params)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function del(url, model) {
            var defered = $q.defer();
            var promise = defered.promise;
            $http.delete(helperService.configServiceUrl(url), model)
                .then(GetComplete.bind(null, defered), GetFailed.bind(null, defered));
            return promise;
        }

        function GetComplete(defered, response) {
            defered.resolve(response.data);
        }

        function GetFailed(defered, response) {
            defered.reject(response);
        }
    }
})();
