(function () {
    angular.module('app')
        .factory('sessionService', sessionService);

    sessionService.$inject = ['$localStorage'];

    function sessionService($localStorage)
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