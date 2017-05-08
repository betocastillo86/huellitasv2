
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('AdoptTermsController', AdoptTermsController);

    AdoptTermsController.$inject = ['$routeParams'];

    function AdoptTermsController($routeParams) {
        var vm = this;

        vm.friendlyName = $routeParams.friendlyName;

        activate();

        function activate() {

        }
    }
})();
