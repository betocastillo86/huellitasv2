(function () {
    angular.module('app')
        .controller('WelcomeController', WelcomeController);

    WelcomeController.$inject = ['$scope'];

    function WelcomeController($scope)
    {
        var vm = this;
        vm.name = 'Gabriel Castillo Incompleto;';
        return vm;
    }
})();