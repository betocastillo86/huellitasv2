(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('PetsController', PetsController);

    PetsController.$inject = [
        '$location',
        '$scope',
        'routingService',
        'petService',
        'helperService'];

    function PetsController(
        $location,
        $scope,
        routingService,
        petService,
        helperService) {

        var vm = this;
        vm.pets = [];
        vm.areAllrescuersPetsLoaded = false;

        vm.filter = {
            pageSize: 6,
            page: 0,
            status: 'Published',
            orderBy: 'Featured',
            size: $location.search().size ? parseInt($location.search().size) : undefined,
            genre: $location.search().genre ? parseInt($location.search().genre) : undefined,
            age: $location.search().age,
            subtype: $location.search().subtype ? parseInt($location.search().subtype) : undefined,
            shelter: $location.search().shelter ? parseInt($location.search().shelter) : undefined,
            keyword: $location.search().keyword,
            locationId: $location.search().locationId,
            locationName: $location.search().locationName,
            contentType: 'Pet',
            withinClosingDate: true,
            onlyRescuers: false
        };

        vm.filterRescuers = {};
        _.defaults(vm.filterRescuers, vm.filter)
        vm.filterRescuers.onlyRescuers = true;
        vm.filterRescuers.pageSize = 6;

        vm.genres = app.Settings.genres;
        vm.sizes = app.Settings.sizes;
        vm.subtypes = app.Settings.subtypes;
        vm.ages = [{ id: '0-11', value: 'Menos de un año' }, { id: '12-35', value: 'De 1 a 2 años' }, { id: '36-59', value: 'De 3 a 4 años' }, { id: '60-', value: 'Más de 5 años' }];

        vm.pagingEnabled = true;

        vm.search = search;
        vm.isSubtypeChecked = isSubtypeChecked;
        vm.changeLocation = changeLocation;
        vm.nextPage = nextPage;

        activate();

        function activate()
        {
            $scope.$parent.root.seo.title = app.Settings.resources['Seo.Pets.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.Pets.Description'];
            $scope.$parent.root.seo.image = routingService.getFullRouteOfFile('img/front/compartir-fb-publicar.png');

           getPets();
        }

        function search()
        {
            $location.path(routingService.getRoute('pets')).search({
                size: vm.filter.size,
                genre: vm.filter.genre,
                age: vm.filter.age,
                subtype: vm.filter.subtype,
                keyword: vm.filter.keyword,
                shelter: vm.filter.shelter,
                locationId: vm.filter.locationId,
                locationName: vm.filter.locationName
            });
        }

        function getPets() {
            petService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                //response = addAdsToResponse(response);
                vm.pets = !vm.pets.length ? response.results : vm.pets.concat(response.results);
                vm.filter.hasNextPage = response.meta.hasNextPage;
                vm.filter.totalCount = response.meta.totalCount;
                getRescuerPets();
            }
        }

        function getRescuerPets() {
            if (!vm.areAllrescuersPetsLoaded) {
                petService.getAll(vm.filterRescuers)
                    .then(getCompleted)
                    .catch(helperService.handleException);
            }
            else {
                vm.filter.pageSize = 9;
            }

            function getCompleted(response) {
                vm.pets = vm.pets.concat(response.results);
                vm.pets.push({ isAd: true });
                vm.areAllrescuersPetsLoaded = !response.meta.hasNextPage;
                // Siempre tiene en cuenta si hay siguiente pagina dependiendo del que tenga más resultados.
                vm.filter.hasNextPage = vm.filter.totalCount < response.totalCount ? response.meta.hasNextPage : vm.filter.hasNextPage;
            }
        }

        //function addAdsToResponse(response) {
        //    if (response.results.length >= vm.filter.pageSize) {
        //        response.results.splice(vm.filter.pageSize, 0, { isAd: true })
        //    }
        //    else {
        //        response.results.push({ isAd: true });
        //    }
        //    return response;
        //}

        function nextPage() {
            vm.filter.page++;
            vm.filterRescuers.page++;
            getPets();
        }

        function isSubtypeChecked(index)
        {
            return vm.filter.subtype == vm.subtypes[index].id;
        }

        function changeLocation(selectedLocation)
        {
            if (selectedLocation)
            {
                vm.filter.locationId = selectedLocation.originalObject.id;
                vm.filter.locationName = selectedLocation.originalObject.name;
                search();
            }
        }
    }
})();
