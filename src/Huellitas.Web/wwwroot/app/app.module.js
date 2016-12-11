(function () {
    'use strict';

    angular.module('app', [
        // Angular modules 
        'ngRoute',

        // Custom modules 

        // 3rd Party Modules
        'angucomplete-alt',
        'underscore'
    ])
    .config(configRoutes);

    function configRoutes($routeProvider) {
        $routeProvider
        .when('/', {
            templateUrl: 'app/layout/welcome.html',
            controller: 'WelcomeController',
            controllerAs: 'main'
        })
        .when('/pets', {
            templateUrl: 'app/pets/listPets.html',
            controller: 'ListPetsController',
            controllerAs: 'main'
        })
        .when('/pets/:id/edit', {
            templateUrl: 'app/pets/editPet.html',
            controller: 'EditPetController',
            controllerAs: 'main'
        });
    }

})();