(function () {
    angular.module('huellitasAdmin')
        .controller('AddFormUserController', AddFormUserController);

    AddFormUserController.$inject = ['$routeParams', 'adoptionFormService', 'modalService', 'helperService'];

    function AddFormUserController($routeParams, adoptionFormService, modalService, helperService) {
        var vm = this;
        vm.model = {};
        vm.model.id = $routeParams.id;

        vm.userChanged = userChanged;
        vm.save = save;
        vm.isSending = false;

        activate();

        return vm;

        function activate() {
            console.log('activado controller');
        }

        function save(isValid) {
            if (isValid && !vm.isSending) {
                vm.isSending = true;
                if (confirm('¿Está seguro de invitar a este usuario?')) {
                    adoptionFormService.postUser(vm.model.id, vm.model.invitedUserId)
                            .then(postUserCompleted)
                            .catch(postUserError);

                    function postUserCompleted() {
                        vm.isSending = false;
                        modalService.show({ message: 'Usuario asociado correctamente' });
                    }

                    function postUserError(response) {
                        vm.isSending = false;
                        helperService.handleException(response);
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