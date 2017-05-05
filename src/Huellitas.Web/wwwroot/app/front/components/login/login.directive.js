
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

    LoginController.$inject = ['$scope', 'authenticationService', 'modalService'];

    function LoginController($scope, authenticationService, modalService) {
        var vm = this;
        vm.model = {};
        vm.modal = undefined;


        vm.authenticate = authenticate;

        activate();

        function activate() {
           vm.modal = $('#loginModal').modal();
        }

        function authenticate() {
            if (vm.form.$valid && !vm.form.isBusy) {
                vm.form.isBusy = true;

                authenticationService.post(vm.model)
                    .then(postCompleted)
                    .catch(postError);
            }

            function postCompleted(response) {
                vm.form.isBusy = false;
                $scope.root.currentUser = response;
                vm.modal.modal('toggle');
            }

            function postError(response) {
                vm.form.isBusy = false;

                if (response.status === 401) {
                    modalService.showError({ message: 'Usuario y clave invalidos', isFront: true });
                }

                console.log(response.error);
            }
        }
    }

})();

