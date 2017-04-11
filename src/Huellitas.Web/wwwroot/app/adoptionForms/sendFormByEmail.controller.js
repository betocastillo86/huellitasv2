(function () {
    angular.module('app')
            .controller('SendFormByEmailController', SendFormByEmailController);

    SendFormByEmailController.$inject = ['$routeParams', 'adoptionFormService', 'modalService'];

    function SendFormByEmailController($routeParams, adoptionFormService, modalService) {
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
                modalService.showError({ error: response.data.error });
            }
        }
    }
})();