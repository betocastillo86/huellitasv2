(function () {
    angular.module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['authenticationService'];

    function LoginController(authenticationService)
    {
        var vm = this;
        vm.model = {};
        vm.validateAuthentication = validateAuthentication;
        
        function validateAuthentication(isValid)
        {
            vm.errorAuthentication = undefined;

            if (isValid)
            {
                authenticationService.post(vm.model)
                .then(authenticationValid)
                .catch(authenticationInvalid);
            }
        }

        function authenticationValid(response)
        {
            response = response.data;


            //document.location.href = '/admmi';
            console.log('autenticado');
        }

        function authenticationInvalid()
        {
            vm.errorAuthentication = 'Los datos son invalidos';
        }
    }
})();