(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('ListAdoptionFormController', ListAdoptionFormController);

    ListAdoptionFormController.$inject = ['$location', '$scope', 'routingService', 'adoptionFormService', 'sessionService', 'helperService', 'adoptionFormStatusService'];

    function ListAdoptionFormController($location, $scope, routingService, adoptionFormService, sessionService, helperService, adoptionFormStatusService) {
        var vm = this;
        vm.forms = [];
        vm.statuses = [];
        vm.hasNextPage = false;
        vm.filter = {
            allRelatedToUserId: sessionService.getCurrentUser().id,
            page: 0,
            pageSize: 10,
            petId: $location.search().petId ? parseInt($location.search().petId) : undefined,
            petName: $location.search().petName ? $location.search().petName : undefined,
            username: $location.search().username ? $location.search().username : undefined,
            status: $location.search().status ? $location.search().status : undefined
        };

        vm.more = more;
        vm.search = search;
        vm.changePet = changePet;
        vm.goToForm = goToForm;

        activate();

        function activate() {

            $scope.$parent.root.seo.title = app.Settings.resources['Seo.AdoptionForms.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.AdoptionForms.Description'];

            getStatus();
        }

        function getStatus() {
            adoptionFormStatusService.getAll()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.statuses = response;
                getForms();
            }
        }

        function getForms() {
            adoptionFormService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                for (var i = 0; i < response.results.length; i++) {
                    response.results[i].statusName = _.findWhere(vm.statuses, { enum: response.results[i].status }).name;
                }

                if (vm.forms.length) {
                    vm.forms = vm.forms.concat(response.results);
                }
                else {
                    vm.forms = response.results;
                }

                vm.hasNextPage = response.meta.hasNextPage;
            }
        }

        function more() {
            vm.filter.page++;
            getForms();
        }

        function search() {
            $location.path(routingService.getRoute('forms')).search({
                username: vm.filter.username,
                petId: vm.filter.petId,
                status: vm.filter.status,
                petName: vm.filter.petName
            });
        }

        function changePet(selectedPet) {
            vm.filter.petId = selectedPet ? selectedPet.originalObject.id : undefined;
            vm.filter.petName = selectedPet ? selectedPet.originalObject.name : undefined;
            search();
        }

        function goToForm(id)
        {
            $location.path(routingService.getRoute('form', { id: id }));
        }
    }
})();