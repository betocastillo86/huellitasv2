(function () {
    angular.module('huellitasAdmin')
        .controller('EditEmailNotificationController', EditEmailNotificationController);

    EditEmailNotificationController.$inject = ['$routeParams', '$location', 'emailNotificationService', 'modalService', 'helperService'];

    function EditEmailNotificationController($routeParams, $location, emailNotificationService, modalService, helperService)
    {
        var vm = this;
        vm.model = {};
        vm.id = $routeParams.id;
        vm.isSending = false;
        vm.requeue = false;

        vm.save = save;
        vm.markQueue = markQueue;

        activate();

        return vm;

        function activate()
        {
            getNotification();
        }

        function getNotification()
        {
            emailNotificationService.getById(vm.id)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.model = response;
            }
        }

        function save(isValid)
        {
            if (isValid && !vm.isSending)
            {
                vm.isSending = true;

                if (vm.requeue) {
                    if(confirm('¿Seguro desea encolar nuevamente el correo?'))
                    {
                        emailNotificationService.post(vm.model)
                                        .then(saveCompleted)
                                        .catch(saveError);
                    }
                    else
                    {
                        vm.requeue = false;
                        vm.isSending = false;
                        return;
                    }
                }
                else {
                    emailNotificationService.put(vm.id, vm.model)
                                        .then(saveCompleted)
                                        .catch(saveError);
                }

                function saveCompleted(response)
                {
                    vm.isSending = false;
                    
                    var message = vm.requeue ? 'Notificación en cola nuevamente' : 'Notificación actualizada';
                    modalService.show({ message: message })
                        .then(messageShowed)
                        .catch(helperService.handleException);

                    vm.requeue = false;

                    function messageShowed(modal)
                    {
                        modal.closed.then(messageClosed);
                    }

                    function messageClosed()
                    {
                        $location.path('/emailnotifications');
                    }
                }

                function saveError(response)
                {
                    vm.isSending = false;
                    vm.requeue = false;
                    helperService.handleException(response);
                }
            }
        }

        function markQueue()
        {
            vm.requeue = true;
        }
    }
})();