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
            bindToController: true,
            restrict: 'A'
        };
    }

    //angular.module('huellitas')
    //    .controller('ListPetsController');

    ListPetsController.$inject = ['$attrs', '$scope', 'petService', 'helperService', 'routingService'];

    /**
     * Directive with properties
     * @param {any} $attrs ->
            filter:         filter of contents,
            petsloaded:     callback when the pets are loaded,
            pagingenabled:  enables the paging
            displaytype:    small,medium
     * @param {any} $scope
     * @param {any} petService
     */
    function ListPetsController($attrs, $scope, petService, helperService, routingService) {
        var vm = this;
        vm.filter = {};
        vm.pets = [];

        vm.petsLoadedCallback = undefined;
        vm.showPager = false;
        vm.pagingEnabled = false;
        vm.title = undefined;
        vm.displayType = 'medium';

        vm.nextPage = nextPage;

        activate();

        function activate() {
            vm.filter = $attrs.filter ? $scope.$eval($attrs.filter) : {};
            vm.petsLoadedCallback = $attrs.petsloaded ? $scope.$eval($attrs.petsloaded) : undefined;
            vm.pagingEnabled = $attrs.pagingenabled ? $scope.$eval($attrs.pagingenabled) : false;
            vm.displayType = $attrs.displaytype ? $attrs.displaytype : 'medium';
            vm.title = $attrs.title ? $scope.$eval($attrs.title) : undefined;

            getPets();
        }

        function getPets()
        {
            petService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                for (var i = 0; i < response.results.length; i++) {
                    response.results[i].url = routingService.getRoute(response.results[i].type == 'Pet' ? 'pet' : 'lostpet', { friendlyName: response.results[i].friendlyName });
                }


                if (vm.pets.length && vm.filter.page) {
                    vm.pets = vm.pets.concat(response.results);
                }
                else{
                    vm.pets = response.results;
                }

                if (vm.pagingEnabled)
                {
                    vm.showPager = response.meta.hasNextPage;
                }
                
                if (vm.petsLoadedCallback)
                {
                    vm.petsLoadedCallback(response);
                }
            }
        }

        function nextPage()
        {
            vm.filter.page++;
            getPets();
        }
    }

})();

