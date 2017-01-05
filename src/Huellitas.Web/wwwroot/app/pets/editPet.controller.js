(function () {
    angular.module('app')
        .controller('EditPetController', EditPetController);

    EditPetController.$inject = ['$routeParams', '$location', 'petService', 'customTableRowService', 'statusTypeService', 'fileService', 'modalService'];

    function EditPetController($routeParams, $location, petService, customTableRowService, statusTypeService, fileService, modalService) {
        var vm = this;
        vm.id = $routeParams.id;

        vm.model = {};
        vm.model.autoReply = true;
        vm.model.featured = false;
        vm.model.status = app.Settings.statusTypes.published;

        vm.showMoreActive = false;
        vm.showPicturesActive = false;
        
        vm.activeTooggleClass = activeTooggleClass;
        vm.changeGenre = changeGenre;
        vm.changeSubtype = changeSubtype;
        vm.changeShelter = changeShelter;
        vm.changeLocation = changeLocation;
        vm.toogleShowMore = toogleShowMore;
        vm.toogleShowPictures = toogleShowPictures;
        vm.changeFeatured = changeFeatured;
        vm.changeAutoReply = changeAutoReply;
        vm.changeCastrated = changeCastrated;
        vm.removeImage = removeImage;
        vm.imageAdded = imageAdded;
        vm.save = save;
        vm.isInvalidClass = isInvalidClass;
        vm.changeMonths = changeMonths;
        vm.isLocationRequired = isLocationRequired;
        vm.saveAndContinue = saveAndContinue;

        activate();

        function activate() {
            if (vm.id) {
                getPetById(vm.id);
            }
            else {
                vm.showPicturesActive = true;
            }

            getSizes();
            getSubtypes();
            getGenres();
            getStatusTypes();
        }

        function getPetById(id) {
            petService.getById(id)
                    .then(getCompleted)
                    .catch(getError);

            function getCompleted(model) {
                vm.model = model;
                vm.years = Math.floor(model.months / 12);
                vm.months = model.months % 12;
                vm.showPicturesActive = true;
            }

            function getError() {
                debugger;
            }
        }

        function getSizes() {
            customTableRowService.getSizes()
                .then(getSizesCompleted);

            function getSizesCompleted(rows) {
                vm.sizes = rows;
            }

            function getSizesError() {
                console.log(error, arguments);
            }
        }

        function getSubtypes() {
            customTableRowService.getSubtypes()
                .then(getSubtypesCompleted);

            function getSubtypesCompleted(rows) {
                vm.subtypes = rows;
            }

            function getSubtypesError() {
                console.log(error, arguments);
            }
        }

        function getGenres() {
            customTableRowService.getGenres()
                .then(getGenresCompleted);

            function getGenresCompleted(rows) {
                vm.genres = rows;
            }

            function getGenresError() {
                console.log(error, arguments);
            }
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

        function activeTooggleClass(indexValue, actualValue) {
            return indexValue == actualValue ? 'btn-primary' : 'btn-default';
        }

        function changeGenre(genre) {
            vm.model.genre = vm.model.genre || {};
            vm.model.genre.value = genre;
        }

        function changeSubtype(subtype) {
            vm.model.subtype = vm.model.subtype || {};
            vm.model.subtype.value = subtype;
        }

        function changeShelter(selectedShelter)
        {
            vm.model.shelter = vm.model.shelter || {};
            vm.model.shelter.id = selectedShelter ? selectedShelter.originalObject.id : undefined;
        }

        function changeLocation(selectedLocation)
        {
            vm.model.location = vm.model.location || {};
            vm.model.location.id = selectedLocation ? selectedLocation.originalObject.id : undefined;
        }

        function toogleShowMore()
        {
            vm.showMoreActive = !vm.showMoreActive;
        }

        function toogleShowPictures() {
            vm.showPicturesActive = !vm.showPicturesActive;
        }

        function changeFeatured(featured)
        {
            vm.model.featured = featured;
        }

        function changeAutoReply(autoReply) {
            vm.model.autoReply = autoReply;
        }

        function changeCastrated(castrated) {
            vm.model.castrated = castrated;
        }

        function removeImage(image)
        {
            if (vm.model.id) {
                fileService.deleteContentFile(vm.model.id, image.id);
            }
            else {
                vm.model.files = _.reject(vm.model.files, function (el) { return el.id == image.id });
            }
        }

        function imageAdded(image)
        {
            if (vm.model.id) {
                fileService.postContentFile(vm.model.id, image);
            }
            else {
                vm.model.files = vm.model.files || [];
                vm.model.files.push(image);
            }
        }

        function isInvalidClass(form, field)
        {
            return form.$submitted && !field.$valid ? 'parsley-error' : undefined;
        }

        function changeMonths()
        {
            if(!vm.years || vm.years == '')
            {
                vm.years = 0;
            }
            if(!vm.months || vm.months == '')
            {
                vm.months = 0;
            }
            vm.model.months = (vm.years * 12) + vm.months;
        }

        function isLocationRequired()
        {
            return (!vm.model.location || !vm.model.location.id) && (!vm.model.shelter || !vm.model.shelter.id);
        }

        function saveAndContinue()
        {
            vm.continueAfterSaving = true;
        }

        function save(isValid)
        {
            if (isValid)
            {
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

            function saveCompleted(response)
            {
                response = response.data;
                var message = 'La mascota fue actualizada correctamente';
                var isNew = !vm.model.id;

                if (isNew)
                {
                    message = 'La mascota se ha creado correctamente';
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
                                $location.path('/pets/' + vm.model.id + '/edit');
                            }
                        }
                        else {
                            $location.path('/pets');
                        }

                        vm.continueAfterSaving = false;
                    });
                });
            }


            function saveError(response)
            {
                modalService.showError({
                    error: response.data.error
                });
            }
        }
    }
})();