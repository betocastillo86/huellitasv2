(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('PetDetailController', PetDetailController);

    PetDetailController.$inject = ['$routeParams','$scope', 'helperService', 'petService', 'contentService', 'routingService'];

    function PetDetailController($routeParams, $scope, helperService, petService, contentService, routingService) {
        var vm = this;

        vm.friendlyName = $routeParams.friendlyName;
        vm.model = {};
        vm.filterSimilar = {};
        vm.titleSimilar = 'Mascotas similares';
        
        activate();

        function activate()
        {
            getPet();
        }

        function getPet()
        {
            petService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.notFound);

            function getCompleted(response)
            {
                vm.model = response;
                vm.model.fullRoute = routingService.getFullRoute('pet', { friendlyName: vm.model.friendlyName });

                vm.filterSimilar = {
                    pageSize: 4,
                    status: 'Published',
                    subtype: vm.model.subtype.value,
                    size: vm.model.size.value,
                    contentType: 'Pet',
                    excludeId: vm.model.id
                };

                $scope.$parent.root.seo.title = helperService.replaceJson(app.Settings.resources['Seo.PetDetail.Title'], { petName: vm.model.name, petLocation: vm.model.location.name, petSubtype: vm.model.subtype.name });
                $scope.$parent.root.seo.description = vm.model.body;
                $scope.$parent.root.seo.image = routingService.getFullRouteOfFile(vm.model.image.fileName);

                if (!vm.model.shelter)
                {
                    getContentUsers();
                }
            }
        }

        function getContentUsers()
        {
            contentService.getUsers(vm.model.id, { relationType: 'Parent' })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.model.parents = response.results;
            }
        }
    }
})();
