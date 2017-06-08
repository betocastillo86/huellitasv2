
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('RootController', RootController);


    RootController.$inject = [
        '$location',
        '$scope',
        '$window',
        'sessionService',
        'authenticationService',
        'helperService',
        'routingService']

    function RootController($location,
        $scope,
        $window,
        sessionService,
        authenticationService,
        helperService,
        routingService) {

        var vm = this;

        vm.currentUser = undefined;
        vm.showingUserInfo = false;
        vm.resources = app.Settings.resources;
        vm.currentMenu = '/';
        vm.seo = {};
        vm.isShowingMenu = undefined;

        vm.showUserInfo = showUserInfo;
        vm.getRoute = routingService.getRoute;
        vm.showLogin = showLogin;
        vm.logout = logout;
        vm.goToAdmin = goToAdmin;
        vm.showMenu = showMenu;
        vm.getFirstLetters = getFirstLetters;

        activate();

        function activate() {

            $scope.$on("$routeChangeStart", locationChanged);
            $scope.$on('$viewContentLoaded', contentLoaded);

            if (sessionService.isAuthenticated()) {
                getCurrentUser();
            }
        }

        function goToAdmin() {
            window.location.href = '/admin';
        }

        function getCurrentUser() {
            authenticationService.get()
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response) {
                vm.currentUser = response;
            }

            function getError(response) {
                sessionService.removeCurrentUser();
                console.log('Token Vencido');
            }
        }

        function showMenu(openButton) {
            //// Solo lo muestra si el clic viene del boton de abrir el menu
            //// o si previamente había abierto el menu
            if (vm.isShowingMenu !== undefined || openButton) {
                vm.isShowingMenu = !vm.isShowingMenu;
                angular.element('.menu').css('display', vm.isShowingMenu ? 'block' : 'none')
            }
        }

        function showUserInfo() {
            vm.showingUserInfo = !vm.showingUserInfo;
        }

        function locationChanged(event, next, current) {
            vm.currentMenu = next.$$route.originalPath;
            helperService.trackVisit($window, $location);
        }

        function contentLoaded() {
            vm.seo.url = $window.location.href;
            vm.seo.image = vm.seo.image ? vm.seo.image : routingService.getFullRouteOfFile(app.Settings.general.seoImage);
        }

        function showLogin() {
            authenticationService.showLogin($scope);
        }

        function getFirstLetters()
        {
            if (vm.currentUser)
            {
                var nameParts = vm.currentUser.name.split(/ /g);
                return nameParts[0][0] + (nameParts.length > 1 ? nameParts[1][0] : '');
            }
        }

        function logout() {
            sessionService.removeCurrentUser();
            vm.currentUser = undefined;
            $location.path(routingService.getRoute('home'));
        }
    }
})();
