(function () {
    'use strict';

    angular
        .module('huellitas')
        .directive('galleryHuellitas', galleryHuellitas);

    function galleryHuellitas() {
        return {
            scope: {
                images : '='
            },
            templateUrl: '/app/front/components/gallery/gallery.html',
            controller: 'GalleryController',
            controllerAs: 'gallery',
            //bindToController: true,
            restrict: 'E'
        };
    }
})();

