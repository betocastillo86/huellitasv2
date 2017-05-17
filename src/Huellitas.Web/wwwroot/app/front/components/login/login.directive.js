
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
            templateUrl: '/app/front/components/login/login.html'
        };
    }

    angular
        .module('huellitas')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$scope', 'authenticationService', 'modalService', 'userService', 'helperService'];

    function LoginController($scope, authenticationService, modalService, userService, helperService) {
        var vm = this;
        vm.model = {};
        vm.modal = undefined;
        vm.modeLogin = true;

        vm.register = register;
        vm.authenticate = authenticate;
        vm.facebookLogin = facebookLogin;

        activate();

        function activate() {
            vm.modal = $('#loginModal').modal();
        }

        function authenticate() {
            if (vm.form.$valid && !vm.form.isBusy) {
                vm.form.isBusy = true;

                if (vm.modeLogin) {
                    authenticationService.post(vm.model)
                        .then(postCompleted)
                        .catch(postError);
                }
                else {
                    userService.post(vm.model)
                        .then(registerCompleted)
                        .catch(registerError);
                }
            }

            function postCompleted(response) {
                vm.form.isBusy = false;
                $scope.root.currentUser = response;
                vm.modal.modal('toggle');
            }

            function registerCompleted(response)
            {
                authenticationService.setSessionUser(response);
                postCompleted(response);
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

                console.log(response.error);
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
                $scope.root.currentUser = response;
                vm.modal.modal('toggle');
            }
        }

        function register(isRegister) {
            vm.modeLogin = !isRegister;
        }
    }

})();

