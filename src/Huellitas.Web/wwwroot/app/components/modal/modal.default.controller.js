(function () {
    angular.module('app')
        .controller('ModalDefaultController', ModalDefaultController);

    angular.$inject = ['$scope'];

    function ModalDefaultController($scope) {
        var vm = this;
        vm.title = $scope.title;
        vm.message = $scope.message;
        vm.close = close;

        function close() {
            $scope.close({ accept: true });
        }
    }
})();