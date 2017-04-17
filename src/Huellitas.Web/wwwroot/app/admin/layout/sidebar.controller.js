(function () {
    angular
        .module('huellitasAdmin')
        .controller('SideBarController', SideBarController);

    SideBarController.$inject = ['moduleService', '$location'];

    function SideBarController(moduleService, $location)
    {
        var vm = this;
        vm.title = "Huellitas Admin";
        vm.currentMenuOption = undefined;
        vm.currentChild = undefined;

        vm.changeCurrentMenuOption = changeCurrentMenuOption;
        vm.changeCurrentChild = changeCurrentChild;

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

                for (var i = 0; i < vm.modules.length; i++) {
                    var module = vm.modules[i];
                    if (module.url == $location.$$url) {
                        vm.currentMenuOption = module.key;
                    }
                    module.url = module.children ? '#' : ('/admin' + module.url);

                    if (module.children)
                    {
                        for (var j = 0; j < module.children.length; j++) {
                            var child = module.children[j];
                            if (child.url == $location.$$url) {
                                vm.currentMenuOption = module.key;
                                vm.currentChild = child.key;
                            }

                            child.url = '/admin' + child.url;
                        }
                    }
                }

                return vm.modules;
            }
        }

        function changeCurrentMenuOption(module)
        {
            vm.currentMenuOption = module.key;
        }

        function changeCurrentChild(module) {
            vm.currentChild = module.key;
        }
    }
})();