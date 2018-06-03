(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('GetPasswordRecoveryController', GetPasswordRecoveryController);

    GetPasswordRecoveryController.$inject = ['userService', 'modalService', 'routingService'];

    function GetPasswordRecoveryController(userService, modalService, routingService) {
        var vm = this;

        vm.isSending = false;
        vm.model = {};
        vm.errorToken = undefined;
        vm.modal = undefined;

        vm.save = save;

        activate();

        function activate()
        {
        }

        function save() {
            if (vm.form.$valid && !vm.isSending) {
                userService.postPasswordRecovery(vm.model)
                    .then(postCompleted)
                    .catch(postError);
            }

            function postCompleted(response) {
                closeModal();
                modalService.show(
                    {
                        message: 'Te acabamos de enviar un correo para que puedas escoger tu nueva clave. Revísalo.',
                        redirectAfterClose: routingService.getRoute('login')
                    });
                vm.errorToken = undefined;
            }

            function postError(response) {
                vm.errorToken = 'Valida que tu correo si se encuentre registrado';
            }
        }

        function closeModal() {
            vm.modal = $('#getPasswordRecovery').modal();
            vm.modal.off('hidden.bs.modal');
            vm.modal.modal('toggle');
            $(vm.modal).data('bs.modal', null);
            $(vm.modal).remove();
        }
    }
})();

