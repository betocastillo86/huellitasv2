(function () {
    angular.module('huellitasAdmin')
        .controller('ListUsersController', ListUsersController);

    ListUsersController.$inject = ['userService', 'roleService'];

    function ListUsersController(userService, roleService) {
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
            .catch(getUsersError);

            function getUsersCompleted(response)
            {
                vm.users = response.data.results;
                vm.pager = response.data.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
            }

            function getUsersError()
            {
                console.log('Error al obtener');
            }
        }

        function getRoles()
        {
            roleService.getAll()
                .then(getAllCompleted)
                .catch(getAllError);

            function getAllCompleted(response)
            {
                vm.roles = response.data;
            }

            function getAllError()
            {
                console.log('Error al cargar');
            }
        }

        function changePage(page)
        {
            vm.filter.page = page;
            getUsers();
        }
    }
})();