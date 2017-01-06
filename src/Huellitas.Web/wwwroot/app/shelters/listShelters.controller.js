(function () {
    angular.module('app')
        .controller('ListSheltersController', ListSheltersController);

    ListSheltersController.$inject = ['shelterService'];

    function ListSheltersController(shelterService)
    {
        var vm = this;
        vm.shelters = [];
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
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
                .catch(getSheltersError);

            function getSheltersCompleted(response)
            {
                vm.shelters = response.data.results;
                vm.pager = response.data.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }

            function getSheltersError()
            {
                console.log('Error cargando los contenidos');
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