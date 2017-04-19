﻿(function () {
    angular.module('huellitasAdmin')
        .controller('EditFormController', EditFormController);

    EditFormController.$inject = ['$routeParams', 'adoptionFormService', 'adoptionFormAnswerService', 'modalService', 'helperService']

    function EditFormController($routeParams, adoptionFormService, adoptionFormAnswerService, modalService, helperService) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.model = {};
        vm.answer = {};
        vm.showAttributes = false;
        vm.showForm = false;
        vm.isSending = false;
        vm.showAnswers = false;

        vm.showAddUser = showAddUser;
        vm.showDetailAnswer = showDetailAnswer;
        vm.activeTooggleClass = activeTooggleClass;
        vm.changeAnswerStatus = changeAnswerStatus;
        vm.toogleShowMore = toogleShowMore;
        vm.saveResponse = saveResponse;
        vm.showSendByEmail = showSendByEmail;

        return activate();

        function activate() {
            getForm();
        }

        function getForm() {
            adoptionFormService.getById(vm.id)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.model = response;
            }
        }

        function getAnswers() {
            adoptionFormAnswerService.getByFormId(vm.id)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.model.answers = response;
            }
        }

        function showDetailAnswer(answer) {
            var html = '<h3>Información adicional</h3>' + (answer.additionalInfo ? answer.additionalInfo : '') + '<br><h3>Notas:</h3>' + (answer.notes ? answer.notes : '');
            modalService.show({ message: html, title: 'Información de respuesta' });
        }

        function activeTooggleClass(indexValue, className) {
            return indexValue == vm.answer.status ? className : 'btn-default';
        }

        function changeAnswerStatus(status) {
            vm.answer.status = status;
        }

        function showAddUser()
        {
            modalService.show({
                controller: 'AddFormUserController',
                template : '/app/admin/adoptionForms/addFormUser.html'
            });
        }

        function saveResponse(isValid) {
            if (isValid && !vm.isSending) {
                vm.isSending = true;
                vm.answer.adoptionFormId = vm.id;
                adoptionFormAnswerService.post(vm.answer)
                    .then(postCompleted)
                    .catch(postError);

                function postCompleted(response) {
                    vm.isSending = false;
                    vm.answer = {};
                    getAnswers();
                    modalService.show({ message: 'Mensaje enviado correctamente' });
                }

                function postError(response) {
                    vm.isSending = false;
                    helperService.handleException(response);
                }
            }
        }

        function toogleShowMore(section) {
            switch (section) {
                case 'attributes':
                    vm.showAttributes = !vm.showAttributes;
                    break;
                case 'form':
                    vm.showForm = !vm.showForm;
                    break;
                case 'answers':
                    vm.showAnswers = !vm.showAnswers;
                    break;
            }
        }

        function showSendByEmail()
        {
            modalService.show({
                controller: 'SendFormByEmailController',
                template: '/app/admin/adoptionForms/sendFormByEmail.html'
            });
        }
    }
})();