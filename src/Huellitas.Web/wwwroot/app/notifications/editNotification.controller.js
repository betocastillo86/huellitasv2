(function () {
    angular.module('app')
        .controller('EditNotificationController', EditNotificationController);

    EditNotificationController.$inject = ['$routeParams', '$location', 'notificationService', 'modalService'];

    function EditNotificationController($routeParams, $location, notificationService, modalService)
    {
        var vm = this;
        vm.model = {};
        vm.id = $routeParams.id;
        vm.continueAfterSaving = false;
        vm.tags = [];

        vm.save = save;
        vm.saveAndContinue = saveAndContinue;
        vm.changeIsEmail = changeIsEmail;
        vm.changeIsSystem = changeIsSystem;

        activate();

        return vm;

        function activate()
        {
            getNotification();
        }

        function getNotification()
        {
            notificationService.getById(vm.id)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response)
            {
                vm.model = response.data;
                vm.tags = JSON.parse(vm.model.tags);
            }

            function getError()
            {
                console.log('Error cargando notificacion');
            }
        }

        function save(isValid)
        {
            if (isValid)
            {
                notificationService.put(vm.id, vm.model)
                    .then(putCompleted)
                    .catch(putError);

                function putCompleted(response)
                {
                    modalService.show({message: 'Notificación actualizada correctamente'})
                        .then(messageShowed);

                    function messageShowed(modal)
                    {
                        modal.closed.then(messageClosed);
                    }

                    function messageClosed()
                    {
                        if(!vm.continueAfterSaving)
                        {
                            $location.path('/notifications');
                        }

                        vm.continueAfterSaving = false;
                    }
                }

                function putError(response)
                {
                    modalService.showError({ error: response.data.error });
                }
            }
        }

        function saveAndContinue()
        {
            vm.continueAfterSaving = true;
        }

        function changeIsEmail(isEmail)
        {
            vm.model.isEmail = isEmail;
        }

        function changeIsSystem(isSystem) {
            vm.model.isSystem = isSystem;
        }
    }
})();