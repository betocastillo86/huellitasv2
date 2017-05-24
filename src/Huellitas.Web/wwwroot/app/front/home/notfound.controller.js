(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('NotFoundController', NotFoundController);

    function NotFoundController() {
        var vm = this;

        activate();

        function activate() { }
    }
})();