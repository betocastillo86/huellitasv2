
(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('routingService', routingService);

    function routingService() {
        return {
            getRoute: getRoute    
        };

        function getRoute(routeName, params)
        {
            switch (routeName) {
                case 'pets':
                    return '/sinhogar';
                case 'pet':
                    return '/sinhogar/' + params.friendlyName;
                case 'home':
                    return '/';
                default:
            }
        }
    }
})();
