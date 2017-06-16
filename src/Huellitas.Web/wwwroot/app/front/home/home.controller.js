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
        vm.iCurrentBanner = 0;

        vm.filterPets = {
            pageSize: 9,
            orderBy: 'Featured',
            status: 'Published',
            contentType: 'Pet',
            withinClosingDate: true
        };

        vm.previousBanner = previousBanner;
        vm.nextBanner = nextBanner;

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
        }

        var interval = undefined;
        function rotateBanners() {
            if (vm.banners.length) {
                vm.currentBanner = vm.banners[vm.iCurrentBanner];

                if (vm.banners.length > 1) {
                    //vm.iCurrentBanner = 0;

                    if (interval) {
                        $interval.cancel(interval);
                    }

                    interval = $interval(function () {
                        if (vm.iCurrentBanner + 1 == vm.banners.length) {
                            vm.iCurrentBanner = 0;
                        }
                        else {
                            vm.iCurrentBanner++;
                        }

                        vm.currentBanner = vm.banners[vm.iCurrentBanner];
                    }, 5000);
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

        function previousBanner()
        {
            vm.iCurrentBanner = vm.iCurrentBanner == 0 ? vm.banners.length - 1 : vm.iCurrentBanner - 1;
            rotateBanners();
        }

        function nextBanner()
        {
            vm.iCurrentBanner = vm.iCurrentBanner + 1 == vm.banners.length ? 0 : vm.iCurrentBanner + 1;
            rotateBanners();
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
