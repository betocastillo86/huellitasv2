(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('UpdatePasswordRecoveryController', UpdatePasswordRecoveryController);

    UpdatePasswordRecoveryController.$inject = ['$scope', '$routeParams', 'userService', 'modalService', 'routingService'];

    function UpdatePasswordRecoveryController($scope, $routeParams, userService, modalService, routingService) {
        var vm = this;
        vm.token = $routeParams.token;
        vm.model = {};
        vm.isSending = false;

        vm.save = save;

        activate();

        function activate()
        {
            $scope.$parent.root.seo.title = 'Cambiar contraseña de huellitas sin hogar';
            $scope.$parent.root.seo.description = 'En esta página puedes realizar el cambio de tu contraseña si previamente ya la habías solicitado';
            $scope.$parent.root.seo.image = routingService.getFullRouteOfFile(app.Settings.general.seoImage);

            getToken();
        }

        function getToken()
        {
            userService.getPasswordRecovery(vm.token)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response)
            {

            }

            function getError(response)
            {
                modalService.showError({
                    message: 'La url a la que intentas acceder ya expiro. Debes realizar la solicitud de cambio de clave nuevamente',
                    redirectAfterClose: routingService.getRoute('home')
                });
            }
        }

        function save()
        {
            if (vm.form.$valid && !vm.isSending)
            {
                vm.isSending = true;

                userService.putPasswordRecovery(vm.token, vm.model)
                    .then(putCompleted)
                    .catch(putError);

                function putCompleted()
                {
                    modalService.show({
                        message: 'Tu clave ha sido cambiada correctamente, ingresa ahora a Hostaliando',
                        redirectAfterClose: routingService.getRoute('home'/*, {login: true}*/)
                    });
                }

                function putError()
                {
                    modalService.showError({
                        message: 'No fue posible actualizar tus datos'
                    });
                }
            }
        }
    }
})();

