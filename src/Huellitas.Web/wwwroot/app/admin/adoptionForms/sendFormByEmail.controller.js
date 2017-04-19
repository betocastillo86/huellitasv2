(function () {
    angular.module('huellitasAdmin')
            .controller('SendFormByEmailController', SendFormByEmailController);

    SendFormByEmailController.$inject = ['$routeParams', 'adoptionFormService', 'modalService', 'helperService'];

    function SendFormByEmailController($routeParams, adoptionFormService, modalService, helperService) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.isSending = false;

        vm.save = save;

        return vm;

        function save(isValid) {
            if (isValid && !vm.isSending) {
                vm.isSending = true;
                adoptionFormService.sendByEmail(vm.id, vm.email)
                    .then(postCompleted)
                    .catch(postError);
            }

            function postCompleted(response) {
                vm.isSending = false;
                modalService.show({ message: 'Mensaje enviado correctamente' });
            }

            function postError(response) {
                vm.isSending = false;
                helperService.handleException(response);
            }
        }
    }
})();