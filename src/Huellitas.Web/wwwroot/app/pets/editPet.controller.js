(function () {
    angular.module('app')
        .controller('EditPetController', EditPetController);

    EditPetController.$inject = ['$routeParams', 'petService', 'customTableRowService', 'statusTypeService'];

    function EditPetController($routeParams, petService, customTableRowService, statusTypeService) {
        var vm = this;
        vm.id = $routeParams.id;
        vm.model = {};
        vm.showMoreActive = false;
        vm.showPicturesActive = true;

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
        

        activate();

        function activate() {
            if (vm.id) {
                getPetById(vm.id);
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

        
    }
})();