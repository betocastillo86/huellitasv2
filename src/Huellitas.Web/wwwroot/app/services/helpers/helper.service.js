(function () {
    'use strict';

    angular
      .module('huellitasServices')
      .factory('helperService', helperService);

    helperService.$inject = [
        '$window',
        '$compile',
        'modalService'];

    function helperService($window, $compile, modalService) {
        var service = {
            configServiceUrl: configServiceUrl,
            handleException: handleException,
            isMobileWidth: isMobileWidth,
            compile: compile
        };

        return service;

        function configServiceUrl(localUrl, modalService) {
            if ($window.isIE) {
                var rdn = Math.floor(Math.random() * 600) + 1;
                var url = localUrl;
                return url.indexOf('?') > -1 ? url + '&rdn=' + rdn : url + '?rdn=' + rdn;
            } else {
                return localUrl;
            }
        }

        function handleException(data) {
            if (data.status == 500) {
                modalService.showError({ message: 'Ha occurrido un error inesperado. Intenta de nuevo' });
            }
            else if (data.status == 403) {
                modalService.showError({ message: 'No tienes permisos para acceder a esta funcionalidad' });
            }
            else if (data.status == 401) {
                modalService.showError({ message: 'Debes estar autenticado para realizar esta acción' });
            }
            else {
                modalService.showError({ error: data.data.error });
            }
        }

        function isMobileWidth()
        {
            return $window.innerWidth <= 600;
        }

        function compile(element, html, scope) {
            angular.element(element).append($compile(html)(scope));
        }

    }
})();
