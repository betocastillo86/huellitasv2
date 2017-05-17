(function () {

    angular.module('huellitasServices')
        .factory('authenticationService', authenticationService);

    authenticationService.$inject = [
        '$http',
        '$q',
        '$window',
        '$interval',
        'httpService',
        'sessionService',
        'helperService',
        'routingService'];

    function authenticationService($http, $q, $window, $interval, http, sessionService, helperService, routingService)
    {
        return {
            promiseAuth: null,
            post: post,
            get: get,
            showLogin: showLogin,
            external: external,
            postExternal: postExternal,
            setSessionUser: setSessionUser
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
                setSessionUser(response);
                deferred.resolve(response);
            }

            function postError(response)
            {
                deferred.reject(response);
            }
        }

        function setSessionUser(response)
        {
            var user = { email: response.email, id: response.id, name: response.name, token: response.token.accessToken };
            sessionService.setCurrentUser(user);
            $http.defaults.headers.common.Authorization = 'Bearer ' + response.token.accessToken;
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

        function external(provider)
        {
            var url = '';
            if (provider == 'facebook')
            {
                url = getFacebookUrl();
            }

            var defered = $q.defer();
            var promise = defered.promise;

            var width = 500;
            var height = 500;
            var top = $window.screenY + (($window.outerHeight - height) / 2.5);
            var left = $window.screenX + (($window.outerWidth - width) / 2);

            var newWindow = $window.open(url, 'oauth', 'width='.concat(width, ',height=', height, ',top=', top, ',left=', left));

            var polling = $interval(function () {
                try {

                    if (newWindow.location.search || newWindow.location.hash) {
                        switch (provider) {
                            case 'facebook':
                                token = newWindow.location.hash.queryStringToJson().access_token;
                                break;
                            default:
                        }

                        postExternal(provider, token)
                            .then(postCompleted)
                            .catch(helperService.handleException);

                        newWindow.close();
                        $interval.cancel(polling);
                    }

                    if (!newWindow || newWindow.closed) {
                        $interval.cancel(polling);
                    }

                } catch (e) {
                    console.log(e);
                }
            }, 500);

            function postCompleted(result) {
                defered.resolve(result);
            }

            return promise;
        }

        function postExternal(provider, token1, token2)
        {
            var deferred = $q.defer();

            http.post('/api/auth/external', { socialNetwork: provider, token: token1, token2: token2 })
                .then(postCompleted)
                .catch(postError);

            return deferred.promise;

            function postCompleted(response) {
                response = response;
                setSessionUser(response);
                deferred.resolve(response);
            }

            function postError(response) {
                deferred.reject(response);
            }
        }

        function getFacebookUrl()
        {
            var params = {
                response_type: 'token',
                client_id: app.Settings.general.facebookPublicToken,
                redirect_uri: routingService.getFullRoute('facebooklogin'),
                display: 'popup',
                scope: 'email',
                state: 'undefined'
            };
            
            return 'https://www.facebook.com/v2.5/dialog/oauth?' + $.param(params);
        }
    }
})();