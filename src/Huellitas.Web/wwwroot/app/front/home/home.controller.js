(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$interval','bannerService', 'helperService'];

    function HomeController($interval, bannerService, helperService) {
        var vm = this;
        vm.banners = [];
        vm.currentBanner = undefined;

        activate();

        function activate() {
            getBanners();
        }

        function getBanners(response)
        {
            bannerService.getAll({ active: true, section: 'Home' })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.banners = response.results;

                rotateBanners();
            }

            function rotateBanners()
            {
                if (vm.banners.length)
                {
                    vm.currentBanner = vm.banners[0];

                    if (vm.banners.length > 1)
                    {
                        var iBanner = 0;

                        $interval(function () {
                            if (iBanner + 1 == vm.banners.length) {
                                iBanner = 0;
                            }
                            else {
                                iBanner++;
                            }

                            vm.currentBanner = vm.banners[iBanner];
                        }, 5000);
                    }
                }
            }
        }
    }
})();
