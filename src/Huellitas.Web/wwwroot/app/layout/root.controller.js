(function () {
    angular.module('app')
        .controller('RootController', RootController);

    RootController.$inject = ['authenticationService', 'sessionService'];

    function RootController(authenticationService, sessionService)
    {
        var vm = this;
        vm.currentUser = undefined;

        return activate();

        function activate()
        {
            loadCurrentUser();
        }

        function loadCurrentUser()
        {
            if (sessionService.getCurrentUser())
            {
                authenticationService.get()
                   .then(getCompleted)
                   .catch(getError);
            }

            function getCompleted(response)
            {
                vm.currentUser = response.data;
            }

            function getError()
            {
                console.log('Usuario no autenticado');
            }
        }
    }

})();