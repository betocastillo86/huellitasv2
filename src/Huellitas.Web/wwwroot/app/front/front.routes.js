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
                templateUrl: '/app/front/contents/pets/editPet.html',
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/adoptar/formulario', {
                templateUrl: '/app/front/contents/pets/adopt.html',
                controller: 'AdoptController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/adoptar', {
                templateUrl: '/app/front/contents/pets/adoptTerms.html',
                controller: 'AdoptTermsController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName', {
                templateUrl: '/app/front/contents/pets/petDetail.html',
                controller: 'PetDetailController',
                controllerAs: 'main'
            })
            .when('/sinhogar', {
                templateUrl: '/app/front/contents/pets/pets.html',
                controller: 'PetsController',
                controllerAs: 'main'
            })
            .when('/fundaciones/crear', {
                templateUrl: '/app/front/contents/shelters/editShelter.html',
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/fundaciones/:friendlyName/editar', {
                templateUrl: '/app/front/contents/shelters/editShelter.html',
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/fundaciones/:friendlyName', {
                templateUrl: '/app/front/contents/shelters/shelterDetail.html',
                controller: 'ShelterDetailController',
                controllerAs: 'main'
            })
            .when('/fundaciones', {
                templateUrl: '/app/front/contents/shelters/shelters.html',
                controller: 'SheltersController',
                controllerAs: 'main'
            })
            .when('/dar-en-adopcion/crear', {
                templateUrl: '/app/front/contents/pets/editPet.html',
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/dar-en-adopcion', {
                templateUrl: '/app/front/contents/pets/newPet.html'
            });
            
    }
})();