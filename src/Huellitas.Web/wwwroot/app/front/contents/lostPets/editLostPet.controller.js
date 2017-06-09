(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('EditLostPetController', EditLostPetController);

    EditLostPetController.$inject = [
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
        'authenticationService',
        'contentService'];

    function EditLostPetController(
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
        authenticationService,
        contentService) {

        var vm = this;
        vm.friendlyName = $routeParams.friendlyName;
        vm.model = {};
        vm.years = undefined;
        vm.years = undefined;
        vm.defaultNameImage = '';
        vm.originalPhone = undefined;
        vm.originalLocation = undefined;
        vm.canChangePhone = true;
        vm.breedTable = app.Settings.customTables.breed;
        vm.maxdate = moment().toDate();
        vm.progressFiles = [];
        
        vm.genres = app.Settings.genres;
        vm.sizes = app.Settings.sizes;
        vm.subtypes = app.Settings.subtypes;

        vm.isSubtypeChecked = isSubtypeChecked;
        vm.changeLocation = changeLocation;
        vm.imageAdded = imageAdded;
        vm.canShowGallery = canShowGallery;
        vm.getFullNameImage = getFullNameImage;
        vm.changeSubtype = changeSubtype;
        vm.removeFile = removeFile;
        vm.save = save;
        vm.reorder = reorder;
        vm.changeBreed = changeBreed;
        vm.imageOnProgress = imageOnProgress;

        activate();

        function activate() {

            vm.currentUser = sessionService.isAuthenticated() ? sessionService.getCurrentUser() : {};

            $scope.$parent.root.seo.title = app.Settings.resources['Seo.EditLostPet.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.EditLostPet.Description'];

            if (vm.friendlyName) {
                getPet();
            }
            else {
                vm.model.files = [];
                setUser(vm.currentUser);
            }
        }

        function setUser(user)
        {
            if (user)
            {
                vm.model.user = user;
                vm.model.location = vm.model.location ? vm.model.location : user.location;
                vm.originalPhone = user.phone;
                vm.originalLocation = user.location ? user.location.id : undefined;
            }
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

                vm.canChangePhone = vm.currentUser.id === vm.model.user.id;
                getFullNameImage();

                if (!vm.canChangePhone) {
                    getParents();
                }
                else
                {
                    setUser(vm.currentUser)
                }
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

        function save() {

            if (!vm.model.breed)
            {
                $scope.$broadcast('angucomplete-alt:clearInput', 'breed');
            }

            if (vm.form.$valid && !vm.form.isBusy) {

                vm.form.isBusy = true;

                authenticationService.showLogin($scope)
                    .then(authenticationCompleted)
                    .catch(authenticationError);

                function authenticationCompleted(response)
                {
                    vm.currentUser = response;

                    if (vm.model.files.length < 2) {
                        modalService.showError({ message: 'Debes cargar al menos dos imagenes' });
                        vm.form.isBusy = false;
                        return;
                    }

                    var newPhone = vm.model.user.phone;
                    var newLocation = vm.model.location;
                    vm.model.user = vm.currentUser;
                    vm.model.user.phone = newPhone;
                    vm.model.user.location = newLocation;

                    if (vm.friendlyName) {
                        petService.put(vm.model)
                            .then(updateUserPhone)
                            .catch(errorSaving);
                    }
                    else {
                        vm.model.type = 'LostPet';
                        vm.model.months = 1;
                        
                        vm.model.parents = [{ userid: vm.model.user.id, relationType: 'Parent' }];

                        petService.post(vm.model)
                            .then(updateUserPhone)
                            .catch(errorSaving);
                    }

                    function updateUserPhone() {
                        if (vm.canChangePhone && (vm.currentUser.phone !== vm.originalPhone || vm.originalLocation !== vm.currentUser.location.id)) {
                            
                            userService.put(vm.currentUser)
                                .then(confirmSaved)
                                .catch(putUserError);
                        }
                        else {
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
                                redirectAfterClose: routingService.getRoute('lostpet', { friendlyName: vm.model.friendlyName })
                            });
                        }
                        else {
                            modalService.show({
                                title: 'Mascota guardada',
                                message: 'Muchas gracias por dejar tus datos, validarémos la información y aprobarémos la huellita pronto. Debes estar pendiente. Si tienes dudas <a href="' + routingService.getRoute('contact') + '" target="_blank">escribenos a Facebook dando clic aquí</a>.',
                                redirectAfterClose: routingService.getRoute('lostpets')
                            });

                            helperService.trackGoal('LostPets', 'Request');
                        }

                        vm.form.isBusy = false;
                    }

                    function errorSaving(response)
                    {
                        vm.form.isBusy = false;
                        helperService.handleException(response);
                    }

                }

                function authenticationError()
                {
                    vm.form.isBusy = false;
                    console.log('No autenticado');
                }
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

        function changeBreed(selectedBreed) {
            vm.model.breed = selectedBreed ? { value: selectedBreed.originalObject.id, text: selectedBreed.originalObject.value } : undefined;
        }

        function changeSubtype(index) {
            vm.model.subtype = {
                value: vm.subtypes[index].id,
                text: vm.subtypes[index].value
            };
            getFullNameImage();
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

        function imageOnProgress(progressFiles)
        {
            vm.progressFiles = progressFiles;
        }

        function imageAdded(image) {
            if (vm.model.id) {
                fileService.postContentFile(vm.model.id, image)
                    .then(postCompleted);

                function postCompleted(response) {
                    vm.model.files.push(image);
                }
            }
            else {
                vm.model.files = vm.model.files || [];
                vm.model.files.push(image);
            }
        }


        function reorder(newFiles) {
            vm.model.files = newFiles;
        }
    }
})();
