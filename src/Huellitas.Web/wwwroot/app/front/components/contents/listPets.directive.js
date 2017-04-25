
(function () {
    'use strict';

    angular
        .module('huellitas')
        .directive('listPets', listContents);

    function listContents() {
        return {
            scope: false,
            templateUrl: '/app/front/components/contents/listPets.html',
            controller: ListPetsController,
            controllerAs: 'listPet',
            bindToController: true
        };
    }

    angular.module('huellitas')
        .controller('ListPetsController');

    ListPetsController.$inject = ['$attrs', '$scope', 'petService'];

    function ListPetsController($attrs, $scope, petService) {
        var vm = this;
        vm.filter = {};
        vm.pets = [];

        vm.petsLoadedCallback = undefined;

        activate();

        function activate() {
            vm.filter = $attrs.filter ? $scope.$eval($attrs.filter) : {};
            vm.petsLoadedCallback = $attrs.petsloaded ? $scope.$eval($attrs.petsloaded) : undefined;

            getPets();
        }

        function getPets()
        {
            petService.getAll(vm.filter)
                .then(getCompleted)
                .catch(petService.handleException);

            function getCompleted(response)
            {
                vm.pets = response.results;

                if (vm.petsLoadedCallback)
                {
                    vm.petsLoadedCallback(response);
                }
            }
        }

    }

})();

