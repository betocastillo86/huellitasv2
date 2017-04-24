(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$interval','bannerService', 'helperService', 'shelterService'];

    function HomeController($interval, bannerService, helperService, shelterService) {
        var vm = this;
        vm.banners = [];
        vm.shelters = [];
        vm.currentBanner = undefined;
        vm.filterPets = {
            pageSize: 9,
            orderBy: 'CreatedDate',
            status: 'Published'
        };

        activate();

        function activate() {
            getBanners();
            getShelters();
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

        function getShelters()
        {
            var filter = {
                pageSize: 4,
                orderBy: 'DisplayOrder',
                status: 'Published'
            };

            shelterService.getAll(filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.shelters = response.results;
            }
        }
    }
})();
