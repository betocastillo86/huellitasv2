﻿(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('AdoptionFormDetailController', AdoptionFormDetailController);

    AdoptionFormDetailController.$inject = [
        '$scope',
        '$routeParams',
        '$location',
        'adoptionFormService',
        'helperService',
        'adoptionFormStatusService',
        'modalService',
        'adoptionFormAnswerService',
        'routingService',
        'authenticationService'];

    function AdoptionFormDetailController(
        $scope,
        $routeParams,
        $location,
        adoptionFormService,
        helperService,
        adoptionFormStatusService,
        modalService,
        adoptionFormAnswerService,
        routingService,
        authenticationService) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.model = {};
        vm.answer = {};
        vm.statuses = [];
        vm.showAnswers = false;

        vm.send = send;
        vm.seePreviousAnswers = seePreviousAnswers;
        vm.showHelp = showHelp;

        activate();

        function activate() {
            validateAuthentication();
            getStatus();

            $scope.$parent.root.seo.title = app.Settings.resources['Seo.AdoptionFormDetail.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.AdoptionFormDetail.Description'];
        }

        function getStatus() {
            adoptionFormStatusService.getAll()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.statuses = _.reject(response, function (s) { return s.enum === 'None'; });
            }
        }

        function validateAuthentication() {
            authenticationService.showLogin($scope)
                .then(getForm)
                .catch(authenticationError);
        }

        function getForm() {
            adoptionFormService.getById(vm.id)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.model = response;

                if (!vm.model.alreadyOpened) {
                    markAsOpened();
                }

                for (var i = 0; i < vm.model.answers.length; i++) {
                    vm.model.answers[i].statusName = _.findWhere(vm.statuses, { enum: vm.model.answers[i].status }).name;
                }
            }
        }

        function markAsOpened() {
            adoptionFormService.markAsOpened(vm.id)
                .then(markCompleted)
                .catch(helperService.handleException);

            function markCompleted(response) {
                console.log('read');
            }
        }

        function authenticationError() {
            $location.path(routingService.getRoute('home'));
        }

        function send() {
            if (vm.form.$valid && !vm.form.isBusy) {
                modalService.showDialog({
                    message: 'Despues de aceptar la respuesta de este formulario será enviada al solicitante. ¿Estás seguro de la respuesta?',
                    closed: modalClosed
                });
            }

            function modalClosed(response) {
                if (response.accept) {
                    vm.form.isBusy = true;
                    vm.answer.adoptionFormId = vm.id;

                    adoptionFormAnswerService.post(vm.answer)
                        .then(postCompleted)
                        .catch(postError);
                }

                function postCompleted(response) {
                    modalService.show({
                        message: 'Formulario respondido correctamente',
                        redirectAfterClose: routingService.getRoute('forms')
                    });
                    vm.form.isBusy = false;

                    vm.answer = {};
                    vm.form.$submitted = false;
                }

                function postError(response) {
                    helperService.handleException(response);
                    vm.form.isBusy = false;
                }
            }
        }

        function seePreviousAnswers() {
            vm.showAnswers = true;
        }

        function showHelp(message) {
            modalService.show({message: message });
        }
    }
})();