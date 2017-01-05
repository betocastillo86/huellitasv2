(function () {
    angular.module('app')
        .controller('EditShelterController', EditShelterController);

    EditShelterController.$inject = ['$routeParams', '$location', 'shelterService', 'statusTypeService', 'modalService'];

    function EditShelterController($routeParams, $location, shelterService, statusTypeService, modalService)
    {
        var vm = this;
        vm.id = $routeParams.id
        vm.model = {};
        vm.model.status = app.Settings.statusTypes.published;
        vm.showMoreActive = false;
        vm.regexYoutube = '^(https?\:\/\/)?(www\.)?(youtube\.com|youtu\.?be)\/.+$';
        vm.regexFacebook = 'http(?:s)?:\/\/(?:www\.)?facebook\.com\/([a-zA-Z0-9_\?\=\/]+)';
        vm.regexTwitter = 'http(?:s)?:\/\/(?:www\.)?twitter\.com\/([a-zA-Z0-9_\?\=\/]+)';
        vm.regexInstagram = 'http(?:s)?:\/\/(?:www\.)?instagram\.com\/([a-zA-Z0-9_\?\=\/]+)';
        vm.continueAfterSaving = false;
        vm.showPicturesActive = false;

        vm.changeFeatured = changeFeatured;
        vm.changeAutoReply = changeAutoReply;
        vm.isInvalidClass = isInvalidClass;
        vm.toogleShowMore = toogleShowMore;
        vm.activeTooggleClass = activeTooggleClass;
        vm.saveAndContinue = saveAndContinue;
        vm.changeLocation = changeLocation;
        vm.removeImage = removeImage;
        vm.imageAdded = imageAdded;
        vm.save = save;

        activate();

        return vm;

        function activate() {
            if (vm.id) {
                getShelterById(vm.id);
            }
            else {
                vm.showPicturesActive = true;
            }

            getStatusTypes();
        }

        function getStatusTypes() {
            statusTypeService.getAll()
                .then(statusTypesCompleted);

            function statusTypesCompleted(rows) {
                vm.statusTypes = rows;
            }

            function statusTypesError() {
                console.log(error, arguments);
            }
        }

        function getShelterById(id)
        {
            shelterService.getById(id)
                .then(getCompleted)
                .catch(getError);

            function getCompleted(model)
            {
                vm.model = model.data;
                vm.showPicturesActive = true;
            }

            function getError()
            {
                debugger;
            }
        }

        function saveAndContinue() {
            vm.continueAfterSaving = true;
        }

        function activeTooggleClass(indexValue, actualValue) {
            return indexValue == actualValue ? 'btn-primary' : 'btn-default';
        }

        function isInvalidClass(form, field) {
            return form.$submitted && !field.$valid ? 'parsley-error' : undefined;
        }

        function toogleShowMore() {
            vm.showMoreActive = !vm.showMoreActive;
        }

        function changeLocation(selectedLocation) {
            vm.model.location = vm.model.location || {};
            vm.model.location.id = selectedLocation ? selectedLocation.originalObject.id : undefined;
        }

        function removeImage(image) {
            if (vm.model.id) {
                fileService.deleteContentFile(vm.model.id, image.id);
            }
            else {
                vm.model.files = _.reject(vm.model.files, function (el) { return el.id == image.id });
            }
        }

        function changeFeatured(featured) {
            vm.model.featured = featured;
        }

        function changeAutoReply(autoReply) {
            vm.model.autoReply = autoReply;
        }

        function imageAdded(image) {
            if (vm.model.id) {
                fileService.postContentFile(vm.model.id, image);
            }
            else {
                vm.model.files = vm.model.files || [];
                vm.model.files.push(image);
            }
        }

        function save(isValid) {
            if (isValid) {
                if (vm.model.id > 0) {
                    shelterService.put(vm.model)
                    .then(saveCompleted)
                    .catch(saveError);
                }
                else {
                    shelterService.post(vm.model)
                    .then(saveCompleted)
                    .catch(saveError);
                }
            }

            function saveCompleted(response) {
                response = response.data;
                var message = 'El refugio fue actualizado correctamente';
                var isNew = !vm.model.id;

                if (isNew) {
                    message = 'El refugio se ha creado correctamente';
                    vm.model.id = response.id;
                }

                modalService.show({
                    message: message
                })
                .then(function (modal) {
                    modal.closed.then(function () {
                        if (vm.continueAfterSaving) {
                            //if it is new and want to continue updates the location
                            if (isNew) {
                                $location.path('/shelters/' + vm.model.id + '/edit');
                            }
                        }
                        else {
                            $location.path('/shelters');
                        }

                        vm.continueAfterSaving = false;
                    });
                });
            }


            function saveError(response) {
                modalService.showError({
                    error: response.data.error
                });
            }
        }

    }
})();