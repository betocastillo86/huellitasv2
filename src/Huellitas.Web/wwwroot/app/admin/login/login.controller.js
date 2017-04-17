(function () {
    angular.module('huellitasAdmin')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['authenticationService'];

    function LoginController(authenticationService)
    {
        var vm = this;
        vm.model = {};
        vm.validateAuthentication = validateAuthentication;
        vm.isSending = false;
        
        function validateAuthentication(isValid)
        {
            vm.errorAuthentication = undefined;

            if (isValid && !vm.isSending)
            {
                vm.isSending = true;
                authenticationService.post(vm.model)
                .then(authenticationValid)
                .catch(authenticationInvalid);
            }
        }

        function authenticationValid(response)
        {
            vm.isSending = false;
            document.location = '/admin/pets';
        }

        function authenticationInvalid()
        {
            vm.isSending = false;
            vm.errorAuthentication = 'Los datos son invalidos';
        }
    }
})();