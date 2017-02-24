(function () {
    angular.module('app')
            .controller('SendFormByEmailController', SendFormByEmailController);

    SendFormByEmailController.$inject = ['$routeParams', 'adoptionFormService'];

    function SendFormByEmailController($routeParams, adoptionFormService)
    {
        var vm = this;
        vm.id = $routeParams.id;

        vm.save = save;

        return vm;

        function save(isValid)
        {
            if (isValid)
            {
                adoptionFormService.sendByEmail(vm.id, vm.email);
            }
        }
    }
})();