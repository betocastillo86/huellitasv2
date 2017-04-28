(function () {
    'use strict';

    angular
        .module('huellitas')
        .run(runHuellitas);

    runHuellitas.$inject = ['$rootScope', '$http', '$location', '$window', 'sessionService'];

    function runHuellitas($rootScope, $http, $location, $window, sessionService) {
        // keep user logged in after page refresh
        if (sessionService.getCurrentUser()) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + sessionService.getToken();
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            var publicPages = ['/login', '/Login'];
            var restrictedPage = publicPages.indexOf($location.path()) === -1;
            if (restrictedPage && !sessionService.isAuthenticated()) {
                //document.location = '/admin/login';
                console.log('Sacar el login');
            }
            else if (!restrictedPage && sessionService.isAuthenticated()) {
                console.log('Sacar el login');
            }

            $window.document.body.scrollTop = 0;
        });
    }
})();
