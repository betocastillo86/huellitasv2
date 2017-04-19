(function () {
    angular.module('huellitasAdmin')
        .controller('ListUsersController', ListUsersController);

    ListUsersController.$inject = ['userService', 'roleService', 'helperService'];

    function ListUsersController(userService, roleService, helperService) {
        var vm = this;
        vm.users = [];
        vm.roles = [];

        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        };
        vm.pager = {};

        vm.changePage = changePage;
        vm.getUsers = getUsers;
        vm.resources = app.Settings.resources;

        activate();

        return vm;

        function activate() {
            getUsers();
            getRoles();
        }

        function getUsers()
        {
            userService.getAll(vm.filter)
            .then(getUsersCompleted)
            .catch(helperService.handleException);

            function getUsersCompleted(response)
            {
                vm.users = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }
        }

        function getRoles()
        {
            roleService.getAll()
                .then(getAllCompleted)
                .catch(helperService.handleException);

            function getAllCompleted(response)
            {
                vm.roles = response;
            }
        }

        function changePage(page)
        {
            vm.filter.page = page;
            getUsers();
        }
    }
})();