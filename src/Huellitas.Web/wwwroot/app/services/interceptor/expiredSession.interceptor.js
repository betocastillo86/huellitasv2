(function () {
    angular.module('app')
        .factory('expiredSessionInterceptor', expiredSessionInterceptor);

    expiredSessionInterceptor.$inject = ['$q', 'sessionService'];

    function expiredSessionInterceptor($q, sessionService) {
        var interceptor = {
            responseError: responseError
        };

        return interceptor;

        function responseError(response) {
            if (response.status == 401) {
                sessionService.removeCurrentUser();
                document.location = '/admin/login';
                return;
            }

            return $q.reject(response);
        }
    }

})();