(function () {
    'use strict';

    angular.module('app', [
        // Angular modules 
        'ngRoute'

        // Custom modules 

        // 3rd Party Modules

    ])
    .config(configRoutes);

    function configRoutes($routeProvider) {
        $routeProvider
        .when('/', {
            templateUrl: 'app/layout/welcome.html',
            controller: 'WelcomeController',
            controllerAs: 'vm'
        })
        .when('/pets', {
            templateUrl: 'app/pets/listPets.html',
            controller: 'ListPetsController',
            controllerAs: 'vm'
        });
    }

})();