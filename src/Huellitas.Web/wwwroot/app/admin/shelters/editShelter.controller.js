﻿(function () {
    angular.module('huellitasAdmin')
        .controller('EditShelterController', EditShelterController);

    EditShelterController.$inject = ['$routeParams', '$location', 'shelterService', 'statusTypeService', 'modalService', 'contentService', 'fileService', 'helperService'];

    function EditShelterController($routeParams, $location, shelterService, statusTypeService, modalService, contentService, fileService, helperService) {
        var vm = this;
        vm.id = $routeParams.id
        vm.model = {};
        vm.users = [];
        vm.isSending = false;

        vm.usersFilter = {
            page: 0,
            pageSize: 20,
            relationType: app.Settings.contentRelationTypes.shelter
        };

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
        vm.addUser = addUser;
        vm.deleteUser = deleteUser;

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
            getUsers();
        }

        function getStatusTypes() {
            statusTypeService.getAll()
                .then(statusTypesCompleted)
                .catch(helperService.handleException);

            function statusTypesCompleted(rows) {
                vm.statusTypes = rows;
            }
        }

        function getUsers() {
            if (vm.id) {
                contentService.getUsers(vm.id, vm.usersFilter)
                .then(getCompleted)
                .catch(helperService.handleException);

                function getCompleted(response) {
                    vm.users = response.results;
                }
            }
        }

        function deleteUser(user) {
            if (vm.id) {

                if (confirm('¿Seguro deseas eliminar este usuario?')) {
                    contentService.deleteUser(vm.id, user.id)
                        .then(deleteCompleted)
                        .catch(helperService.handleException);

                    function deleteCompleted() {
                        vm.users = _.reject(vm.users, function (parent) { return parent.id === user.id; });
                    }
                }
            }
            else {
                vm.users = _.reject(vm.users, function (parent) { return parent.id === user.id; });
                vm.model.users = _.reject(vm.model.users, function (parent) { return parent.userId === user.id; });
            }
        }

        function addUser(selected) {
            if (selected) {
                var user = selected.originalObject;

                user.relationType = app.Settings.contentRelationTypes.shelter;
                vm.users = vm.users || [];
                var contentUser = { userId: user.id, relationType: user.relationType };

                if (vm.id) {


                    contentService.postUser(vm.id, contentUser)
                        .then(postCompleted)
                        .catch(helperService.handleException);

                    function postCompleted() {
                        vm.users.push(user);
                    }
                }
                else {
                    vm.model.users = vm.model.users || [];

                    vm.model.users.push(contentUser);
                    vm.users.push(user);
                }
            }
        }

        function getShelterById(id) {
            shelterService.getById(id)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(model) {
                vm.model = model;
                vm.showPicturesActive = true;
            }
        }

        function saveAndContinue() {
            vm.continueAfterSaving = true;
        }

        function activeTooggleClass(indexValue, actualValue) {
            return indexValue === actualValue ? 'btn-primary' : 'btn-default';
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
                vm.model.files = _.reject(vm.model.files, function (el) { return el.id === image.id });
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
            if (!vm.model.files || vm.model.files.length == 0) {
                modalService.showError({ message: 'Al menos debe seleccionar una imagen' });
                return false;
            }

            if (isValid && !vm.isSending) {
                vm.isSending = true;
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
                vm.isSending = false;
                response = response;
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
                vm.isSending = false;
                helperService.handleException(response);
            }
        }

    }
})();