(function () {
    'use strict';

    angular.module('app', [
        // Angular modules 
        'ngRoute',
        'ngStorage',

        // Custom modules 

        // 3rd Party Modules
        'angucomplete-alt',
        'underscore'
    ])
    .config(configRoutes)
    .run(run);

    function configRoutes($routeProvider, $locationProvider) {

        $locationProvider.html5Mode(true);
        $locationProvider.hashPrefix("!");

        $routeProvider
        .when('/', {
            templateUrl: '/app/layout/welcome.html',
            controller: 'WelcomeController',
            controllerAs: 'main'
        })
        .when('/login', {
            //templateUrl: 'app/pets/listPets.html',
            controller: 'LoginController',
            controllerAs: 'main'
        })
        .when('/pets', {
            templateUrl: '/app/pets/listPets.html',
            controller: 'ListPetsController',
            controllerAs: 'main'
        })
        .when('/pets/:id/edit', {
            templateUrl: '/app/pets/editPet.html',
            controller: 'EditPetController',
            controllerAs: 'main'
        })
        .when('/pets/new', {
            templateUrl: '/app/pets/editPet.html',
            controller: 'EditPetController',
            controllerAs: 'main'
        });
    }

    function run($rootScope, $http, $location, $localStorage)
    {
        // keep user logged in after page refresh
        if ($localStorage.currentUser) {
            $http.defaults.headers.common.Authorization = 'Bearer ' + $localStorage.currentUser.token;
        }
        
        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            var publicPages = ['/login', '/Login'];
            var restrictedPage = publicPages.indexOf($location.path()) === -1;
            if (restrictedPage && !$localStorage.currentUser) {
                document.location = '/admin/login';
            }
            else if (!restrictedPage && $localStorage.currentUser)
            {
                document.location = '/admin';
            }
        });

    }

})();