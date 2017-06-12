(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('routingService', routingService);

    function routingService() {
        return {
            getRoute: getRoute,
            getFullRoute: getFullRoute,
            getFullRouteOfFile: getFullRouteOfFile
        };

        function getRoute(routeName, params) {

            var routeValue = '/'+app.Settings.routes[routeName];

            switch (routeName) {
                case 'pets':
                case 'lostpets':
                case 'mypets':
                case 'forms':
                    return routeValue + (params ? '?' + $.param(params) : '');
                case 'pet':
                case 'shelter':
                case 'lostpet':
                case 'adopt0':
                case 'adopt1':
                case 'editpet':
                case 'editlostpet':
                case 'editshelter':
                    return routeValue.format(params.friendlyName);
                case 'shelters':
                case 'home':
                case 'newpet0':
                case 'newpet1':
                case 'newlostpet':
                case 'myaccount':
                case 'facebooklogin':
                case 'newshelter':
                case 'notifications':
                case 'faq':
                case 'notfound':
                    return routeValue;
                case 'contact': //////////////<------------- no borrar
                    return 'https://m.me/huellitas.social';
                case 'form':
                    return routeValue.format(params.id);
                default:
            }
        }

        function getFullRoute(routeName, params) {
            return app.Settings.general.siteUrl + getRoute(routeName, params);
        }

        function getFullRouteOfFile(file)
        {
            return app.Settings.general.siteUrl + (file[0] == '/' ? file.substring(1) : file);
        }
    }
})();