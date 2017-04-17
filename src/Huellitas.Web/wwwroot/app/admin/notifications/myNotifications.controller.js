(function () {
    angular.module('huellitasAdmin')
        .controller('MyNotificationsController', MyNotificationsController);

    MyNotificationsController.$inject = ['notificationService'];

    function MyNotificationsController(notificationService)
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
                .catch(getError);

            function getCompleted(response)
            {
                vm.notifications = _.union(vm.notifications, response.data.results);
                vm.showPager = response.data.meta.hasNextPage;
            }

            function getError()
            {
                console.log('Error cargando');
            }
        }

        function showMore()
        {
            vm.currentPage++;
            loadNotifications();
        }
    }
})();