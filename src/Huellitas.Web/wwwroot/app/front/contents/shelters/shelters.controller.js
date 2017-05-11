(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('SheltersController', SheltersController);

    SheltersController.$inject = ['$location', 'helperService', 'routingService', 'shelterService'];

    function SheltersController($location, helperService, routingService, shelterService) {
        var vm = this;

        vm.shelters = [];
        vm.hasNextPage = false;
        vm.filter = {
            page: 0,
            pageSize: 5,
            status: 'Published',
            orderby: 'CreatedDate',
            locationId: $location.search().locationId ? $location.search().locationId : undefined,
            keyword: $location.search().keyword ? $location.search().keyword : undefined,
            locationName: $location.search().locationName ? $location.search().locationName : undefined,
        };

        vm.changeLocation = changeLocation;
        vm.search = search;
        vm.more = more;

        activate();

        function activate() {
            getShelters();
        }

        function getShelters()
        {
            shelterService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                if (vm.filter.page > 0) {
                    vm.shelters = vm.shelters.concat(response.results);
                }
                else
                {
                    vm.shelters = response.results;
                }

                vm.hasNextPage = response.meta.hasNextPage;
            }        
        }

        function changeLocation(selectedLocation) {
            vm.filter.locationId = selectedLocation ? selectedLocation.originalObject.id : undefined;
            vm.filter.locationName = selectedLocation ? selectedLocation.originalObject.name : undefined;
        }

        function search()
        {
            $location.path(routingService.getRoute('shelters')).search({
                locationId: vm.filter.locationId,
                locationName: vm.filter.locationName,
                keyword: vm.filter.keyword
            });
        }

        function more()
        {
            vm.filter.page++;
            getShelters();
        }
    }
})();
