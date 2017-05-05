(function () {

    angular.module('huellitasServices')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = [
        '$http',
        '$q',
        '$window',
        'httpService',
        'sessionService',
        'helperService'];

    function authenticationService($http, $q, $window, http, sessionService, helperService)
    {
        return {
            promiseAuth: null,
            post: post,
            get: get,
            showLogin : showLogin
        };

        function post(model)
        {
            var deferred = $q.defer();

            http.post('/api/auth', model)
                .then(postCompleted)
                .catch(postError);

            return deferred.promise;

            function postCompleted(response)
            {
                response = response;
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
            return http.get('/api/auth');
        }

        function showLogin(scope)
        {
            this.promiseAuth = $q.defer();

            if (scope.root.currentUser) {
                this.promiseAuth.resolve(scope.root.currentUser);
            }
            else
            {
                helperService.compile($window.document.body, '<login-huellitas></login-huellitas>', scope);
            }
        }
    }
})();