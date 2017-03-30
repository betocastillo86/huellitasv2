(function () {

    angular.module('app')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$q', 'sessionService'];

    function authenticationService($http, $q, sessionService)
    {
        return {
            post: post,
            get : get
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
                var user = { email: response.email, id: response.id, name: response.name, token: response.token.accessToken };
                sessionService.setCurrentUser(user);

                $http.defaults.headers.common.Authorization = 'Bearer ' + response.token.accessToken;
                deferred.resolve(response);
            }

            function postError(response)
            {
                deferred.reject(response);
            }
        }

        function get()
        {
            return $http.get('/api/auth');
        }
    }
})();