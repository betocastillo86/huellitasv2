(function () {
    angular.module('app')
        .controller('ListPetsController', ListPetsController);

    ListPetsController.$inject = ['petService', 'shelterService'];

    function ListPetsController(petService, shelterService)
    {
        var vm = this;
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page : 0
        };
        vm.getPets = getPets;
        vm.pets = [];
        vm.pager = {};
        vm.changePage = changePage;

        activate();
        
        function activate() {
            getPets();
            getShelters();
        }

        function getPets() {
            return petService.getAll(vm.filter)
                .then(getPetsCompleted);

            function getPetsCompleted(data) {
                vm.pets = data.results;
                vm.pager = data.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
                return vm.pets;
            }
        }

        function getShelters() {
            return shelterService.getAll()
                .then(getSheltersCompleted);

            function getSheltersCompleted(data) {
                vm.shelters = data.results;
                return vm.shelters;
            }
        }

        function changePage(page)
        {
            vm.filter.page = page;
            return getPets();
        }
    }

})();