(function () {
    angular.module('huellitas')
        .config(huellitasRoutes);

    huellitasRoutes.$inject = ['$routeProvider'];

    function huellitasRoutes($routeProvider)
    {
        $routeProvider
            .when('/', {
                templateUrl: '/app/front/home/home.html',
                controller: 'HomeController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/editar', {
                templateUrl: '/app/front/contents/editPet.html',
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/adoptar/formulario', {
                templateUrl: '/app/front/contents/adopt.html',
                controller: 'AdoptController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/adoptar', {
                templateUrl:    '/app/front/contents/adoptTerms.html',
                controller:     'AdoptTermsController',
                controllerAs:   'main'
            })
            .when('/sinhogar/:friendlyName', {
                templateUrl: '/app/front/contents/petDetail.html',
                controller: 'PetDetailController',
                controllerAs: 'main'
            })
            .when('/sinhogar', {
                templateUrl: '/app/front/contents/pets.html',
                controller: 'PetsController',
                controllerAs: 'main'
            })
            .when('/dar-en-adopcion/crear', {
                templateUrl: '/app/front/contents/editPet.html',
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/dar-en-adopcion', {
                templateUrl: '/app/front/contents/newPet.html'
            });
    }
})();