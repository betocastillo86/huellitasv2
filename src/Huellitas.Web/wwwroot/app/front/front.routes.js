(function () {
    angular.module('huellitas')
        .config(huellitasRoutes);

    huellitasRoutes.$inject = ['$routeProvider'];

    function huellitasRoutes($routeProvider)
    {
        var cacheKey = app.Settings.general.configJavascriptCacheKey;
        $routeProvider
            .when('/', {
                templateUrl: '/app/front/home/home.html?' + cacheKey,
                controller: 'HomeController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/editar', {
                templateUrl: '/app/front/contents/pets/editPet.html?' + cacheKey,
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/adoptar/formulario', {
                templateUrl: '/app/front/contents/pets/adopt.html?' + cacheKey,
                controller: 'AdoptController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName/adoptar', {
                templateUrl: '/app/front/contents/pets/adoptTerms.html?' + cacheKey,
                controller: 'AdoptTermsController',
                controllerAs: 'main'
            })
            .when('/sinhogar/:friendlyName', {
                templateUrl: '/app/front/contents/pets/petDetail.html?' + cacheKey,
                controller: 'PetDetailController',
                controllerAs: 'main'
            })
            .when('/sinhogar', {
                templateUrl: '/app/front/contents/pets/pets.html?' + cacheKey,
                controller: 'PetsController',
                controllerAs: 'main'
            })
            .when('/fundaciones/crear', {
                templateUrl: '/app/front/contents/shelters/editShelter.html?' + cacheKey,
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/fundaciones/:friendlyName/editar', {
                templateUrl: '/app/front/contents/shelters/editShelter.html?' + cacheKey,
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/fundaciones/:friendlyName', {
                templateUrl: '/app/front/contents/shelters/shelterDetail.html?' + cacheKey,
                controller: 'ShelterDetailController',
                controllerAs: 'main'
            })
            .when('/perdidos', {
                templateUrl: '/app/front/contents/lostPets/lostPets.html?' + cacheKey,
                controller: 'LostPetsController',
                controllerAs: 'main'
            })
            .when('/perdidos/crear', {
                templateUrl: '/app/front/contents/lostPets/editLostPet.html?' + cacheKey,
                controller: 'EditLostPetController',
                controllerAs: 'main'
            })
            .when('/perdidos/:friendlyName/editar', {
                templateUrl: '/app/front/contents/lostPets/editLostPet.html?' + cacheKey,
                controller: 'EditLostPetController',
                controllerAs: 'main'
            })
            .when('/perdidos/:friendlyName', {
                templateUrl: '/app/front/contents/lostPets/lostPetDetail.html?' + cacheKey,
                controller: 'LostPetDetailController',
                controllerAs: 'main'
            })
            .when('/fundaciones', {
                templateUrl: '/app/front/contents/shelters/shelters.html?' + cacheKey,
                controller: 'SheltersController',
                controllerAs: 'main'
            })
            .when('/dar-en-adopcion/crear', {
                templateUrl: '/app/front/contents/pets/editPet.html?' + cacheKey,
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/dar-en-adopcion', {
                templateUrl: '/app/front/contents/pets/newPet.html?' + cacheKey,
                controller: 'NewPetController',
                controllerAs: 'main'
            })
            .when('/formularios-adopcion/:id', {
                templateUrl: '/app/front/contents/pets/adoptionFormDetail.html?' + cacheKey,
                controller: 'AdoptionFormDetailController',
                controllerAs: 'main'
            })
            .when('/formularios-adopcion', {
                templateUrl: '/app/front/contents/pets/listAdoptionForms.html?' + cacheKey,
                controller: 'ListAdoptionFormController',
                controllerAs: 'main'
            })
            .when('/mis-huellitas', {
                templateUrl: '/app/front/contents/pets/myPets.html?' + cacheKey,
                controller: 'MyPetsController',
                controllerAs: 'main'
            })
            .when('/notificaciones', {
                templateUrl: '/app/front/home/notifications.html?' + cacheKey,
                controller: 'NotificationsController',
                controllerAs: 'main'
            })
            .when('/por-que-adoptar', {
                templateUrl: '/app/front/home/faq.html?' + cacheKey,
                controller: 'FaqController',
                controllerAs: 'main'
            })
            .when('/pagina-no-encontrada', {
                templateUrl: '/app/front/home/notfound.html?' + cacheKey,
                controller: 'NotFoundController',
                controllerAs: 'main'
            });

        $routeProvider.otherwise({ redirectTo: "/pagina-no-encontrada" });
        $routeProvider.caseInsensitiveMatch = true;
            
    }
})();