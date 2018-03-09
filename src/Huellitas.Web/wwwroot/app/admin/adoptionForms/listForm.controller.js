(function () {
    angular.module('huellitasAdmin')
        .controller('ListFormController', ListFormController);

    ListFormController.$inject = ['$scope', 'adoptionFormService', 'adoptionFormStatusService', 'helperService', 'modalService', 'adoptionFormAnswerService'];

    function ListFormController($scope, adoptionFormService, adoptionFormStatusService, helperService, modalService, adoptionFormAnswerService) {
        var vm = this;
        vm.forms = [];
        vm.listStatus = [];
        vm.toResponse = [];
        vm.toogleToResponse = toogleToResponse;
        
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        };
        vm.pager = {};

        vm.changePage = changePage;
        vm.getForms = getForms;
        vm.shelterChanged = shelterChanged;
        vm.petChanged = petChanged;
        vm.filterByPet = filterByPet;
        vm.filterByStatus = filterByStatus;
        vm.filterByUser = filterByUser;
        vm.filterByPetStatus = filterByPetStatus;
        vm.removeFilterByPet = removeFilterByPet;
        vm.toogleAll = toogleAll;
        vm.responseAdopted = responseAdopted;

        return activate();

        function activate() {
            getForms();
            getStatus();
            return vm;
        }

        function getForms()
        {
            adoptionFormService.getAll(vm.filter)
            .then(getAllCompleted)
            .catch(helperService.handleException);

            function getAllCompleted(response)
            {
                vm.forms = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }
        }

        function getStatus()
        {
            adoptionFormStatusService.getAll()
                .then(getAllCompleted)
                .catch(helperService.handleException);

            function getAllCompleted(response)
            {
                vm.listStatus = response;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            vm.toResponse = [];
            vm.allSelected = false;
            getForms();
        }

        function shelterChanged(selected)
        {
            vm.filter.shelterid = selected ? selected.originalObject.id : undefined;
            changePage(0);
        }

        function petChanged(selected) {
            vm.filter.petid = selected ? selected.originalObject.id : undefined;
            changePage(0);
        }

        function filterByPet(form)
        {
            vm.filter.petId = form.content.id;
            vm.filter.petName = form.content.name;
            changePage(0);
        }

        function removeFilterByPet()
        {
            vm.filter.petId = undefined;
            vm.filter.petName = undefined;
            $scope.$broadcast('angucomplete-alt:clearInput', 'petName');
            changePage(0);
        }

        function filterByUser(form)
        {
            vm.filter.userName = form.name;
            changePage(0);
        }

        function filterByStatus(form) {
            vm.filter.status = form.status;
            changePage(0);
        }

        function filterByPetStatus(form) {
            vm.filter.petStatus = form.status;
            changePage(0);
        }

        function toogleToResponse(form)
        {
            var position = vm.toResponse.indexOf(form.id);

            if (position > -1) {
                vm.toResponse.splice(position, 1);
                vm.allSelected = false;
            }
            else
            {
                vm.toResponse.push(form.id);
            }
        }

        function toogleAll(selected)
        {
            if (selected)
            {
                vm.toResponse = _.map(vm.forms, function (form) { form.checked = true; return form.id });
            }
            else
            {
                _.each(vm.forms, function (form) { form.checked = false; });
                vm.toResponse = [];
            }
        }

        function responseAdopted()
        {
            var sentAnswers = 0;

            if (vm.toResponse.length > 0) {
                if (confirm('¿Está seguro de responder esos formularios?'))
                {
                    for (var i = 0; i < vm.toResponse.length; i++) {
                        var formId = vm.toResponse[i];
                        adoptionFormAnswerService.post({ adoptionFormId: formId, status: 'AlreadyAdopted' })
                            .then(postAnswerCompleted)
                            .catch(helperService.handleException);
                    }
                }
            }
            else
            {
                modalService.showError({message: 'Selecciona al menos un registro'});
            }

            function postAnswerCompleted(response)
            {
                sentAnswers++;
                if (vm.toResponse.length == sentAnswers)
                {
                    modalService.show({message:'Se han respondido todos los formularios correctamente'});
                }
            }
        }
    }
})();