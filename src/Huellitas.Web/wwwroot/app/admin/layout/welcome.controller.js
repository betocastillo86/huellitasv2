﻿(function () {
    angular.module('huellitasAdmin')
        .controller('WelcomeController', WelcomeController);

    WelcomeController.$inject = ['$scope', 'modalService'];

    function WelcomeController($scope, modalService) {
        console.log('Carga el controlador');
        var vm = this;
        vm.name = 'Gabriel Castillo Incompleto;';
        vm.modal = {};
        vm.showModal = function () {
            modalService.show({
                message: 'este es el mensaje'
            });
        }

        return vm;
    }
})();