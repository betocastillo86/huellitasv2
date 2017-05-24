(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$interval', '$scope', '$window', '$document', 'bannerService', 'helperService', 'shelterService', 'routingService'];

    function HomeController($interval, $scope, $window, $document, bannerService, helperService, shelterService, routingService) {
        var vm = this;
        vm.banners = [];
        vm.shelters = [];

        vm.petsLoaded = petsLoaded;

        vm.currentBanner = undefined;
        vm.filterPets = {
            pageSize: 9,
            orderBy: 'Featured',
            status: 'Published',
            contentType: 'Pet'
        };

        activate();

        function activate() {
            $scope.$parent.root.transparentHeader = true;

            $scope.$parent.root.seo.title = app.Settings.resources['Seo.Home.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.Home.Description'];
            $scope.$parent.root.seo.image = routingService.getFullRouteOfFile(app.Settings.general.seoImage);

            getShelters();
            getBanners();
            attachScrollEvent();
        }

        function getBanners(response) {
            bannerService.getAll({ active: true, section: 'Home' })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.banners = response.results;

                addPetToBanner();

                rotateBanners();
            }

            function rotateBanners() {
                if (vm.banners.length) {
                    vm.currentBanner = vm.banners[0];

                    if (vm.banners.length > 1) {
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

        function attachScrollEvent()
        {
            if (!helperService.isMobileWidth())
            {
                $document[0].getElementsByTagName('header')[0].className = '';

                $window.addEventListener('scroll', function (e) {
                    $document[0].getElementsByTagName('header')[0].className = ($window.scrollY > 100 ? 'inner-header' : '');
                });
            }
        }

        function getShelters() {
            var filter = {
                pageSize: 4,
                orderBy: 'DisplayOrder',
                status: 'Published'
            };

            shelterService.getAll(filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.shelters = response.results;
            }
        }

        function petsLoaded(pets) {
            vm.featuredPet = _.findWhere(pets.results, { featured: true });

            if (vm.banners.length) {
                addPetToBanner();
            }
        }

        function addPetToBanner()
        {
            if (vm.featuredPet)
            {
                vm.banners.push({ isPet: true });
            }
        }
    }
})();
