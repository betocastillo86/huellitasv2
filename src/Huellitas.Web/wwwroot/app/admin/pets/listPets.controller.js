(function () {
    angular.module('huellitasAdmin')
        .controller('ListPetsController', ListPetsController);

    ListPetsController.$inject = [
        'petService',
        'shelterService',
        'helperService',
        'statusTypeService',
        'modalService',
        'crawlingService'];

    function ListPetsController(
        petService,
        shelterService,
        helperService,
        statusTypeService,
        modalService,
        crawlingService)
    {
        var vm = this;
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0,
            orderBy: 'CreatedDate',
            contentType:'Pet'
        };
        vm.getPets = getPets;
        vm.pets = [];
        vm.toResponse = [];
        vm.toogleToResponse = toogleToResponse;
        vm.statusTypes = [];
        vm.pager = {};
        vm.changePage = changePage;
        vm.shelterChanged = shelterChanged;
        vm.filterByPet = filterByPet;
        vm.filterByStatus = filterByStatus;
        vm.toogleAll = toogleAll;
        vm.approveAll = approveAll;

        activate();
        
        function activate() {
            getPets();
            getStatusTypes();
        }

        return vm;

        function getPets() {
            return petService.getAll(vm.filter)
                .then(getPetsCompleted)
                .catch(helperService.handleException);

            function getPetsCompleted(data) {
                vm.pets = data.results;
                vm.pager = data.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
                return vm.pets;
            }
        }

        function getStatusTypes() {
            statusTypeService.getAll()
                .then(statusTypesCompleted)
                .catch(helperService.handleException);;

            function statusTypesCompleted(rows) {
                vm.statusTypes = rows;
            }
        }

        function toogleAll(selected) {
            if (selected) {
                vm.toResponse = _.map(vm.pets, function (el) { el.checked = true; return el.id });
            }
            else {
                _.each(vm.pets, function (el) { el.checked = false; });
                vm.toResponse = [];
            }
        }

        function toogleToResponse(pet) {
            var position = vm.toResponse.indexOf(pet.id);

            if (position > -1) {
                vm.toResponse.splice(position, 1);
                vm.allSelected = false;
            }
            else {
                vm.toResponse.push(pet.id);
            }
        }

        function changePage(page)
        {
            vm.filter.page = page;
            return getPets();
        }

        function filterByPet(pet)
        {
            vm.filter.page = 0;
            vm.filter.keyword = pet.name;
            getPets();
        }

        function filterByStatus(pet) {
            vm.filter.page = 0;
            vm.filter.status = pet.status;
            getPets();
        }

        function shelterChanged(selected)
        {
            vm.filter.shelter = selected ? selected.originalObject.id : undefined;
            changePage(0);
        }

        function crawlPet(petId) {
            var pet = _.findWhere(vm.pets, { id: petId });
            if (pet) {
                crawlingService.openCrawlingWindow('pet', { friendlyName: pet.friendlyName }, undefined, 10000);
            }
        }

        function approveAll() {
            var sentAnswers = 0;

            if (vm.toResponse.length > 0) {
                if (confirm('¿Está seguro de aprobar esos formularios?')) {
                    for (var i = 0; i < vm.toResponse.length; i++) {
                        var petId = vm.toResponse[i];
                        petService.changeStatus(petId, 'Published')
                            .then(approveCompleted)
                            .catch(helperService.handleException);

                        crawlPet(petId);
                    }
                }
            }
            else {
                modalService.showError({ message: 'Selecciona al menos un registro' });
            }

            function approveCompleted(response) {
                sentAnswers++;
                if (vm.toResponse.length == sentAnswers) {
                    modalService.show({ message: 'Se han aprobado todos los animales' });
                }
            }
        }
    }

})();