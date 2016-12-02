(function() {
    'use strict';

    angular
        .module('app')
        .directive('mainMenu', mainMenu);

    //MainMenuDirective.$inject = [];
    
    function mainMenu() {
        // Usage:
        //     <main-menu></main-menu>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'E',
            //templateUrl: '/app/layout/sidebar.html',
            //controller: 'SideBarController',
            //controllerAs: 'SideBar'
        };
        return directive;

        function link(scope, element, attrs) {

        }
    }

})();