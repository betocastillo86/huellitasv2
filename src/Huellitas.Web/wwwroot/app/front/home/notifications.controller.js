(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('NotificationsController', NotificationsController);

    NotificationsController.$inject = ['notificationService', 'helperService'];

    function NotificationsController(notificationService, helperService) {
        var vm = this;
        vm.notifications = [];
        vm.hasNextPage = false;
        vm.filter = {
            page: 0,
            pageSize: 10
        };

        vm.more = more;

        activate();

        function activate() {
            getNotifications();
        }

        function getNotifications() {
            notificationService.getMyNotifications(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                if (vm.notifications.length) {
                    vm.notifications = vm.notifications.concat(response.results);
                }
                else {
                    vm.notifications = response.results;
                }

                vm.hasNextPage = response.meta.hasNextPage;
            }
        }

        function more() {
            vm.filter.page++;
            getNotifications();
        }
    }
})();