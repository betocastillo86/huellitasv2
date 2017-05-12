
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('ShelterDetailController', ShelterDetailController);

    ShelterDetailController.$inject = ['$routeParams','shelterService'];

    function ShelterDetailController($routeParams, shelterService) {
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
                .catch(getError);

            function getCompleted(response)
            {
                vm.model = response;

                vm.filterMyPets = {
                    page: 0,
                    pageSize: 6,
                    shelter: vm.model.id,
                    status: 'Published'
                };
            }

            function getError()
            {
                //validar not 
            }
        }
    }
})();
