(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('PetDetailController', PetDetailController);

    PetDetailController.$inject = [
        '$routeParams',
        '$scope',
        'helperService',
        'petService',
        'contentService',
        'routingService',
        'modalService'];

    function PetDetailController(
        $routeParams,
        $scope,
        helperService,
        petService,
        contentService,
        routingService,
        modalService) {

        var vm = this;

        vm.friendlyName = $routeParams.friendlyName;
        vm.model = {};
        vm.filterSimilar = {};
        vm.titleSimilar = 'Mascotas similares';
        vm.activate = activatePet;
        vm.deactivate = deactivate;
        
        activate();

        function activate()
        {
            getPet();
            showWarningMessage();
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
                    excludeId: vm.model.id,
                    orderBy: 'Random',
                    genre: vm.model.genre.value,
                    withinClosingDate: true
                };

                $scope.$parent.root.seo.title = helperService.replaceJson(app.Settings.resources['Seo.PetDetail.Title'], { petName: vm.model.name, petLocation: vm.model.location.name, petSubtype: vm.model.subtype.text });
                $scope.$parent.root.seo.description = vm.model.body;
                $scope.$parent.root.seo.image = routingService.getFullRouteOfFile(vm.model.image.fileName);

                if (!vm.model.shelter)
                {
                    getContentUsers();
                }
            }
        }

        function showWarningMessage() {
            modalService.show({
                template: '/app/front/contents/pets/warningMessage.html?' + app.Settings.general.configJavascriptCacheKey
            });
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

        function activatePet() {
            switchStatus('Published');
        }

        function deactivate() {
            switchStatus('Created');
        }

        function switchStatus(status) {

            var message = status == 'Published' ? 'activar' : 'desactivar';

            if (confirm('¿Estas seguro de ' + message+' a '+ vm.model.name +'?')) {
                petService.changeStatus(vm.model.id, status)
                    .then(changeStatusCompleted)
                    .catch(helperService.handleException);
            }

            function changeStatusCompleted(response) {
                vm.model.status = status;
                if (status == 'Published') {
                    modalService.show({ message: 'Animal activado correctamente' });
                }
                else {
                    modalService.show({ message: 'Animal inactivado correctamente' });
                }
            }
        }
    }
})();
