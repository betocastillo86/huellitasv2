(function () {
    angular.module('huellitasAdmin')
        .controller('ListFormController', ListFormController);

    ListFormController.$inject = ['adoptionFormService', 'adoptionFormStatusService'];

    function ListFormController(adoptionFormService, adoptionFormStatusService) {
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
            .catch(getAllError);

            function getAllCompleted(response)
            {
                vm.forms = response.data.results;
                vm.pager = response.data.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }

            function getAllError()
            {
                console.log('Error con formularios');
            }
        }

        function getStatus()
        {
            adoptionFormStatusService.getAll()
                .then(getAllCompleted)
                .catch(getAllError);

            function getAllCompleted(response)
            {
                vm.listStatus = response.data;
            }

            function getAllError()
            {
                console.log('Error en los estados');
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