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
        $httpProvider.interceptors.push('interceptorService');
        

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
        })
        .when('/systemsettings', {
            templateUrl: '/app/systemsettings/listSystemSetting.html',
            controller: 'ListSystemSettingController',
            controllerAs: 'main'
        })
        .when('/textresources', {
            templateUrl: '/app/textresources/listTextResources.html',
            controller: 'ListTextResourceController',
            controllerAs: 'main'
        })
        .when('/notifications', {
            templateUrl: '/app/notifications/listNotifications.html',
            controller: 'ListNotificationsController',
            controllerAs: 'main'
        })
        .when('/notifications/mine', {
            templateUrl: '/app/notifications/myNotifications.html',
            controller: 'MyNotificationsController',
            controllerAs: 'main'
        })
        .when('/notifications/:id/edit', {
            templateUrl: '/app/notifications/editNotification.html',
            controller: 'EditNotificationController',
            controllerAs: 'main'
        })
        .when('/emailnotifications', {
            templateUrl: '/app/emailnotifications/listEmailNotifications.html',
            controller: 'ListEmailNotificationsController',
            controllerAs: 'main'
        })
        .when('/emailnotifications/:id/edit', {
            templateUrl: '/app/emailnotifications/editEmailNotification.html',
            controller: 'EditEmailNotificationController',
            controllerAs: 'main'
        });

        String.prototype.getIntervalTime = function () {
            if (this !== '') {
                var date = new Date(this);
                var difference = (window.currentDate - date) / 1000;

                if (difference <= 5) {
                    return 'En este momento';
                }

                if (difference <= 20) {
                    return 'Hace segundos';
                }

                if (difference <= 60) {
                    return 'Hace 1 minuto';
                }

                if (difference < 3600) {
                    return 'Hace '.concat(parseInt(difference / 60), ' minutos');
                }

                if (difference <= 5400) {
                    return 'Hace 1 hora';
                }

                if (difference < 84600) {
                    return 'Hace '.concat(parseInt(difference / 3600), ' horas');
                }
                else {
                    return 'Hace '.concat(parseInt(difference / 86400), ' dias');
                }
            } else {
                return '';
            }
        };





















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