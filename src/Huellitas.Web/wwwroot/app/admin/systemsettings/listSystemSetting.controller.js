(function () {
    angular.module('huellitasAdmin')
        .controller('ListSystemSettingController', ListSystemSettingController);

    ListSystemSettingController.$inject = ['systemSettingService', 'modalService', 'helperService'];

    function ListSystemSettingController(systemSettingService, modalService, helperService) {
        var vm = this;
        vm.settings = [];
        vm.isSending = false;

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
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.settings = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
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
                vm.isSending = false;
                modalService.show({ message: 'Llave guardada correctamente' });
            }

            function putError(response) {
                vm.isSending = false;
                helperService.handleException(response);
            }
        }

        function toggleEdit(setting) {
            if (setting.isEditing) {
                if (!vm.isSending) {
                    vm.isSending = true;
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