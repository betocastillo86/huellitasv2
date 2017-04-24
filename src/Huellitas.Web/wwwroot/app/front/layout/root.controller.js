
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('RootController', RootController);


    RootController.$inject = ['sessionService', 'authenticationService', 'helperService']

    function RootController(sessionService, authenticationService, helperService) {
        var vm = this;

        vm.currentUser = undefined;
        vm.showingUserInfo = false;
        
        vm.showUserInfo = showUserInfo;

        activate();

        function activate() {
            if (sessionService.isAuthenticated())
            {
                getCurrentUser();
            }
        }

        function getCurrentUser()
        {
            authenticationService.get()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.currentUser = response;
            }
        }

        function showUserInfo()
        {
            vm.showingUserInfo = !vm.showingUserInfo;
        }
    }
})();
