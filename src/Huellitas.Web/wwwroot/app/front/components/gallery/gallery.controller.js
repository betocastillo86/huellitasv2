(function () {

    angular.module('huellitas')
        .controller('GalleryController', GalleryController);

    GalleryController.$inject = ['$attrs', '$scope', '$interval'];

    function GalleryController($attrs, $scope, $interval) {
        var vm = this;
        vm.images = [];
        vm.currentImage = {};
        vm.iImage = 0;

        vm.next = next;
        vm.back = back;
        vm.select = select;

        activate();

        function activate() {
            vm.images = $scope.images;
            rotateImages();
        }

        var interval = undefined;
        function rotateImages() {
            if (vm.images.length) {
                vm.currentImage = vm.images[vm.iImage];

                if (vm.images.length > 1) {
                    //vm.iImage = 0;

                    if (interval) {
                        $interval.cancel(interval);
                    }

                    interval = $interval(function () {
                        if (vm.iImage + 1 == vm.images.length) {
                            vm.iImage = 0;
                        }
                        else {
                            vm.iImage++;
                        }

                        vm.currentImage = vm.images[vm.iImage];
                    }, 10000);
                }
            }
        }

        function next() {
            vm.iImage = vm.iImage + 1 == vm.images.length ? 0 : vm.iImage + 1;
            rotateImages();
        }

        function back() {
            vm.iImage = vm.iImage == 0 ? vm.images.length - 1 : vm.iImage - 1;
            rotateImages();
        }

        function select(index)
        {
            vm.iImage = index;
            rotateImages();
        }
    }

})();
