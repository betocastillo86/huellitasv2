(function () {
    'use strict';

    angular
        .module('huellitasAdmin')
        .config(huellitasAdminRoutes);

    huellitasAdminRoutes.$inject = ['$routeProvider'];

    function huellitasAdminRoutes($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: '/app/admin/layout/welcome.html',
                controller: 'WelcomeController',
                controllerAs: 'main'
            })
            .when('/login', {
                //templateUrl: 'app/pets/listPets.html',
                controller: 'LoginController',
                controllerAs: 'main'
            })
            .when('/pets', {
                templateUrl: '/app/admin/pets/listPets.html',
                controller: 'ListPetsController',
                controllerAs: 'main'
            })
            .when('/pets/:id/edit', {
                templateUrl: '/app/admin/pets/editPet.html',
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/pets/new', {
                templateUrl: '/app/admin/pets/editPet.html',
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/lostpets', {
                templateUrl: '/app/admin/lostpets/listLostPets.html',
                controller: 'ListLostPetsController',
                controllerAs: 'main'
            })
            .when('/lostpets/:id/edit', {
                templateUrl: '/app/admin/lostpets/editLostPet.html',
                controller: 'EditLostPetController',
                controllerAs: 'main'
            })
            .when('/shelters', {
                templateUrl: '/app/admin/shelters/listShelters.html',
                controller: 'ListSheltersController',
                controllerAs: 'main'
            })
            .when('/shelters/:id/edit', {
                templateUrl: '/app/admin/shelters/editShelter.html',
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/shelters/new', {
                templateUrl: '/app/admin/shelters/editShelter.html',
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/users', {
                templateUrl: '/app/admin/users/listUsers.html',
                controller: 'ListUsersController',
                controllerAs: 'main'
            })
            .when('/users/:id/edit', {
                templateUrl: '/app/admin/users/editUser.html',
                controller: 'EditUserController',
                controllerAs: 'main'
            })
            .when('/users/new', {
                templateUrl: '/app/admin/users/editUser.html',
                controller: 'EditUserController',
                controllerAs: 'main'
            })
            .when('/adoptionforms', {
                templateUrl: '/app/admin/adoptionforms/listForms.html',
                controller: 'ListFormController',
                controllerAs: 'main'
            })
            .when('/adoptionforms/:id/edit', {
                templateUrl: '/app/admin/adoptionforms/editForm.html',
                controller: 'EditFormController',
                controllerAs: 'main'
            })
            .when('/systemsettings', {
                templateUrl: '/app/admin/systemsettings/listSystemSetting.html',
                controller: 'ListSystemSettingController',
                controllerAs: 'main'
            })
            .when('/textresources', {
                templateUrl: '/app/admin/textresources/listTextResources.html',
                controller: 'ListTextResourceController',
                controllerAs: 'main'
            })
            .when('/notifications', {
                templateUrl: '/app/admin/notifications/listNotifications.html',
                controller: 'ListNotificationsController',
                controllerAs: 'main'
            })
            .when('/notifications/mine', {
                templateUrl: '/app/admin/notifications/myNotifications.html',
                controller: 'MyNotificationsController',
                controllerAs: 'main'
            })
            .when('/notifications/:id/edit', {
                templateUrl: '/app/admin/notifications/editNotification.html',
                controller: 'EditNotificationController',
                controllerAs: 'main'
            })
            .when('/emailnotifications', {
                templateUrl: '/app/admin/emailnotifications/listEmailNotifications.html',
                controller: 'ListEmailNotificationsController',
                controllerAs: 'main'
            })
            .when('/emailnotifications/:id/edit', {
                templateUrl: '/app/admin/emailnotifications/editEmailNotification.html',
                controller: 'EditEmailNotificationController',
                controllerAs: 'main'
            })
            .when('/banners', {
                templateUrl: '/app/admin/sliders/listBanners.html',
                controller: 'ListBannersController',
                controllerAs: 'main'
            })
            .when('/banners/new', {
                templateUrl: '/app/admin/sliders/editBanner.html',
                controller: 'EditBannerController',
                controllerAs: 'main'
            })
            .when('/banners/:id/edit', {
                templateUrl: '/app/admin/sliders/editBanner.html',
                controller: 'EditBannerController',
                controllerAs: 'main'
            })
            .when('/logs', {
                templateUrl: '/app/admin/logs/listLogs.html',
                controller: 'ListLogsController',
                controllerAs: 'main'
            });
    }
})();
