(function () {
    angular
        .module('app')
        .controller('SideBarController', SideBarController);

    SideBarController.$inject = ['moduleService'];

    function SideBarController(moduleService)
    {
        var vm = this;
        vm.title = "Este es el nuevo titulo";

        activate();

        function activate()
        {
            return getModules();
        }

        function getModules()
        {
            moduleService.getAll()
                .then(modulesCompleted);

            function modulesCompleted(data)
            {
                vm.modules = data;
                return vm.modules;
            }
        }
    }
})();