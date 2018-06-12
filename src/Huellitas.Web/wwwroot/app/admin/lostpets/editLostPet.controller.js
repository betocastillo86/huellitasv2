(function () {
    angular.module('huellitasAdmin')
        .controller('EditLostPetController', EditLostPetController);

    EditLostPetController.$inject = [
        '$routeParams',
        '$location',
        'petService',
        'customTableRowService',
        'statusTypeService',
        'fileService',
        'modalService',
        'contentService',
        'helperService',
        'crawlingService'];

    function EditLostPetController(
        $routeParams,
        $location,
        petService,
        customTableRowService,
        statusTypeService,
        fileService,
        modalService,
        contentService,
        helperService,
        crawlingService) {

        var vm = this;
        vm.id = $routeParams.id;
        vm.saveonreorder = vm.id > 0;
        vm.isSending = false;
        vm.breedTable = app.Settings.customTables.breed;

        vm.model = {};
        vm.parents = [];
        vm.usersFilter = {
            page: 0,
            pageSize: 20,
            relationType: app.Settings.contentRelationTypes.parent
        };

        vm.model.autoReply = true;
        vm.model.featured = false;
        vm.model.status = app.Settings.statusTypes.published;
        vm.defaultNameImage = '';

        vm.showMoreActive = false;
        vm.showPicturesActive = false;

        vm.activeTooggleClass = activeTooggleClass;
        vm.changeGenre = changeGenre;
        vm.changeSubtype = changeSubtype;
        vm.changeLocation = changeLocation;
        vm.toogleShowMore = toogleShowMore;
        vm.toogleShowPictures = toogleShowPictures;
        vm.changeFeatured = changeFeatured;
        vm.removeImage = removeImage;
        vm.imageAdded = imageAdded;
        vm.save = save;
        vm.isInvalidClass = isInvalidClass;
        vm.changeMonths = changeMonths;
        vm.saveAndContinue = saveAndContinue;
        vm.addParent = addParent;
        vm.deleteParent = deleteParent;
        vm.getFullNameImage = getFullNameImage;
        vm.canShowGallery = canShowGallery;
        vm.changeBreed = changeBreed;

        activate();

        function activate() {
            if (vm.id) {
                getPetById(vm.id);
            }
            else {
                vm.model.type = 'Pet';
                vm.showPicturesActive = true;
            }

            getSizes();
            getSubtypes();
            getGenres();
            getStatusTypes();
            getParents();
        }

        function getPetById(id) {
            petService.getById(id)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(model) {
                vm.model = model;
                vm.years = Math.floor(model.months / 12);
                vm.months = model.months % 12;
                vm.showPicturesActive = true;
                vm.model.relatedPets = [];
                vm.getFullNameImage();
                //vm.model.closingDate = moment(vm.model.closingDate, 'YYYY/MM/DD HH:mm:ss').format('YYYY/MM/DD');
            }
        }

        function getParents() {
            if (vm.id) {
                contentService.getUsers(vm.id, vm.usersFilter)
                    .then(getCompleted)
                    .catch(helperService.handleException);

                function getCompleted(response) {
                    vm.parents = response.results;
                }
            }
        }

        function addParent(selected) {
            if (selected) {
                var user = selected.originalObject;

                user.relationType = app.Settings.contentRelationTypes.parent;
                vm.parents = vm.parents || [];
                var contentUser = { userId: user.id, relationType: user.relationType };

                if (vm.id) {
                    contentService.postUser(vm.id, contentUser)
                        .then(postCompleted)
                        .catch(helperService.handleException);

                    function postCompleted() {
                        vm.parents.push(user);
                    }
                }
                else {
                    vm.model.parents = vm.model.parents || [];

                    vm.model.parents.push(contentUser);
                    vm.parents.push(user);
                }
            }
        }

        function deleteParent(user) {
            if (vm.id) {
                if (confirm('¿Seguro deseas eliminar este usuario?')) {
                    contentService.deleteUser(vm.id, user.id)
                        .then(deleteCompleted)
                        .catch(helperService.handleException);

                    function deleteCompleted() {
                        vm.parents = _.reject(vm.parents, function (parent) { return parent.id === user.id; });
                    }
                }
            }
            else {
                vm.parents = _.reject(vm.parents, function (parent) { return parent.id === user.id; });
                vm.model.parents = _.reject(vm.model.parents, function (parent) { return parent.userId === user.id; });
            }
        }

        function getSizes() {
            customTableRowService.getSizes()
                .then(getSizesCompleted)
                .catch(helperService.handleException);

            function getSizesCompleted(rows) {
                vm.sizes = rows.results;
                vm.getFullNameImage();
            }
        }

        function getSubtypes() {
            customTableRowService.getSubtypes()
                .then(getSubtypesCompleted)
                .catch(helperService.handleException);

            function getSubtypesCompleted(rows) {
                vm.subtypes = rows.results;
            }
        }

        function changeBreed(selectedBreed) {
            vm.model.breed = selectedBreed ? { value: selectedBreed.originalObject.id, text: selectedBreed.originalObject.value } : undefined;
        }

        function getGenres() {
            customTableRowService.getGenres()
                .then(getGenresCompleted)
                .catch(helperService.handleException);

            function getGenresCompleted(rows) {
                vm.genres = rows.results;
            }
        }

        function getStatusTypes() {
            statusTypeService.getAll()
                .then(statusTypesCompleted)
                .catch(helperService.handleException);;

            function statusTypesCompleted(rows) {
                vm.statusTypes = rows;
            }
        }

        function activeTooggleClass(indexValue, actualValue) {
            return indexValue === actualValue ? 'btn-primary' : 'btn-default';
        }

        function changeGenre(genreId) {
            var genre = _.findWhere(vm.genres, { id: genreId })
            vm.model.genre = { value: genre.id, text: genre.value };
            vm.getFullNameImage();
        }

        function changeSubtype(subtypeId) {
            var subtype = _.findWhere(vm.subtypes, { id: subtypeId })
            vm.model.subtype = { value: subtype.id, text: subtype.value };
            vm.getFullNameImage();
        }

        function changeLocation(selectedLocation) {
            vm.model.location = vm.model.location || {};
            vm.model.location = selectedLocation ? selectedLocation.originalObject : undefined;
            getFullNameImage();
        }

        function toogleShowMore() {
            vm.showMoreActive = !vm.showMoreActive;
        }

        function toogleShowPictures() {
            vm.showPicturesActive = !vm.showPicturesActive;
        }

        function changeFeatured(featured) {
            vm.model.featured = featured;
        }

        function removeImage(image) {
            if (vm.model.id) {
                fileService.deleteContentFile(vm.model.id, image.id);
            }
            else {
                vm.model.files = _.reject(vm.model.files, function (el) { return el.id === image.id });
            }
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

        function isInvalidClass(form, field) {
            return form.$submitted && !field.$valid ? 'parsley-error' : undefined;
        }

        function getFullNameImage() {
            if (canShowGallery() && vm.sizes) {
                var size = _.findWhere(vm.sizes, { id: vm.model.size.value });
                var location = vm.model.location;
                return vm.defaultNameImage = vm.model.subtype.text + ' ' + vm.model.genre.text + ' ' + size.value + ' ' + vm.model.name + ' ' + location.name;
            }
            else {
                return '';
            }
        }

        function canShowGallery() {
            return vm.model.subtype && vm.model.genre && vm.model.size && vm.model.name && vm.model.location;
        }

        function changeMonths() {
            if (!vm.years || vm.years === '') {
                vm.years = 0;
            }
            if (!vm.months || vm.months === '') {
                vm.months = 0;
            }
            vm.model.months = (vm.years * 12) + vm.months;
        }

        function saveAndContinue() {
            vm.continueAfterSaving = true;
        }

        function save(isValid) {
            if (isValid && !vm.isSending) {
                if (!vm.model.files || vm.model.files.length == 0) {
                    modalService.showError({ message: 'Al menos debe seleccionar una imagen' });
                    return false;
                }

                vm.isSending = true;

                if (vm.model.id > 0) {
                    petService.put(vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }
                else {
                    petService.post(vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }
            }

            function saveCompleted(response) {
                vm.isSending = false;

                response = response;
                var message = 'La mascota fue actualizada correctamente';
                var isNew = !vm.model.id;

                if (isNew) {
                    message = 'La mascota se ha creado correctamente';
                    vm.model.id = response.id;
                }
                else {
                    crawlingService.openCrawlingWindow('lostpet', { friendlyName: vm.model.friendlyName });
                }

                modalService.show({
                    message: message
                })
                    .then(function (modal) {
                        modal.closed.then(function () {
                            if (vm.continueAfterSaving) {
                                //if it is new and want to continue updates the location
                                if (isNew) {
                                    $location.path('/lostpets/' + vm.model.id + '/edit');
                                }
                            }
                            else {
                                $location.path('/lostpets');
                            }

                            vm.continueAfterSaving = false;
                        });
                    });
            }

            function saveError(response) {
                vm.isSending = false;
                helperService.handleException(response);
            }
        }
    }
})();