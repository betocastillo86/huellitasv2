
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('RootController', RootController);


    RootController.$inject = ['$location', '$scope', 'sessionService', 'authenticationService', 'helperService', 'routingService']

    function RootController($location, $scope, sessionService, authenticationService, helperService, routingService) {
        var vm = this;

        vm.currentUser = undefined;
        vm.showingUserInfo = false;
        vm.resources = app.Settings.resources;
        vm.currentMenu = '/';

        vm.showUserInfo = showUserInfo;
        vm.getRoute = routingService.getRoute;
        vm.showLogin = showLogin;
        vm.logout = logout;
        vm.goToAdmin = goToAdmin;


        activate();

        function activate() {

            $scope.$on("$routeChangeStart", locationChanged);

            if (sessionService.isAuthenticated()) {
                getCurrentUser();
            }
        }

        function goToAdmin()
        {
            window.location.href = '/admin';
        }

        function getCurrentUser() {
            authenticationService.get()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.currentUser = response;
            }
        }

        function showUserInfo() {
            vm.showingUserInfo = !vm.showingUserInfo;
        }
        
        function locationChanged(event, next, current) {
            vm.currentMenu = next.$$route.originalPath;
        }

        function showLogin()
        {
            authenticationService.showLogin($scope);
        }

        function logout()
        {
            sessionService.removeCurrentUser();
            vm.currentUser = undefined;
            $location.path(routingService.getRoute('home'));
        }
    }
})();
