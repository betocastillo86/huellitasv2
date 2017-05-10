
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
                    return '/sinhogar' + (params ?  '?'+$.param(params) : '');
                case 'pet':
                    return '/sinhogar/' + params.friendlyName;
                case 'adopt0':
                    return '/sinhogar/' + params.friendlyName + '/adoptar';
                case 'adopt1':
                    return '/sinhogar/' + params.friendlyName + '/adoptar/formulario';
                case 'shelters':
                    return '/fundaciones';
                case 'shelter':
                    return '/fundaciones/' + params.friendlyName;
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
                case 'newshelter':
                    return '/fundaciones/crear';
                case 'editshelter':
                    return '/fundaciones/' + params.friendlyName + '/editar';
                default:
            }
        }
    }
})();
