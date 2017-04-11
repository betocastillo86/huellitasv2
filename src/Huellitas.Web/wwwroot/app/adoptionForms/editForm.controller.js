(function () {
    angular.module('app')
        .controller('EditFormController', EditFormController);

    EditFormController.$inject = ['$routeParams', 'adoptionFormService', 'adoptionFormAnswerService', 'modalService']

    function EditFormController($routeParams, adoptionFormService, adoptionFormAnswerService, modalService) {
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
                .catch(getError);

            function getCompleted(response) {
                vm.model = response.data;
            }

            function getError() {
                console.log('Error get form by id');
            }
        }

        function getAnswers() {
            adoptionFormAnswerService.getByFormId(vm.id)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(response) {
                vm.model.answers = response.data;
            }

            function getError() {
                console.log('Error con respuestas');
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
                template : '/app/adoptionForms/addFormUser.html'
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
                    modalService.showError({error:response.data.error});
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
                template: '/app/adoptionForms/sendFormByEmail.html'
            });
        }
    }
})();