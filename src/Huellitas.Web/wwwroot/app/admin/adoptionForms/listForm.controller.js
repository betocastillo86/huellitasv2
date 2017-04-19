(function () {
    angular.module('huellitasAdmin')
        .controller('ListFormController', ListFormController);

    ListFormController.$inject = ['adoptionFormService', 'adoptionFormStatusService', 'helperService'];

    function ListFormController(adoptionFormService, adoptionFormStatusService, helperService) {
        var vm = this;
        vm.forms = [];
        vm.listStatus = [];
        
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        };
        vm.pager = {};

        vm.changePage = changePage;
        vm.getForms = getForms;
        vm.shelterChanged = shelterChanged;
        vm.petChanged = petChanged;

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
    }
})();