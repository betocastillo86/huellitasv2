(function () {
    angular.module('huellitasComponents')
        .controller('ModalDefaultController', ModalDefaultController);

    ModalDefaultController.$inject = ['$scope', 'routingService'];

    function ModalDefaultController($scope, routingService) {
        var vm = this;
        vm.title = $scope.title;
        vm.message = $scope.message;
        vm.large = $scope.large;
        vm.close = close;

        vm.getRoute = routingService.getRoute;

        function close() {
            $scope.close({ accept: true });
        }
    }
})();