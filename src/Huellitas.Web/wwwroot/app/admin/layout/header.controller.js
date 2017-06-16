(function () {
    angular.module('huellitasAdmin')
        .controller('HeaderController', HeaderController);

    HeaderController.$inject = ['$http', 'sessionService', 'notificationService', 'helperService'];

    function HeaderController($http, sessionService, notificationService, helperService)
    {
        var vm = this;
        vm.model = {};
        vm.logout = logout;
        vm.website = website;
        vm.notifications = [];
        vm.showNotifications = false;


        vm.toogleNotifications = toogleNotifications;

        active();

        function active()
        {
            vm.model.user = sessionService.getCurrentUser();
        }

        function logout()
        {
            sessionService.removeCurrentUser();
            $http.defaults.headers.common.Authorization = '';
            document.location = '/admin/login';
        }

        function website()
        {
            document.location = '/';
        }

        function loadNotifications()
        {
            notificationService.getMyNotifications()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                var notifications = response.results;
                for (var i = 0; i < notifications.length; i++) {
                    var notification = notifications[i];
                    notification['creationDateString'] = notification.creationDate.getIntervalTime();
                }
                vm.notifications = notifications;
            }
        }

        function toogleNotifications()
        {
            vm.showNotifications = !vm.showNotifications;
            if (vm.showNotifications)
            {
                loadNotifications();
            }
        }
    }
    
})();