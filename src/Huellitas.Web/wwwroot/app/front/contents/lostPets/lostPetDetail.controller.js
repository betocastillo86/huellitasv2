(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('LostPetDetailController', LostPetDetailController);

    LostPetDetailController.$inject = ['$routeParams', '$scope', 'helperService', 'petService', 'contentService', 'routingService'];

    function LostPetDetailController($routeParams, $scope, helperService, petService, contentService, routingService) {
        var vm = this;

        vm.friendlyName = $routeParams.friendlyName;
        vm.model = {};
        vm.filterSimilar = {};
        vm.titleSimilar = 'Mascotas similares';

        activate();

        function activate() {
            getPet();
        }

        function getPet() {
            petService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.notFound);

            function getCompleted(response) {
                vm.model = response;

                vm.filterSimilar = {
                    pageSize: 4,
                    status: 'Published',
                    subtype: vm.model.subtype.value,
                    size: vm.model.size.value,
                    contentType: 'Pet'
                };

                $scope.$parent.root.seo.title = helperService.replaceJson(app.Settings.resources['Seo.LostPetDetail.Title'], { petName: vm.model.name, petLocation: vm.model.location.name, petSubtype: vm.model.subtype.text });
                $scope.$parent.root.seo.description = vm.model.body;
                $scope.$parent.root.seo.image = routingService.getFullRouteOfFile(vm.model.image.fileName);

                getContentUsers();
            }
        }

        function getContentUsers() {
            contentService.getUsers(vm.model.id, { relationType: 'Parent' })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.model.parents = response.results;
            }
        }
    }
})();
