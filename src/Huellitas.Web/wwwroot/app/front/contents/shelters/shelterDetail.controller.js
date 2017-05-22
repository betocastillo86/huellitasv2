
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('ShelterDetailController', ShelterDetailController);

    ShelterDetailController.$inject = ['$routeParams','shelterService', 'helperService'];

    function ShelterDetailController($routeParams, shelterService, helperService) {
        var vm = this;
        vm.model = {};
        vm.friendlyName = $routeParams.friendlyName;

        activate();

        function activate()
        {
            getShelter();
        }

        function getShelter()
        {
            shelterService.getById(vm.friendlyName)
                .then(getCompleted)
                .catch(helperService.notFound);

            function getCompleted(response)
            {
                vm.model = response;

                vm.filterMyPets = {
                    page: 0,
                    pageSize: 6,
                    shelter: vm.model.id,
                    status: 'Published',
                    contentType: 'Pet'
                };
            }

        }
    }
})();
