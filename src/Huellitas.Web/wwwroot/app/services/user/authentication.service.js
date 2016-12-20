(function () {

    angular.module('app')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$q', '$localStorage'];

    function authenticationService($http, $q, $localStorage)
    {
        return {
            post : post
        };

        function post(model)
        {
            var deferred = $q.defer();

            $http.post('/api/auth', model)
                .then(postCompleted)
                .catch(postError);

            return deferred.promise;

            function postCompleted(response)
            {
                response = response.data;
                $localStorage.currentUser = { email: response.email, id: response.id, name: response.name, token: response.token.accessToken };
                $http.defaults.headers.common.Authorization = 'Bearer ' + response.token.accessToken;
                deferred.resolve(response);
            }

            function postError(response)
            {
                deferred.reject(response);
            }
        }
    }
})();