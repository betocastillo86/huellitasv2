﻿(function () {
    angular.module('huellitasComponents')
        .controller('ModalDialogController', ModalDialogController);

    ModalDialogController.$inject = ['$scope'];

    function ModalDialogController($scope) {
        var vm = this;
        vm.title = $scope.title;
        vm.message = $scope.message;
        vm.large = $scope.large;

        vm.cancel = cancel;
        vm.accept = accept;

        function cancel()
        {
            $scope.close({ accept: false });
        }

        function accept()
        {
            $scope.close({ accept: true });
        }

        function close() {
            $scope.close({ accept: true });
        }
    }
})();