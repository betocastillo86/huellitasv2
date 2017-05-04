
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
                case 'newpet0':
                    return '/dar-en-adopcion';
                case 'newpet1':
                    return '/dar-en-adopcion/crear';
                case 'editpet':
                    return '/sinhogar/' + params.friendlyName + '/editar';
                case 'myaccount':
                    return '/mis-datos';
                case 'home':
                    return '/';
                default:
            }
        }
    }
})();
