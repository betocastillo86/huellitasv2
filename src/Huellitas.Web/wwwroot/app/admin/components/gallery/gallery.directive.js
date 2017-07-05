(function () {
    angular.module('huellitasAdmin')
        .directive('galleryHuellitas', galleryHuellitas);

    function galleryHuellitas() {
        return {
            restrict: 'E',
            templateUrl: '/app/admin/components/gallery/gallery.html?' + app.Settings.general.configJavascriptCacheKey,
            controller: 'GalleryController',
            controllerAs: 'gallery',
            scope: {
                images: '=',
                title: '@',
                ondelete: '=',
                onadded: '=',
                defaultname: '@',
                width: '@',
                height: '@',
                saveonreorder: '=',
                contentid: '=',
                socialpost: '@'
            }
        };
    }
})();