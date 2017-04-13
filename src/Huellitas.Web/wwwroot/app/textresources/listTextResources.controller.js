(function () {
    angular.module('app')
        .controller('ListTextResourceController', ListTextResourceController);

    ListTextResourceController.$inject = ['textResourceService', 'modalService'];

    function ListTextResourceController(textResourceService, modalService) {
        var vm = this;
        vm.resources = [];
        vm.isSending = false;

        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        }
        vm.pager = {};

        vm.changePage = changePage;
        vm.getResources = getResources;
        vm.toggleEdit = toggleEdit;

        return activate();

        function activate() {
            getResources();
        }

        function getResources() {
            textResourceService.get(vm.filter)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response) {
                vm.resources = response.data.results;
                vm.pager = response.data.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }

            function getError(response) {
                console.log('Error cargando');
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            getResources();
        }

        function updateResource(model) {
            textResourceService.put(model)
                .then(putCompleted)
                .catch(putError);

            function putCompleted(response) {
                vm.isSending = false;
                modalService.show({ message: 'Llave guardada correctamente' });
            }

            function putError(response) {
                vm.isSending = false;
                modalService.showError({ error: response.data.error });
            }
        }

        function toggleEdit(resource) {
            if (resource.isEditing) {
                if (resource.value && resource.value.length && !vm.isSending) {
                    vm.isSending = true;
                    updateResource(resource);
                }
                else {
                    modalService.show({ message: 'El valor no puede ser vacio' });
                }
            }

            resource.isEditing = !resource.isEditing;
        }
    }
})();