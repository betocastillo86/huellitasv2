(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('EditShelterController', EditShelterController);

    EditShelterController.$inject = [
        '$location',
        '$routeParams',
        '$scope',
        'shelterService',
        'helperService',
        'modalService',
        'routingService',
        'fileService',
        'authenticationService'];

    function EditShelterController(
        $location,
        $routeParams,
        $scope,
        shelterService,
        helperService,
        modalService,
        routingService,
        fileService,
        authenticationService) {

        var vm = this;
        vm.model = {};
        vm.model.files = [];
        vm.progressFiles = [];

        vm.friendlyName = $routeParams.friendlyName;

        vm.changeLocation = changeLocation;
        vm.canShowGallery = canShowGallery;
        vm.getFullNameImage = getFullNameImage;
        vm.imageAdded = imageAdded;
        vm.removeFile = removeFile;
        vm.reorder = reorder;
        vm.save = save;
        vm.imageOnProgress = imageOnProgress;

        $scope.$parent.root.seo.title = app.Settings.resources['Seo.EditShelter.Title'];
        $scope.$parent.root.seo.description = app.Settings.resources['Seo.EditShelter.Description'];

        activate();

        function activate()
        {
            if (vm.friendlyName)
            {
                getShelter();
            }
        }

        function getShelter()
        {
            shelterService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.handleError);

            function getCompleted(response)
            {
                vm.model = response;
            }
        }

        function changeLocation(selectedLocation)
        {
            vm.model.location = vm.model.location || {};
            vm.model.location = selectedLocation ? selectedLocation.originalObject : undefined;
            getFullNameImage();
        }

        function getFullNameImage() {
            if (canShowGallery()) {
                return vm.defaultNameImage = vm.model.name + ' ' + vm.model.location.name;
            }
            else {
                return '';
            }
        }

        function canShowGallery() {
            return vm.model.name && vm.model.location;
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

                function postCompleted(response) {
                    vm.model.files.push(image);
                }
            }
            else {
                vm.model.files = vm.model.files || [];
                vm.model.files.push(image);
            }
        }

        function reorder(newFiles)
        {
            vm.model.files = newFiles;
        }

        function save()
        {
            if (vm.form.$submitted && vm.form.$valid && !vm.form.isBusy) {

                vm.form.isBusy = true;

                authenticationService.showLogin($scope)
                    .then(authenticationCompleted)
                    .catch(authenticationError);

                function authenticationCompleted(responseAuth)
                {
                    var currentUser = responseAuth;

                    if (vm.model.files.length < 3) {
                        modalService.showError({ message: 'Debes cargar al menos tres imágenes de tu fundación', title: 'Muy pocas imágenes!' });
                        vm.form.isBusy = false;
                        return;
                    }

                    if (vm.friendlyName) {
                        shelterService.put(vm.model)
                            .then(postCompleted)
                            .catch(postError);
                    }
                    else {
                        vm.model.user = currentUser;
                        vm.model.users = [{ userid: vm.model.user.id, relationType: 'Shelter' }];

                        shelterService.post(vm.model)
                            .then(postCompleted)
                            .catch(postError);
                    }

                    function postCompleted(response) {

                        if (!vm.friendlyName)
                        {
                            helperService.trackGoal('Shelters', 'Request');
                        }
                        
                        vm.form.isBusy = false;
                        if (vm.friendlyName) {
                            modalService.show({
                                message: 'La fundación fue correctamente actualizada',
                                redirectAfterClose: routingService.getRoute('shelter', { friendlyName: vm.friendlyName })
                            });
                        }
                        else {
                            modalService.show({
                                message: 'Muchas gracias por registrar tu fundación en Huellitas sin Hogar. Pronto recibirás noticias de nosotros.',
                                redirectAfterClose: routingService.getRoute('shelters')
                            });
                        }
                    }

                    function postError(response) {
                        vm.form.isBusy = false;
                        modalService.showError({ message: 'No pudimos guardar los datos. Intenta de nuevo o comunicate con nosotros por medio de Facebook' });
                    }
                }

                function authenticationError()
                {
                    console.log('No autenticado');
                    vm.form.isBusy = false;
                }
            }
            else {
                helperService.goToFocusError();
            }
        }

        function imageOnProgress(progressFiles) {
            vm.progressFiles = progressFiles;
        }
    }
})();
