(function () {
    'use strict';

    angular
        .module('huellitasAdmin')
        .controller('ContactUserController', ContactUserController);

    ContactUserController.$inject = ['$routeParams', 'userService', 'helperService', 'modalService'];

    function ContactUserController($routeParams, userService, helperService, modalService) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.model = {};

        vm.save = save;


        activate();

        function activate()
        {
            
        }

        function save(isValid)
        {
            if (isValid)
            {
                userService.contact(vm.id, vm.model)
                    .then(contactCompleted)
                    .catch(helperService.handleException);
            }

            function contactCompleted(response)
            {
                modalService.show({ message: 'Mensaje enviado correctamente' });
            }
        }
    }
})();

