(function () {
    'use strict';

    angular
        .module('huellitasAdmin')
        .config(huellitasAdminRoutes);

    huellitasAdminRoutes.$inject = ['$routeProvider'];

    function huellitasAdminRoutes($routeProvider) {
        var cacheKey = app.Settings.general.configJavascriptCacheKey;
        $routeProvider
            .when('/', {
                templateUrl: '/app/admin/layout/welcome.html?' + cacheKey,
                controller: 'WelcomeController',
                controllerAs: 'main'
            })
            .when('/login', {
                //templateUrl: 'app/pets/listPets.html',
                controller: 'LoginController',
                controllerAs: 'main'
            })
            .when('/pets', {
                templateUrl: '/app/admin/pets/listPets.html?' + cacheKey,
                controller: 'ListPetsController',
                controllerAs: 'main'
            })
            .when('/pets/:id/edit', {
                templateUrl: '/app/admin/pets/editPet.html?' + cacheKey,
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/pets/new', {
                templateUrl: '/app/admin/pets/editPet.html?' + cacheKey,
                controller: 'EditPetController',
                controllerAs: 'main'
            })
            .when('/lostpets', {
                templateUrl: '/app/admin/lostpets/listLostPets.html?' + cacheKey,
                controller: 'ListLostPetsController',
                controllerAs: 'main'
            })
            .when('/lostpets/:id/edit', {
                templateUrl: '/app/admin/lostpets/editLostPet.html?' + cacheKey,
                controller: 'EditLostPetController',
                controllerAs: 'main'
            })
            .when('/shelters', {
                templateUrl: '/app/admin/shelters/listShelters.html?' + cacheKey,
                controller: 'ListSheltersController',
                controllerAs: 'main'
            })
            .when('/shelters/:id/edit', {
                templateUrl: '/app/admin/shelters/editShelter.html?' + cacheKey,
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/shelters/new', {
                templateUrl: '/app/admin/shelters/editShelter.html?' + cacheKey,
                controller: 'EditShelterController',
                controllerAs: 'main'
            })
            .when('/users', {
                templateUrl: '/app/admin/users/listUsers.html?' + cacheKey,
                controller: 'ListUsersController',
                controllerAs: 'main'
            })
            .when('/users/:id/edit', {
                templateUrl: '/app/admin/users/editUser.html?' + cacheKey,
                controller: 'EditUserController',
                controllerAs: 'main'
            })
            .when('/users/new', {
                templateUrl: '/app/admin/users/editUser.html?' + cacheKey,
                controller: 'EditUserController',
                controllerAs: 'main'
            })
            .when('/adoptionforms', {
                templateUrl: '/app/admin/adoptionForms/listForms.html?' + cacheKey,
                controller: 'ListFormController',
                controllerAs: 'main'
            })
            .when('/adoptionforms/:id/edit', {
                templateUrl: '/app/admin/adoptionForms/editForm.html?' + cacheKey,
                controller: 'EditFormController',
                controllerAs: 'main'
            })
            .when('/systemsettings', {
                templateUrl: '/app/admin/systemsettings/listSystemSetting.html?' + cacheKey,
                controller: 'ListSystemSettingController',
                controllerAs: 'main'
            })
            .when('/textresources', {
                templateUrl: '/app/admin/textresources/listTextResources.html?' + cacheKey,
                controller: 'ListTextResourceController',
                controllerAs: 'main'
            })
            .when('/notifications', {
                templateUrl: '/app/admin/notifications/listNotifications.html?' + cacheKey,
                controller: 'ListNotificationsController',
                controllerAs: 'main'
            })
            .when('/notifications/mine', {
                templateUrl: '/app/admin/notifications/myNotifications.html?' + cacheKey,
                controller: 'MyNotificationsController',
                controllerAs: 'main'
            })
            .when('/notifications/:id/edit', {
                templateUrl: '/app/admin/notifications/editNotification.html?' + cacheKey,
                controller: 'EditNotificationController',
                controllerAs: 'main'
            })
            .when('/emailnotifications', {
                templateUrl: '/app/admin/emailnotifications/listEmailNotifications.html?' + cacheKey,
                controller: 'ListEmailNotificationsController',
                controllerAs: 'main'
            })
            .when('/emailnotifications/:id/edit', {
                templateUrl: '/app/admin/emailnotifications/editEmailNotification.html?' + cacheKey,
                controller: 'EditEmailNotificationController',
                controllerAs: 'main'
            })
            .when('/banners', {
                templateUrl: '/app/admin/sliders/listBanners.html?' + cacheKey,
                controller: 'ListBannersController',
                controllerAs: 'main'
            })
            .when('/banners/new', {
                templateUrl: '/app/admin/sliders/editBanner.html?' + cacheKey,
                controller: 'EditBannerController',
                controllerAs: 'main'
            })
            .when('/banners/:id/edit', {
                templateUrl: '/app/admin/sliders/editBanner.html?' + cacheKey,
                controller: 'EditBannerController',
                controllerAs: 'main'
            })
            .when('/logs', {
                templateUrl: '/app/admin/logs/listLogs.html?' + cacheKey,
                controller: 'ListLogsController',
                controllerAs: 'main'
            });
    }
})();
