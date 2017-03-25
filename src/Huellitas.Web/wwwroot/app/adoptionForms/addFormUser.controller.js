﻿(function () {
    angular.module('app')
        .controller('AddFormUserController', AddFormUserController);

    AddFormUserController.$inject = ['$routeParams', 'adoptionFormService', 'modalService'];

    function AddFormUserController($routeParams, adoptionFormService, modalService) {
        var vm = this;
        vm.model = {};
        vm.model.id = $routeParams.id;

        vm.userChanged = userChanged;
        vm.save = save;

        activate();

        return vm;

        function activate() {
            console.log('activado controller');
        }

        function save(isValid) {
            if (isValid) {
                if (confirm('¿Está seguro de invitar a este usuario?')) {
                    adoptionFormService.postUser(vm.model.id, vm.model.invitedUserId)
                            .then(postUserCompleted)
                            .catch(postUserError);

                    function postUserCompleted() {
                        modalService.show({ message: 'Usuario asociado correctamente' });
                    }

                    function postUserError(response) {
                        modalService.showError({ error: response.data.error });
                    }
                }
            }
        }

        function userChanged(selected) {
            if (selected) {
                vm.model.invitedUserId = selected.originalObject.id;
            }
        }
    }

})();