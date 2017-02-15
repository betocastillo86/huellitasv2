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
        'underscore',
        'textAngular'
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
        })
        .when('/users/:id/edit', {
            templateUrl: '/app/users/editUser.html',
            controller: 'EditUserController',
            controllerAs: 'main'
        })
        .when('/users/new', {
            templateUrl: '/app/users/editUser.html',
            controller: 'EditUserController',
            controllerAs: 'main'
        })
        .when('/adoptionforms', {
            templateUrl: '/app/adoptionforms/listforms.html',
            controller: 'ListFormController',
            controllerAs: 'main'
        })
        .when('/adoptionforms/:id/edit', {
            templateUrl: '/app/adoptionforms/editform.html',
            controller: 'EditFormController',
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