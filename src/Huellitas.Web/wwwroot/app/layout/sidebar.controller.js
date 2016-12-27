(function () {
    angular
        .module('app')
        .controller('SideBarController', SideBarController);

    SideBarController.$inject = ['moduleService'];

    function SideBarController(moduleService)
    {
        var vm = this;
        vm.title = "Huellitas sin hogar";

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