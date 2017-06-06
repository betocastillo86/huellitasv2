(function () {
    'use strict';

    angular
        .module('huellitasServices')
        .factory('helperService', helperService);

    helperService.$inject = [
        '$window',
        '$location',
        '$compile',
        'modalService',
        'routingService'];

    function helperService($window, $location, $compile, modalService, routingService) {
        var service = {
            configServiceUrl: configServiceUrl,
            handleException: handleException,
            isMobileWidth: isMobileWidth,
            goToFocus: goToFocus,
            goToFocusError: goToFocusError,
            compile: compile,
            notFound: notFound,
            replaceJson: replaceJson,
            enableLeavingPageMode: enableLeavingPageMode
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

        function enableLeavingPageMode($scope, $window)
        {
            if ($window.addEventListener) {
                $window.addEventListener("beforeunload", handleUnloadEvent);
            } else {
                //For IE browsers
                $window.attachEvent("onbeforeunload", handleUnloadEvent);
            }

            var offEvent = $scope.$on('$locationChangeStart', function (event) {
                var answer = confirm("¿Seguro deseas dejar esta página sin terminar el proceso?");
                if (!answer) {
                    event.preventDefault();
                }
                else
                {
                    disableEvent();
                }
            });

            var disableEvent = function()
            {
                if ($window.removeEventListener) {
                    $window.removeEventListener("beforeunload", handleUnloadEvent);
                } else {
                    $window.detachEvent("onbeforeunload", handleUnloadEvent);
                }

                if (offEvent) {
                    offEvent()
                }
            };

            return disableEvent;
        }

        

        function handleUnloadEvent(event) {
            event.returnValue = "Your warning text";
        };

        function isMobileWidth() {
            return $window.innerWidth <= 600;
        }

        function goToFocus(selector, addPixels) {
            selector = selector || '.error';

            var obj = $(selector);
            if (!obj.length)
                return;
            if (addPixels == undefined)
                addPixels = 0;

            var position = 0;
            if (obj.offset() != undefined)
                position = obj.offset().top;
            $('html, body').animate({
                scrollTop: position + addPixels
            }, 500);
        }

        function notFound()
        {
            $location.path(routingService.getRoute('notfound'));
        }

        function goToFocusError() {
            goToFocus('.error', -100);
        }

        function compile(element, html, scope) {
            angular.element(element).append($compile(html)(scope));
        }

        function replaceJson(originalString, jsonReplace)
        {
            var keys = _.keys(jsonReplace);
            for (var i = 0; i < keys.length; i++) {
                var key = keys[i];
                originalString = originalString.replace(new RegExp('{' + key + '}', 'gi'), jsonReplace[key]);
            }
            return originalString;
        }
    }
})();