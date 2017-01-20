(function () {
    angular.module('app')
        .controller('EditUserController', EditUserController);

    EditUserController.$inject = ['$routeParams', '$location', 'userService', 'roleService', 'modalService'];

    function EditUserController($routeParams, $location, userService, roleService, modalService) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.model = {};
        vm.roles = [];
        vm.continueAfterSaving = false;
        vm.changePassword = vm.id == undefined;

        vm.save = save;
        vm.saveAndContinue = saveAndContinue;

        activate();

        return vm;

        function activate() {
            if (vm.id) {
                getUserById(vm.id);
            }

            getRoles();
        }

        function getUserById(id) {
            userService.getById(id)
                .then(getUserCompleted)
                .catch(getUserError);

            function getUserCompleted(response) {
                vm.model = response.data;
            }

            function getUserError() {
                console.log('get user error');
            }
        }

        function getRoles() {
            roleService.getAll()
            .then(getRolesCompleted)
            .catch(getRolesError);

            function getRolesCompleted(result) {
                vm.roles = result.data;
            }

            function getRolesError() {
                console.log('get roles error');
            }
        }

        function save(isValid) {

            if (isValid)
            {
                var defer;
                if (vm.id) {
                    defer = userService.put(vm.model);
                }
                else {
                    defer = userService.post(vm.model);
                }

                defer.then(saveCompleted)
                    .catch(saveError);
            }

            function saveCompleted(response)
            {
                response = response.data;

                var message = 'El usuario fue actualizado con exito';
                var isNew = !vm.model.id;

                if (isNew)
                {
                    message = 'El usuario fue creado con exito';
                    vm.model.id = response.id;
                }

                modalService.show({
                    message: message
                })
                .then(function (modal) {
                    modal.closed.then(function () {
                        if (vm.continueAfterSaving)
                        {
                            if (isNew)
                            {
                                $location.path('/users/'+vm.model.id+'/edit');
                            }
                        }
                        else
                        {
                            $location.path('/users');
                        }

                        vm.continueAfterSaving = false;
                    });
                });
            }

            function saveError(response) {
                modalService.showError({ error: response.data.error });
            }
        }

        function saveAndContinue() {
            vm.continueAfterSaving = true;
        }

    }
})();