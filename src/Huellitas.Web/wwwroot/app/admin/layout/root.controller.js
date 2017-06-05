(function () {
    angular.module('huellitasAdmin')
        .controller('RootController', RootController);

    RootController.$inject = ['authenticationService', 'sessionService', 'helperService', 'cacheService', 'modalService', 'routingService'];

    function RootController(authenticationService, sessionService, helperService, cacheService, modalService, routingService)
    {
        var vm = this;
        vm.currentUser = undefined;
        vm.isShowingMobileMenu = false;
        
        vm.showMobileMenu = showMobileMenu;
        vm.clearCache = clearCache;
        vm.getRoute = routingService.getRoute;

        activate();

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

        function clearCache()
        {
            cacheService.clear()
                .then(clearCompleted)
                .catch(helperService.handleException);

            function clearCompleted()
            {
                modalService.show({ message: 'Cache borrado correctamente'});
            }
        }
    }

})();