(function () {
    angular.module('app')
        .controller('ListSystemSettingController', ListSystemSettingController);

    ListSystemSettingController.$inject = ['systemSettingService', 'modalService'];

    function ListSystemSettingController(systemSettingService, modalService) {
        var vm = this;
        vm.settings = [];

        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        }
        vm.pager = {};

        vm.changePage = changePage;
        vm.getSettings = getSettings;
        vm.toggleEdit = toggleEdit;

        return activate();

        function activate() {
            getSettings();
        }

        function getSettings() {
            systemSettingService.get(vm.filter)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response) {
                vm.settings = response.data.results;
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
            getSettings();
        }

        function updateSetting(model) {
            systemSettingService.put(model)
                .then(putCompleted)
                .catch(putError);

            function putCompleted(response) {
                modalService.show({ message: 'Llave guardada correctamente' });
            }

            function putError(response) {
                modalService.showError({ error: response.data.error });
            }
        }

        function toggleEdit(setting) {
            if (setting.isEditing) {
                if (setting.value && setting.value.length) {
                    updateSetting(setting);
                }
                else {
                    modalService.show({ message: 'El valor no puede ser vacio' });
                }
            }

            setting.isEditing = !setting.isEditing;
        }
    }
})();