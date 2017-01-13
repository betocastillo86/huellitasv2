(function () {
    'use strict';

    angular.module('app', [
        // Angular modules 
        'ngRoute',
        'ngStorage',
        'ngSanitize',

        // Custom modules 

        // 3rd Party Modules
        'angucomplete-alt',
        'underscore'
    ])
    .config(config)
    .run(run);

    function config($routeProvider, $locationProvider, $httpProvider) {

        $httpProvider.interceptors.push('expiredSessionInterceptor');

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
        })
        .when('/shelters', {
            templateUrl: '/app/shelters/listShelters.html',
            controller: 'ListSheltersController',
            controllerAs: 'main'
        })
        .when('/shelters/:id/edit', {
            templateUrl: '/app/shelters/editShelter.html',
            controller: 'EditShelterController',
            controllerAs: 'main'
        })
        .when('/shelters/new', {
            templateUrl: '/app/shelters/editShelter.html',
            controller: 'EditShelterController',
            controllerAs: 'main'
        })
        .when('/users', {
            templateUrl: '/app/users/listUsers.html',
            controller: 'ListUsersController',
            controllerAs: 'main'
        });
    }

    function run($rootScope, $http, $location, sessionService) {
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