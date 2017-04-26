(function () {
    angular.module('huellitasAdmin')
        .controller('ListSheltersController', ListSheltersController);

    ListSheltersController.$inject = ['shelterService', 'helperService'];

    function ListSheltersController(shelterService, helperService)
    {
        var vm = this;
        vm.shelters = [];
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0,
            orderBy: 'CreatedDate'
        };
        vm.pager = {};
        vm.changePage = changePage;
        vm.getShelters = getShelters;
        vm.changeFilterLocation = changeFilterLocation;

        active();

        return vm;

        function active()
        {
            getShelters();
        }

        function getShelters()
        {
            shelterService.getAll(vm.filter)
                .then(getSheltersCompleted)
                .catch(helperService.handleException);

            function getSheltersCompleted(response)
            {
                vm.shelters = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            return getShelters();
        }

        function changeFilterLocation(selected) {
            vm.filter.locationid = selected ? selected.originalObject.id : selected;
            return getShelters();
        }
    }
})();