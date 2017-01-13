(function () {
    angular.module('app')
        .controller('ListUsersController', ListUsersController);

    ListUsersController.$inject = ['userService'];

    function ListUsersController(userService) {
        var vm = this;
        vm.users = [];

        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        };
        vm.pager = {};

        vm.changePage = changePage;
        vm.getUsers = getUsers;

        activate();

        return vm;

        function activate() {
            getUsers();
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

        function changePage(page)
        {
            vm.filter.page = page;
            getUsers();
        }
    }
})();