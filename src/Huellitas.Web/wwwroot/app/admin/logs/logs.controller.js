(function () {
    angular.module('huellitasAdmin')
        .controller('ListLogsController', ListLogsController);

    ListLogsController.$inject = ['logService', 'modalService', 'helperService'];

    function ListLogsController(logService, modalService, helperService) {
        var vm = this;
        vm.logs = [];
        vm.isSending = false;

        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        }
        vm.pager = {};

        vm.changePage = changePage;
        vm.getLogs = getLogs;
        vm.showLog = showLog;
        vm.cleanLog = cleanLog;

        return activate();

        function activate() {
            getLogs();
        }

        function getLogs() {
            logService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.logs = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }
        }

        function showLog(log) {
            var message = '<b>Mensaje corto:</b><br>' + log.shortMessage + '<br> <b>Mensaje largo:</b><br>' + log.fullMessage + '<br> <b>Fecha:</b><br>' + log.creationDate + '<br> <b>IP:</b><br>' + log.ipAddress + '<br> <b>Url:</b><br>' + log.pageUrl;
            modalService.show({ message: message, large: true, title: 'Detalle Log' });
        }

        function changePage(page) {
            vm.filter.page = page;
            getLogs();
        }

        function cleanLog()
        {
            if (confirm("¿Está seguro de eliminar el log?"))
            {
                logService.clean()
                    .then(clenCompleted)
                    .catch(helperService.handleException);
            }

            function clenCompleted()
            {
                modalService.show({ message: 'Log eliminado correctamente' });
                getLogs();
            }
        }
    }
})();