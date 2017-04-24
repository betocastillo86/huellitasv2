﻿(function () {
    angular.module('huellitasComponents')
        .controller('ModalDefaultController', ModalDefaultController);

    angular.$inject = ['$scope'];

    function ModalDefaultController($scope) {
        var vm = this;
        vm.title = $scope.title;
        vm.message = $scope.message;
        vm.large = $scope.large;
        vm.close = close;

        function close() {
            $scope.close({ accept: true });
        }
    }
})();