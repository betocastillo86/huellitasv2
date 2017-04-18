(function () {
    angular.module('huellitasAdmin')
        .controller('MyNotificationsController', MyNotificationsController);

    MyNotificationsController.$inject = ['notificationService', 'helperService'];

    function MyNotificationsController(notificationService, helperService)
    {
        var vm = this;
        vm.currentPage = 0;
        vm.notifications = [];
        vm.showPager = true;

        vm.showMore = showMore;

        return activate();

        function activate()
        {
            loadNotifications();
        }

        function loadNotifications()
        {
            notificationService.getMyNotifications({ page : vm.currentPage, pageSize : app.Settings.general.pageSize })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.notifications = _.union(vm.notifications, response.results);
                vm.showPager = response.meta.hasNextPage;
            }
        }

        function showMore()
        {
            vm.currentPage++;
            loadNotifications();
        }
    }
})();