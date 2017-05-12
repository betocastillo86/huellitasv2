(function () {
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
        'sessionService'];

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
        sessionService) {

        var vm = this;
        vm.friendlyName = $routeParams.friendlyName;
        vm.model = {};
        vm.years = undefined;
        vm.years = undefined;
        vm.defaultNameImage = '';
        vm.originalPhone = undefined;
        vm.canChangePhone = true;

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
        vm.currentUser = sessionService.isAuthenticated() ? sessionService.getCurrentUser() : {};

        activate();

        function activate() {

            if (vm.friendlyName) {
                getPet();
            }
            else {
                vm.model.location = vm.currentUser.location;
                vm.model.files = [];
            }

            vm.originalPhone = vm.currentUser.phone;
        }

        function getPet() {
            petService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.model = response;
                vm.years = Math.floor(vm.model.months / 12);
                vm.months = vm.model.months % 12;
                vm.canChangePhone = vm.currentUser.id === vm.model.user.id;
                getFullNameImage();
            }
        }

        function save() {
            if (vm.form.$valid && !vm.form.isBusy) {
                if (vm.model.files.length < 3) {
                    modalService.showError({ message: 'Debes cargar al menos tres imagenes' });
                    return;
                }

                if (vm.friendlyName) {
                    petService.put(vm.model)
                        .then(updateUserPhone)
                        .catch(helperService.handleException);
                }
                else {

                    vm.model.user = vm.currentUser;
                    vm.model.parents = [{ userid: vm.model.user.id, relationType: 'Parent' }];

                    petService.post(vm.model)
                        .then(updateUserPhone)
                        .catch(helperService.handleException);
                }

                function updateUserPhone() {
                    if (vm.originalPhone !== vm.currentUser.phone) {
                        userService.put($scope.root.currentUser)
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
                            redirectAfterClose: routingService.getRoute('pet', { friendlyName: vm.model.friendlyName })
                        });
                    }
                    else {
                        modalService.show({
                            title: 'Mascota guardada',
                            message: 'Muchas gracias por dejar tus datos, validarémos la información y aprobarémos la huellita pronto. Debes estar pendiente. Si tienes dudas escribenos a Facebook.',
                            redirectAfterClose: routingService.getRoute('pets')
                        });
                    }
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

            function confirmRemoved()
            {
                vm.model.files = _.reject(vm.model.files, function (el) { return el.id == image.id });
            }
        }

        function imageAdded(image) {
            if (vm.model.id) {
                fileService.postContentFile(vm.model.id, image)
                    .then(postCompleted);

                function postCompleted(response)
                {
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
