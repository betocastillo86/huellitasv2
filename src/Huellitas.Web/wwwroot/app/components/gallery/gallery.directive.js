(function () {
    angular.module('app')
        .directive('galleryHuellitas', galleryHuellitas);

    function galleryHuellitas() {
        return {
            restrict: 'E',
            templateUrl: 'app/components/gallery.gallery.html',
            controller: 'GalleryController',
            controllerAs: 'gallery'
        };
    }
})();