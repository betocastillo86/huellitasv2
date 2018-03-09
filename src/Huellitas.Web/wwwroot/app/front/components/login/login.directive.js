
(function () {
    'use strict';

    angular
        .module('huellitas')
        .directive('loginHuellitas', loginHuellitas);

    function loginHuellitas() {
        return {
            restrict: 'E',
            bindToController: true,
            controller: LoginController,
            controllerAs: 'login',
            templateUrl: '/app/front/components/login/login.html?' + app.Settings.general.configJavascriptCacheKey
        };
    }

    angular
        .module('huellitas')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$scope', '$element', 'authenticationService', 'modalService', 'userService', 'helperService'];

    function LoginController($scope, $element, authenticationService, modalService, userService, helperService) {
        var vm = this;
        vm.model = {};
        vm.modal = undefined;
        vm.modeLogin = false;

        vm.register = register;
        vm.authenticate = authenticate;
        vm.facebookLogin = facebookLogin;
        vm.getPasswordRecovery = getPasswordRecovery;

        activate();

        function activate() {
            vm.modal = $('#loginModal').modal();
            vm.modal.on('hidden.bs.modal', function () {
                //$scope.$destroy();
                $element.remove();
                //$(this).data('bs.modal', null);
                //$(this).remove();
                //// Cuando se cierra sin autenticar rechaza la promesa de autenticación
                if (authenticationService.promiseAuth)
                {
                    authenticationService.promiseAuth.reject({});
                }
            })
        } 

        function authenticate() {
            if (vm.form.$valid && !vm.form.isBusy) {
                vm.form.isBusy = true;

                if (vm.modeLogin) {
                    authenticationService.post(vm.model)
                        .then(authCompleted)
                        .catch(postError);
                }
                else {
                    userService.post(vm.model)
                        .then(registerCompleted)
                        .catch(registerError);
                }
            }

            function registerCompleted(response)
            {
                var user = authenticationService.setSessionUser(response);
                authCompleted(user);
            }

            function postError(response) {
                vm.form.isBusy = false;

                if (response.status === 401) {
                    modalService.showError({ message: 'Usuario y clave invalidos', isFront: true });
                }
                else
                {
                    helperService.handleException(response);
                }
            }

            function registerError(response) {
                vm.form.isBusy = false;
                helperService.handleException(response);
            }
        }

        function facebookLogin() {
            authenticationService.external('facebook')
                .then(externalCompleted);

            function externalCompleted(response) {
                authCompleted(response);
            }
        }

        function authCompleted(response) {

            authenticationService.promiseAuth.resolve(response);

            vm.form.isBusy = false;
            $scope.root.currentUser = response;

            closeModal();
        }

        function closeModal()
        {
            vm.modal.off('hidden.bs.modal');
            vm.modal.modal('toggle');
            $(vm.modal).data('bs.modal', null);
            $(vm.modal).remove();
        }

        function register(isRegister) {
            vm.modeLogin = !isRegister;
        }

        function getPasswordRecovery()
        {
            closeModal();
            modalService.show({
                controller: 'GetPasswordRecoveryController',
                template: '/app/front/home/getPasswordRecovery.html?' + app.Settings.general.configJavascriptCacheKey,
                controllerAs: 'getPassword'
            });
        }
    }

})();

