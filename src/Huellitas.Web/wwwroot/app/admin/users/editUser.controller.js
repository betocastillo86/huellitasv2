(function () {
    angular.module('huellitasAdmin')
        .controller('EditUserController', EditUserController);

    EditUserController.$inject = ['$routeParams', '$location', 'userService', 'roleService', 'modalService', 'helperService'];

    function EditUserController($routeParams, $location, userService, roleService, modalService, helperService) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.isSending = false;
        vm.model = {};
        vm.roles = [];
        vm.continueAfterSaving = false;
        vm.changePassword = vm.id == undefined;

        vm.save = save;
        vm.saveAndContinue = saveAndContinue;
        vm.changeLocation = changeLocation;

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
                .catch(helperService.handleException);

            function getUserCompleted(response) {
                vm.model = response;
            }
        }

        function getRoles() {
            roleService.getAll()
                .then(getRolesCompleted)
                .catch(helperService.handleException);

            function getRolesCompleted(result) {
                vm.roles = result;
            }
        }

        function save(isValid) {

            if (isValid && !vm.isSending) {
                vm.isSending = true;
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

            function saveCompleted(response) {
                vm.isSending = false;
                response = response;

                var message = 'El usuario fue actualizado con exito';
                var isNew = !vm.model.id;

                if (isNew) {
                    message = 'El usuario fue creado con exito';
                    vm.model.id = response.id;
                }

                modalService.show({
                    message: message
                })
                    .then(function (modal) {
                        modal.closed.then(function () {
                            if (vm.continueAfterSaving) {
                                if (isNew) {
                                    $location.path('/users/' + vm.model.id + '/edit');
                                }
                            }
                            else {
                                $location.path('/users');
                            }

                            vm.continueAfterSaving = false;
                        });
                    });
            }

            function saveError(response) {
                vm.isSending = false;
                helperService.handleException(response);
            }
        }

        function saveAndContinue() {
            vm.continueAfterSaving = true;
        }

        function changeLocation(selectedLocation)
        {
            vm.model.location = selectedLocation ? selectedLocation.originalObject : undefined;
        }

    }
})();