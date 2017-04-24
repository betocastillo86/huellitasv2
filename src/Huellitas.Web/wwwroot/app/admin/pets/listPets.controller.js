(function () {
    angular.module('huellitasAdmin')
        .controller('ListPetsController', ListPetsController);

    ListPetsController.$inject = ['petService', 'shelterService', 'helperService'];

    function ListPetsController(petService, shelterService, helperService)
    {
        var vm = this;
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0,
            orderBy: 'CreatedDate'
        };
        vm.getPets = getPets;
        vm.pets = [];
        vm.pager = {};
        vm.changePage = changePage;
        vm.shelterChanged = shelterChanged;

        activate();
        
        function activate() {
            getPets();
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

        function changePage(page)
        {
            vm.filter.page = page;
            return getPets();
        }

        function shelterChanged(selected)
        {
            vm.filter.shelter = selected ? selected.originalObject.id : undefined;
            changePage(0);
        }
    }

})();