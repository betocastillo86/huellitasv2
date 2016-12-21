(function () {
    angular.module('app')
        .controller('HeaderController', HeaderController);

    HeaderController.$inject = ['sessionService'];

    function HeaderController(sessionService)
    {
        var vm = this;
        vm.model = {};
        vm.logout = logout;


        active();

        function active()
        {
            vm.model.user = sessionService.getCurrentUser();
        }

        function logout()
        {
            sessionService.removeCurrentUser();
            document.location = '/admin/login';
        }
    }
    
})();