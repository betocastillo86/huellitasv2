(function () {
    'use strict';

    angular
      .module('huellitasAdmin')
      .run(runHuellitasAdmin);

    runHuellitasAdmin.$inject = ['$rootScope', '$http', '$location', 'sessionService'];

    function runHuellitasAdmin($rootScope, $http, $location, sessionService) {
        // keep user logged in after page refresh
        if (sessionService.getCurrentUser()) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + sessionService.getToken();
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            var publicPages = ['/login', '/Login'];
            var restrictedPage = publicPages.indexOf($location.path()) === -1;
            if (restrictedPage && !sessionService.isAuthenticated()) {
                document.location = '/admin/login';
            }
            else if (!restrictedPage && sessionService.isAuthenticated()) {
                document.location = '/admin';
            }
        });
    }
})();
