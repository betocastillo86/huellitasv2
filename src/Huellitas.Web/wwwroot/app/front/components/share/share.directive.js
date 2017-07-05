(function () {
    'use strict';

    angular
        .module('huellitas')
        .directive('shareHuellitas', shareHuellitas);

    shareHuellitas.$inject = ['modalService'];

    function shareHuellitas(modalService) {
        var directive = {
            link: link,
            restrict: 'A',
            scope: {
                url: '='
            }
        };

        function link(scope, element, attrs) {
            element.on('click', function () {
                modalService.show({
                    controller: 'ShareController',
                    template: '/app/front/components/share/share.html?' + app.Settings.general.configJavascriptCacheKey,
                    params: { url: scope.url }
                });
            });
        }

        return directive;
    }
})();

