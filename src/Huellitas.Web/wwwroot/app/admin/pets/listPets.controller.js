(function () {
    angular.module('huellitasAdmin')
        .controller('ListPetsController', ListPetsController);

    ListPetsController.$inject = ['petService', 'shelterService', 'helperService', 'statusTypeService'];

    function ListPetsController(petService, shelterService, helperService, statusTypeService )
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
        vm.statusTypes = [];
        vm.pager = {};
        vm.changePage = changePage;
        vm.shelterChanged = shelterChanged;
        vm.filterByPet = filterByPet;
        vm.filterByStatus = filterByStatus;

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
    }

})();