(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('SheltersController', SheltersController);

    SheltersController.$inject = ['$location', '$scope', 'helperService', 'routingService', 'shelterService'];

    function SheltersController($location, $scope, helperService, routingService, shelterService) {
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

            $scope.$parent.root.seo.title = app.Settings.resources['Seo.Shelters.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.Shelters.Description'];
            $scope.$parent.root.seo.image = routingService.getFullRouteOfFile('img/front/compartir-fb-registrar-fundacion.png');
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
