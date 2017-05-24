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
            .when('/perdidos', {
                templateUrl: '/app/front/contents/lostPets/lostPets.html',
                controller: 'LostPetsController',
                controllerAs: 'main'
            })
            .when('/perdidos/crear', {
                templateUrl: '/app/front/contents/lostPets/editLostPet.html',
                controller: 'EditLostPetController',
                controllerAs: 'main'
            })
            .when('/perdidos/:friendlyName/editar', {
                templateUrl: '/app/front/contents/lostPets/editLostPet.html',
                controller: 'EditLostPetController',
                controllerAs: 'main'
            })
            .when('/perdidos/:friendlyName', {
                templateUrl: '/app/front/contents/lostPets/lostPetDetail.html',
                controller: 'LostPetDetailController',
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
                templateUrl: '/app/front/contents/pets/newPet.html',
                controller: 'NewPetController',
                controllerAs: 'main'
            })
            .when('/formularios-adopcion/:id', {
                templateUrl: '/app/front/contents/pets/adoptionFormDetail.html',
                controller: 'AdoptionFormDetailController',
                controllerAs: 'main'
            })
            .when('/formularios-adopcion', {
                templateUrl: '/app/front/contents/pets/listAdoptionForms.html',
                controller: 'ListAdoptionFormController',
                controllerAs: 'main'
            })
            .when('/mis-huellitas', {
                templateUrl: '/app/front/contents/pets/myPets.html',
                controller: 'MyPetsController',
                controllerAs: 'main'
            })
            .when('/notificaciones', {
                templateUrl: '/app/front/home/notifications.html',
                controller: 'NotificationsController',
                controllerAs: 'main'
            })
            .when('/por-que-adoptar', {
                templateUrl: '/app/front/home/faq.html',
                controller: 'FaqController',
                controllerAs: 'main'
            })
            .when('/pagina-no-encontrada', {
                templateUrl: '/app/front/home/notfound.html',
                controller: 'NotFoundController',
                controllerAs: 'main'
            });

        $routeProvider.otherwise({ redirectTo: "/pagina-no-encontrada" });
        $routeProvider.caseInsensitiveMatch = true;
            
    }
})();