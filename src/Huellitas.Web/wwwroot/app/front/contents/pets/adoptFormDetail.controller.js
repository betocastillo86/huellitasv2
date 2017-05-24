
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('AdoptionFormDetailController', AdoptionFormDetailController);

    AdoptionFormDetailController.$inject = [
        '$routeParams',
        'adoptionFormService',
        'helperService',
        'adoptionFormStatusService',
        'modalService',
        'adoptionFormAnswerService',
        'routingService'];

    function AdoptionFormDetailController(
        $routeParams,
        adoptionFormService,
        helperService,
        adoptionFormStatusService,
        modalService,
        adoptionFormAnswerService,
        routingService) {

        var vm = this;
        vm.id = $routeParams.id;
        vm.model = {};
        vm.answer = {};
        vm.statuses = [];
        vm.showAnswers = false;

        vm.send = send;
        vm.seePreviousAnswers = seePreviousAnswers;

        activate();

        function activate()
        {
            getStatus();
        }

        function getStatus() {
            adoptionFormStatusService.getAll()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.statuses = _.reject(response, function (s) { return s.enum == 'None'; }) ;
                getForm();
            }
        }

        function getForm()
        {
            adoptionFormService.getById(vm.id)
                .then(getCompleted)
                .catch(helperService.notFound);

            function getCompleted(response)
            {
                vm.model = response;

                for (var i = 0; i < vm.model.answers.length; i++) {
                    vm.model.answers[i].statusName = _.findWhere(vm.statuses, { enum: vm.model.answers[i].status }).name;
                }
            }
        }

        function send()
        {
            if (vm.form.$valid && !vm.form.isBusy)
            {
                modalService.showDialog({
                    message: 'Despues de aceptar la respuesta de este formulario será enviada al solicitante. ¿Estás seguro de la respuesta?',
                    closed: modalClosed
                });

                function modalClosed(response)
                {
                    if (response.accept)
                    {
                        vm.form.isBusy = true;
                        vm.answer.adoptionFormId = vm.id;

                        adoptionFormAnswerService.post(vm.answer)
                            .then(postCompleted)
                            .catch(postError);

                        function postCompleted(response)
                        {
                            modalService.show({
                                message: 'Formulario respondido correctamente',
                                redirectAfterClose: routingService.getRoute('forms')
                            });
                            vm.form.isBusy = false;

                            vm.answer = {};
                            vm.form.$submitted = false;
                        }

                        function postError(response)
                        {
                            helperService.handleException(response);
                            vm.form.isBusy = false;
                        }
                    }
                }
            }
        }

        function seePreviousAnswers()
        {
            vm.showAnswers = true;
        }
    }
})();
