(function () {
    angular.module('app')
        .controller('ListPetsController', ListPetsController);

    ListPetsController.$inject = ['petService', 'shelterService'];

    function ListPetsController(petService, shelterService)
    {
        var vm = this;
        vm.filter = {};

        vm.doFilter = doFilter;


        activateGetPets();
        activateGetShelters();

        function activateGetPets()
        {
            return getPets();

            function getPets()
            {
                return petService.getAll(vm.filter)
                    .then(getPetsCompleted);

                function getPetsCompleted(data)
                {
                    vm.pets = data;
                    return vm.pets;
                }
            }
        }

        function activateGetShelters()
        {
            return getShelters();

            function getShelters() {
                return shelterService.getAll()
                    .then(getSheltersCompleted);

                function getSheltersCompleted(data) {
                    vm.shelters = data;
                    return vm.shelters;
                }
            }
        }

        function doFilter()
        {
            activateGetPets();
        }
    }

})();