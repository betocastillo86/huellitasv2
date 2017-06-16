(function () {
    angular.module('huellitasServices')
        .factory('sessionService', sessionService);

    sessionService.$inject = ['$localStorage', '$http'];

    function sessionService($localStorage, $http)
    {
        return {
            setCurrentUser: setCurrentUser,
            removeCurrentUser: removeCurrentUser,
            getCurrentUser: getCurrentUser,
            getToken: getToken,
            isAuthenticated: isAuthenticated
        };

        function setCurrentUser(currentUser)
        {
            $localStorage.currentUser = currentUser;
        }

        function getCurrentUser()
        {
            return $localStorage.currentUser;
        }

        function removeCurrentUser()
        {
            $localStorage.$reset({ currentUser: undefined });
            $http.defaults.headers.common.Authorization = '';
        }

        function getToken()
        {
            return $localStorage.currentUser ? $localStorage.currentUser.token : undefined;
        }

        function isAuthenticated()
        {
            return $localStorage.currentUser !== undefined;
        }
    }
})();