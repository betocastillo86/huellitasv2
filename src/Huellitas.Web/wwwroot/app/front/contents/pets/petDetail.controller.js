(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('PetDetailController', PetDetailController);

    PetDetailController.$inject = ['$routeParams', 'helperService', 'petService', 'contentService'];

    function PetDetailController($routeParams, helperService, petService, contentService) {
        var vm = this;

        vm.friendlyName = $routeParams.friendlyName;
        vm.model = {};
        vm.filterSimilar = {};
        vm.titleSimilar = 'Mascotas similares';
        
        activate();

        function activate()
        {
            getPet();
        }

        function getPet()
        {
            petService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.model = response;

                vm.filterSimilar = {
                    pageSize: 4,
                    status: 'Published',
                    subtype: vm.model.subtype.value,
                    size: vm.model.size.value
                };

                if (!vm.model.shelter)
                {
                    getContentUsers();
                }
            }
        }

        function getContentUsers()
        {
            contentService.getUsers(vm.model.id, { relationType: 'Parent' })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.model.parents = response.results;
            }
        }
    }
})();
