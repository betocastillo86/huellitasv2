(function () {
    angular.module('huellitasAdmin')
        .controller('ListLostPetsController', ListLostPetsController);

    ListLostPetsController.$inject = ['petService', 'helperService'];

    function ListLostPetsController(petService, helperService)
    {
        var vm = this;
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0,
            orderBy: 'CreatedDate',
            contentType:'LostPet'
        };
        vm.getPets = getPets;
        vm.pets = [];
        vm.pager = {};
        vm.changePage = changePage;

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

    }

})();