
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('RootController', RootController);


    RootController.$inject = [
        '$http',
        '$location',
        '$scope',
        '$window',
        '$timeout',
        'sessionService',
        'authenticationService',
        'helperService',
        'routingService',
        'crawlingService']

    function RootController(
        $http,
        $location,
        $scope,
        $window,
        $timeout,
        sessionService,
        authenticationService,
        helperService,
        routingService,
        crawlingService) {

        var vm = this;

        vm.currentUser = undefined;
        vm.showingUserInfo = false;
        vm.resources = app.Settings.resources;
        vm.currentMenu = '/';
        vm.seo = {};
        vm.isShowingMenu = undefined;
        vm.previousPages = [];
        vm.isMobileWidth = false;
        vm.showAdsense = app.Settings.general.adsenseEnabled;

        vm.showUserInfo = showUserInfo;
        vm.getRoute = routingService.getRoute;
        vm.showLogin = showLogin;
        vm.logout = logout;
        vm.goToAdmin = goToAdmin;
        vm.showMenu = showMenu;
        vm.getFirstLetters = getFirstLetters;
        vm.getFullRoute = routingService.getFullRoute;
        vm.hideFooter = false;

        activate();

        function activate() {

            $scope.$on("$routeChangeStart", locationChanged);
            $scope.$on('$viewContentLoaded', contentLoaded);
            $scope.$on('seenNotifications', seenNotifications)

            if (sessionService.isAuthenticated()) {
                getCurrentUser();
            }

            vm.isMobileWidth = helperService.isMobileWidth();
        }

        function seenNotifications(event, seen)
        {
            vm.currentUser.unseenNotifications = vm.currentUser.unseenNotifications - seen;
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
                $http.defaults.headers.common.Authorization = '';
                console.log('Token Vencido');
            }
        }

        function showMenu(openButton) {
            if (vm.showingUserInfo) {
                showUserInfo();
            }

            //// Solo lo muestra si el clic viene del boton de abrir el menu
            //// o si previamente había abierto el menu
            if (vm.isShowingMenu !== undefined || openButton) {
                vm.isShowingMenu = !vm.isShowingMenu;
                angular.element('.menu').css('display', vm.isShowingMenu ? 'block' : 'none')
            }
        }

        function showUserInfo() {
            if (vm.isShowingMenu) {
                showMenu();
            }
            vm.showingUserInfo = !vm.showingUserInfo;
        }

        function locationChanged(event, next, current) {
            vm.currentMenu = next.$$route.originalPath;
            helperService.trackVisit($window, $location);
            vm.previousPages.push($location.$$path);
            $window.scrollTo(0, 0);
        }

        function contentLoaded() {
            vm.seo.url = $window.location.href;
            vm.seo.image = vm.seo.image ? vm.seo.image : routingService.getFullRouteOfFile(app.Settings.general.seoImage);

            validateCrawling();
        }

        function validateCrawling()
        {
            if ($location.search().angularjs)
            {
                vm.seo.url = vm.seo.url.replace('?angularjs=true', '').replace('angularjs=true', '');

                $timeout(function () {
                    var html = getHtml();
                    var url = $location.path();
                    crawlingService.post({ url: url, html: html })
                        .then(crawlingCompleted)
                        .catch(crawlingError);
                }, 2000);
            }

            function getHtml()
            {
                var imgs = $('img');
                for (var i = 0; i < imgs.length; i++) {
                    var img = $(imgs[i]);
                    if (img.attr('src').startsWith('/')) {
                        img.attr('src', app.Settings.general.siteUrl + img.attr('src'));
                    }
                }

                return new XMLSerializer().serializeToString(document);                
            }

            function crawlingCompleted() {
                console.log("crawling completado");
            }

            function crawlingError(err) {
                if (!window.opener) {
                    alert("Error haciendo crawling");
                }
                console.log(err);
            }
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
            $http.defaults.headers.common.Authorization = '';
            vm.currentUser = undefined;
            $location.path(routingService.getRoute('home'));
        }
    }
})();
