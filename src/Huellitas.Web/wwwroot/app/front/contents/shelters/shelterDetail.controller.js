
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('ShelterDetailController', ShelterDetailController);

    ShelterDetailController.$inject = ['$routeParams', '$scope', 'shelterService', 'helperService', 'routingService'];

    function ShelterDetailController($routeParams, $scope, shelterService, helperService, routingService) {
        var vm = this;
        vm.model = {};
        vm.friendlyName = $routeParams.friendlyName;

        activate();

        function activate()
        {
            getShelter();
        }

        function getShelter()
        {
            shelterService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.notFound);

            function getCompleted(response)
            {
                vm.model = response;

                vm.filterMyPets = {
                    page: 0,
                    pageSize: 6,
                    shelter: vm.model.id,
                    status: 'Published',
                    contentType: 'Pet'
                };

                $scope.$parent.root.seo.title = helperService.replaceJson(app.Settings.resources['Seo.ShelterDetail.Title'], { shelterName: vm.model.name, shelterLocation: vm.model.location.name });
                $scope.$parent.root.seo.description = vm.model.body;
                $scope.$parent.root.seo.image = routingService.getFullRouteOfFile(vm.model.image.fileName);
            }

        }
    }
})();
