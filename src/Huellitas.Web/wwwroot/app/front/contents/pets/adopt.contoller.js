
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('AdoptController', AdoptController);

    AdoptController.$inject = [
        '$routeParams',
        '$scope',
        '$location',
        'customTableRowService',
        'helperService',
        'adoptionFormService',
        'petService',
        'routingService',
        'modalService'];

    function AdoptController(
        $routeParams,
        $scope,
        $location,
        customTableRowService,
        helperService,
        adoptionFormService,
        petService,
        routingService,
        modalService) {

        var vm = this;
        vm.friendlyName = $routeParams.friendlyName;
        vm.currentStep = 0;
        vm.questions = [];
        vm.jobs = [];
        vm.model = {};
        vm.pet = {};
        vm.ageFamilyMembers = [];
        


        vm.changeLocation = changeLocation;
        vm.ageChanged = ageChanged;
        vm.changeCheckboxType = changeCheckboxType;
        vm.familyMembersChanged = familyMembersChanged;
        vm.changeOptionsWithTextType = changeOptionsWithTextType;
        vm.save = save;
        vm.back = back;

        activate();

        function activate() {
            getPet();
            getJobs();
            getQuestions();
        }

        function getQuestions() {
            customTableRowService.getAdoptionFormQuestions()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.questions = response;
                vm.model.name = $scope.root.currentUser.name;
                vm.model.phoneNumber = $scope.root.currentUser.phone;
                vm.model.email = $scope.root.currentUser.email;
                vm.model.location = $scope.root.currentUser.location;

                var questionsByStep = Math.ceil(vm.questions.length / 3);
                var step = 0;
                for (var i = 0; i < vm.questions.length; i++) {
                    if (i % questionsByStep == 0) {
                        step++;
                    }

                    var question = vm.questions[i];
                    question.template = 'template' + question.questionType;
                    question.key = 'question' + question.id;

                    if (question.questionType == 'ChecksWithText' || question.questionType == 'OptionsWithText') {
                        //mirar lo de los checks con texto

                        question.checks = {};
                        ////Se crean atributos adicionales que permiten cambiar los valores de los checks
                        for (var j = 0; j < question.options.length; j++) {
                            question.checks[question.options[j]] = {
                                text: '',
                                checked: false,
                                option: question.options[j]
                            };
                        }
                    }

                    if (question.questionParentId) {
                        var parentQuestion = _.findWhere(vm.questions, { id: question.questionParentId });
                        parentQuestion.children = new Array();
                        parentQuestion.children.push(question);
                    }
                    else {
                        question.step = step;
                    }
                }

                console.log(vm.questions);

            }
        }

        function getJobs() {
            customTableRowService.getJobs()
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.jobs = response.results;
            }
        }

        function getPet() {
            petService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.pet = response;
                vm.model.contentId = response.id;
            }
        }

        function changeLocation(selectedLocation) {
            vm.model.location = vm.model.location || {};
            vm.model.location = selectedLocation ? selectedLocation.originalObject : undefined;
        }

        function familyMembersChanged() {
            vm.ageFamilyMembers = new Array(vm.model.familyMembers);
        }

        function ageChanged() {
            vm.model.familyMembersAge = vm.ageFamilyMembers.join(',');
        }

        function save() {
            if (vm.form.$valid && !vm.form.isBusy) {

                if (vm.currentStep < 3) {
                    vm.currentStep++;
                    vm.form.$submitted = false;
                    helperService.goToFocus('.tit-form');
                    return;
                }

                modalService.showDialog({
                    message: 'Despues de este paso toda la información será enviada. ¿Confirmas todos los datos ingresados?',
                    closed: confirmClosed
                });

                function confirmClosed(response) {
                    if (response.accept)
                    {
                        vm.form.isBusy = true;

                        vm.model.attributes = new Array();
                        for (var i = 0; i < vm.questions.length; i++) {
                            var question = vm.questions[i];

                            if (!question.questionParentId) {
                                vm.model.attributes.push({
                                    attributeId: question.id,
                                    value: question.answer,
                                    question: question.question
                                });

                                if (question.children) {
                                    for (var j = 0; j < question.children.length; j++) {
                                        vm.model.attributes.push({
                                            attributeId: question.children[j].id,
                                            value: question.children[j].answer,
                                            question: question.children[j].question
                                        });
                                    }
                                }
                            }
                        }

                        adoptionFormService.post(vm.model)
                            .then(postCompleted)
                            .catch(postError);

                        function postCompleted() {
                            vm.form.isBusy = false;

                            modalService.show({
                                message: "Muchas gracias por llenar el formulario. Debes estar pendiente de tu correo donde enviarémos la respuesta.",
                                redirectAfterClose: routingService.getRoute('pet', { friendlyName: vm.friendlyName })
                            });
                        }

                        function postError() {
                            vm.form.isBusy = false;
                            modalService.showError({
                                message: "Ocurrió un error al envíar la información. Intenta de nuevo o escribenos a nuestro fan page"
                            });
                        }
                    }
                }
            }
            else if (vm.form.$invalid) {
                helperService.goToFocusError();
            }
        }

        function back() {
            if (vm.currentStep > 0) {
                helperService.goToFocus('.tit-form');
                vm.currentStep--;
            }
            else {
                modalService.showDialog({
                    message: '¿Seguro deseas salir del formulario?', closed: confirmClosed
                });

                function confirmClosed(response) {
                    if (response.accept) {
                        $location.path(routingService.getRoute('pet', { friendlyName: vm.friendlyName }));
                    }
                }
            }
        }

        function changeCheckboxType(question, option)
        {
            question.answer = _.chain(question.checks)
                .where({ checked: true })
                .map(function (model) { return model.option + ' - ' + model.text })
                .value()
                .join(','); 
        }

        function changeOptionsWithTextType(question, option) {
            var filled = _.filter(question.checks, function (el) {
                return el.text && el.text.trim() != '';
            })

            if (filled.length == question.options.length) {
                question.answer = _.chain(question.checks)
                    .map(function (model) { return model.option + ' - ' + model.text })
                    .value()
                    .join(',');
            }
            else
            {
                question.answer = undefined;
            }
        }
    }
})();
