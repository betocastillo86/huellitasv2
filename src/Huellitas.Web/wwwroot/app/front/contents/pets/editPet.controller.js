﻿(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('EditPetController', EditPetController);

    EditPetController.$inject = [
        '$routeParams',
        '$scope',
        '$location',
        'helperService',
        'petService',
        'userService',
        'routingService',
        'modalService',
        'fileService',
        'sessionService',
        'contentService',
        'authenticationService'];

    function EditPetController(
        $routeParams,
        $scope,
        $location,
        helperService,
        petService,
        userService,
        routingService,
        modalService,
        fileService,
        sessionService,
        contentService,
        authenticationService) {
        var vm = this;
        vm.friendlyName = $routeParams.friendlyName;
        vm.model = {};
        vm.years = undefined;
        vm.years = undefined;
        vm.defaultNameImage = '';
        vm.originalPhone = undefined;
        vm.originalLocation = undefined;
        vm.canChangePhone = true;
        vm.shelters = [];
        vm.showNotLogged = false;
        vm.progressFiles = [];
        vm.previousPage = undefined;
        vm.backLink = undefined;

        vm.genres = app.Settings.genres;
        vm.sizes = app.Settings.sizes;
        vm.subtypes = app.Settings.subtypes;

        vm.changeMonths = changeMonths;
        vm.isSubtypeChecked = isSubtypeChecked;
        vm.changeLocation = changeLocation;
        vm.imageAdded = imageAdded;
        vm.canShowGallery = canShowGallery;
        vm.getFullNameImage = getFullNameImage;
        vm.changeSubtype = changeSubtype;
        vm.removeFile = removeFile;
        vm.save = save;
        vm.reorder = reorder;
        vm.validateAuthentication = validateAuthentication;
        vm.imageOnProgress = imageOnProgress;
        vm.chageShelter = chageShelter;

        activate();

        function activate() {
            vm.currentUser = sessionService.isAuthenticated() ? sessionService.getCurrentUser() : {};
            vm.previousPage = $scope.$parent.root.previousPages[$scope.$parent.root.previousPages.length - 2];

            if (vm.previousPage == routingService.getRoute('newpet0')) {
                vm.backLink = routingService.getRoute('pets');
            }
            else if (vm.previousPage == routingService.getRoute('mypets')) {
                vm.backLink = vm.previousPage;
            }
            else {
                vm.backLink = vm.friendlyName ? routingService.getRoute('pet', { friendlyName: vm.friendlyName }) : routingService.getRoute('pets');
            }

            validateAuthentication();
        }

        function validateAuthentication() {
            authenticationService.showLogin($scope)
                .then(authenticationCompleted)
                .catch(authenticationError);
        }

        function chageShelter() {
            var shelter = _.findWhere(vm.shelters, { id: vm.model.shelter.id });
            vm.model.location = shelter ? shelter.location : undefined;

            getFullNameImage();
        }

        function authenticationCompleted(userAuthenticated) {
            vm.currentUser = userAuthenticated;
            setUser(userAuthenticated);

            $scope.$parent.root.seo.title = app.Settings.resources['Seo.EditPet.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.EditPet.Description'];

            if (vm.friendlyName) {
                getPet();
            }
            else {
                vm.model.location = vm.currentUser.location;
                vm.model.files = [];
            }

            vm.showNotLogged = false;
            getShelters();
        }

        function setUser(user) {
            if (user) {
                vm.model.user = user;
                vm.model.location = vm.model.location ? vm.model.location : user.location;
                vm.originalPhone = user.phone;
                vm.originalLocation = user.location ? user.location.id : undefined;
            }
        }

        function authenticationError() {
            vm.showNotLogged = true;
        }

        function getPet() {
            petService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.model = response;

                if (!vm.model.canEdit) {
                    helperService.notFound();
                }

                vm.years = Math.floor(vm.model.months / 12);
                vm.months = vm.model.months % 12;
                vm.canChangePhone = vm.currentUser.id === vm.model.user.id;

                if (!vm.canChangePhone) {
                    getParents();
                }
                else {
                    setUser(vm.currentUser)
                }

                getFullNameImage();
            }
        }

        function getParents() {
            contentService.getUsers(vm.model.id, { relationType: 'Parent' })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                setUser(response.results[0]);
            }
        }

        function getShelters() {
            var userId = sessionService.getCurrentUser().id;

            contentService.getContentsOfUser(userId, { relationType: 'Shelter', pageSize: 20 })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.shelters = response.results;
            }
        }

        function save() {
            if (vm.form.$valid && !vm.form.isBusy) {
                if (vm.model.files.length < 3) {
                    modalService.showError({ message: 'Debes cargar al menos tres imagenes' });
                    return;
                }

                if (vm.progressFiles.length) {
                    modalService.showError({ message: 'Las imagenes no han terminado de cargar, espera unos segundos y cuando termine dale clic nuevamente en guardar.', title: 'Imagenes cargando' });
                    vm.form.isBusy = false;
                    return;
                }

                vm.form.isBusy = true;

                var newPhone = vm.model.user.phone;
                var newLocation = vm.model.location;
                vm.model.user = vm.currentUser;
                vm.model.user.phone = newPhone;
                vm.model.user.location = newLocation;

                if (vm.friendlyName) {
                    petService.put(vm.model)
                        .then(updateUserPhone)
                        .catch(updateError);
                }
                else {
                    vm.model.type = 'Pet';

                    vm.model.parents = [{ userid: vm.model.user.id, relationType: 'Parent' }];

                    petService.post(vm.model)
                        .then(updateUserPhone)
                        .catch(updateError);
                }
            }

            function updateUserPhone() {

                try {
                    if (vm.canChangePhone && (vm.originalPhone !== vm.currentUser.phone || vm.originalLocation !== vm.currentUser.location.id)) {
                        userService.put(vm.currentUser)
                            .then(confirmSaved)
                            .catch(putUserError);
                    }
                    else {
                        confirmSaved();
                    }
                } catch (e) {
                    confirmSaved();
                }

                function putUserError() {
                    modalService.showError({
                        message: 'La mascota fue actualizada correctamente, pero ocurrió un error guardando el número telefónico, actualizalo por tus datos personales',
                        redirectAfterClose: routingService.getRoute('myaccount')
                    });
                }
            }

            function confirmSaved() {
                if (vm.friendlyName) {
                    modalService.show({
                        title: 'Mascota actualizada',
                        message: 'Los datos de ' + vm.model.name + ' fueron actualizados correctamente',
                        redirectAfterClose: routingService.getRoute('pet', { friendlyName: vm.model.friendlyName })
                    });
                }
                else {
                    modalService.show({
                        title: 'Mascota guardada',
                        message: 'Muchas gracias por dejar tus datos, validarémos la información y aprobarémos la huellita pronto. Debes estar pendiente. Si tienes dudas <a href="' + routingService.getRoute('contact') + '" target="_blank">escribenos a Facebook clic aquí<a>.',
                        redirectAfterClose: routingService.getRoute('pets')
                    });

                    helperService.trackGoal('Pets', 'Request');
                }

                vm.form.isBusy = false;
            }

            function updateError(response) {
                vm.form.isBusy = false;
                helperService.handleException(response);
            }
        }

        function isSubtypeChecked(index) {
            if (vm.model.subtype) {
                return vm.model.subtype.value == vm.subtypes[index].id;
            }
            else {
                return false;
            }
        }

        function changeSubtype(index) {
            vm.model.subtype = {
                value: vm.subtypes[index].id,
                text: vm.subtypes[index].value
            };
            getFullNameImage();
        }

        function changeMonths() {
            vm.model.months = ((vm.years ? vm.years : 0) * 12) + (vm.months ? vm.months : 0);
        }

        function changeLocation(selectedLocation) {
            vm.model.location = vm.model.location || {};
            vm.model.location = selectedLocation ? selectedLocation.originalObject : undefined;
            getFullNameImage();
        }

        function getFullNameImage() {
            if (canShowGallery() && vm.sizes) {
                var size = _.findWhere(vm.sizes, { id: vm.model.size.value });
                var genre = _.findWhere(vm.genres, { id: vm.model.genre.value });
                return vm.defaultNameImage = vm.model.subtype.text + ' ' + genre.value + ' ' + size.value + ' ' + vm.model.name + ' ' + vm.model.location.name;
            }
            else {
                return '';
            }
        }

        function canShowGallery() {
            return vm.model.subtype && vm.model.genre && vm.model.size && vm.model.name && vm.model.location;
        }

        function removeFile(image) {
            if (vm.model.id) {
                fileService.deleteContentFile(vm.model.id, image.id)
                    .then(confirmRemoved);
            }
            else {
                confirmRemoved();
            }

            function confirmRemoved() {
                vm.model.files = _.reject(vm.model.files, function (el) { return el.id == image.id });
            }
        }

        function imageAdded(image) {
            if (vm.model.id) {
                fileService.postContentFile(vm.model.id, image)
                    .then(postCompleted);
            }
            else {
                vm.model.files = vm.model.files || [];
                vm.model.files.push(image);
            }

            function postCompleted(response) {
                vm.model.files.push(image);
            }
        }

        function imageOnProgress(progressFiles) {
            vm.progressFiles = progressFiles;
        }

        function reorder(newFiles) {
            vm.model.files = newFiles;
        }
    }
})();