(function () {
    
    angular.module('huellitasAdmin')
        .controller('ListEmailNotificationsController', ListEmailNotificationsController);

    ListEmailNotificationsController.$inject = ['emailNotificationService'];

    function ListEmailNotificationsController(emailNotificationService)
    {
        var vm = this;
        
        vm.notifications = [];
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        };

        vm.changePage = changePage;
        vm.changeSent = changeSent;
        vm.getNotifications = getNotifications;

        activate();

        return vm;

        function activate()
        {
            getNotifications();
        }

        function getNotifications()
        {
            emailNotificationService.getAll(vm.filter)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response)
            {
                vm.notifications = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
                return vm.notifications;
            }

            function getError()
            {
                console.log('Error mostrando notifications');
            }
        }

        function changeSent(sent)
        {
            if (vm.filter.sent === sent) {
                vm.filter.sent = undefined;
            }
            else {
                vm.filter.sent = sent;
            }

            getNotifications();
        }

        function changePage(page)
        {
            vm.filter.page = page;
            return getNotifications();
        }
    }

})();