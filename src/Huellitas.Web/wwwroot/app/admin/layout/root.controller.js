(function () {
    angular.module('huellitasAdmin')
        .controller('RootController', RootController);

    RootController.$inject = ['authenticationService', 'sessionService', 'helperService'];

    function RootController(authenticationService, sessionService, helperService)
    {
        var vm = this;
        vm.currentUser = undefined;
        vm.isShowingMobileMenu = false;

        vm.showMobileMenu = showMobileMenu;

        return activate();

        function activate()
        {
            loadCurrentUser();
        }

        function loadCurrentUser()
        {
            if (sessionService.getCurrentUser())
            {
                authenticationService.get()
                   .then(getCompleted)
                   .catch(helperService.handleException);
            }

            function getCompleted(response)
            {
                vm.currentUser = response;
            }
        }

        function showMobileMenu()
        {
            vm.isShowingMobileMenu = !vm.isShowingMobileMenu;
        }
    }

})();