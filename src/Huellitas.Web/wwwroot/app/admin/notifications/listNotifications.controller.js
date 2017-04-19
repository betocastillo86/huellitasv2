(function () {
    
    angular.module('huellitasAdmin')
        .controller('ListNotificationsController', ListNotificationsController);

    ListNotificationsController.$inject = ['notificationService', 'helperService'];

    function ListNotificationsController(notificationService, helperService)
    {
        var vm = this;
        
        vm.notifications = [];
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        };

        vm.changePage = changePage;
        vm.getNotifications = getNotifications;

        activate();

        return vm;

        function activate()
        {
            getNotifications();
        }

        function getNotifications()
        {
            notificationService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.notifications = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
                return vm.notifications;
            }
        }

        function changePage(page)
        {
            vm.filter.page = page;
            return getNotifications();
        }
    }

})();