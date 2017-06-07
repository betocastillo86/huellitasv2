
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('LostPetsController', LostPetsController);

    LostPetsController.$inject = ['$location', '$scope', 'helperService',  'petService', 'routingService'];

    function LostPetsController($location, $scope, helperService, petService, routingService) {
        var vm = this;
        vm.pets = [];
        vm.filter = {
            contentType: 'LostPet',
            orderBy: 'CreatedDate',
            status: 'Published',
            page: 0,
            pageSize: 12,
            size: $location.search().size ? parseInt($location.search().size) : undefined,
            genre: $location.search().genre ? parseInt($location.search().genre) : undefined,
            subtype: $location.search().subtype ? parseInt($location.search().subtype) : undefined,
            locationId: $location.search().locationId ? parseInt($location.search().locationId) : undefined,
            locationName: $location.search().locationName,
            breed: $location.search().breed ? parseInt($location.search().breed) : undefined,
            breedName: $location.search().breedName,
            keyword: $location.search().keyword,
            fromStartingDate: $location.search().fromStartingDate
        };

        vm.genres = app.Settings.genres;
        vm.sizes = app.Settings.sizes;
        vm.subtypes = app.Settings.subtypes;
        vm.breedTable = app.Settings.customTables.breed;

        vm.hasNextPage = false;
        vm.pagingEnabled = true;
        vm.changeLocation = changeLocation;
        vm.search = search;
        vm.isSubtypeChecked = isSubtypeChecked;
        vm.changeBreed = changeBreed;

        activate();

        function activate()
        {
            $scope.$parent.root.seo.title = app.Settings.resources['Seo.LostPets.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.LostPets.Description'];
        }

        function search()
        {
            $location.path(routingService.getRoute('lostpets')).search({
                size: vm.filter.size,
                genre: vm.filter.genre,
                subtype: vm.filter.subtype,
                locationId: vm.filter.locationId,
                locationName: vm.filter.locationName,
                keyword: vm.filter.keyword,
                fromStartingDate: vm.filter.fromStartingDate,
                breed: vm.filter.breed,
                breedName: vm.filter.breedName
            });
        }

        function changeLocation(selectedLocation)
        {
            vm.filter.locationId = selectedLocation ? selectedLocation.originalObject.id : undefined;
            vm.filter.locationName = selectedLocation ? selectedLocation.originalObject.name : undefined;
        }

        function changeBreed(selected) {
            vm.filter.breed = selected ? selected.originalObject.id : undefined;
            vm.filter.breedName = selected ? selected.originalObject.value : undefined;
        }

        function isSubtypeChecked(index) {
            return vm.filter.subtype == vm.subtypes[index].id;
        }
    }
})();
