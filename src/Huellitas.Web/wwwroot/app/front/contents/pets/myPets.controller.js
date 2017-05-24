(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('MyPetsController', MyPetsController);


    MyPetsController.$inject = [
        '$scope',
        '$location',
        'petService',
        'helperService',
        'routingService',
        'contentService',
        'sessionService',
        'modalService',
        'authenticationService'];

    function MyPetsController(
        $scope,
        $location,
        petService,
        helperService,
        routingService,
        contentService,
        sessionService,
        modalService,
        authenticationService) {

        var vm = this;
        vm.pets = [];
        vm.filter = {
            page: 0,
            pageSize: 10,
            mine: true,
            countForms: true,
            orderBy: 'CreatedDate',
            contentType: 'Pet',
            subtype: $location.search().subtype ? parseInt($location.search().subtype) : undefined,
            genre: $location.search().genre ? parseInt($location.search().genre)  : undefined,
            keyword: $location.search().keyword ? $location.search().keyword : undefined,
            shelter: $location.search().shelter ? parseInt($location.search().shelter) : undefined,
            status: $location.search().status ? $location.search().status : undefined
        };
        
        vm.hasNextPage = false;
        vm.genres = app.Settings.genres;
        vm.subtypes = app.Settings.subtypes;
        vm.shelters = [];

        vm.search = search;
        vm.isSubtypeChecked = isSubtypeChecked;
        vm.filterByStatus = filterByStatus;
        vm.changeStatus = changeStatus;
        vm.more = more;
        
        activate();

        function activate()
        {
            authenticationService.showLogin($scope)
                .then(authenticationCompleted)
                .catch(authenticationError);

            function authenticationCompleted(response) {
                getPets();
                getShelters();
            }

            function authenticationError()
            {
                $location.path(routingService.getRoute('home'));
            }
        }

        function getPets()
        {
            petService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                if (vm.pets.length) {
                    vm.pets = vm.pets.concat(response.results);
                }
                else{
                    vm.pets = response.results;
                }
                
                vm.hasNextPage = response.meta.hasNextPage;
            }
        }

        function getShelters()
        {
            var userId = sessionService.getCurrentUser().id;

            contentService.getContentsOfUser(userId, { relationType: 'Shelter', pageSize: 20 })
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.shelters = response.results;
            }
        }

        function search() {
            $location.path(routingService.getRoute('mypets')).search({
                genre: vm.filter.genre,
                subtype: vm.filter.subtype,
                keyword: vm.filter.keyword,
                shelter: vm.filter.shelter,
                status: vm.filter.status
            });
        }

        function isSubtypeChecked(index) {
            return vm.filter.subtype == vm.subtypes[index].id;
        }

        function filterByStatus(status)
        {
            vm.filter.status = status === vm.filter.status ? undefined : status;
            vm.search();
        }

        function changeStatus(pet)
        {
            modalService.showDialog({
                message: '¿Seguro deseas cambiar el estado de la mascota?',
                closed: confirmClosed
            });
            
            function confirmClosed(response)
            {
                if (response.accept)
                {
                    var newStatus = pet.status === 'Published' ? 'Hidden' : 'Published';
                    petService.changeStatus(pet.id, newStatus)
                        .then(patchCompleted)
                        .catch(helperService.handleException);

                    function patchCompleted(response)
                    {
                        pet.status = newStatus;
                    }
                }
            }
        }

        function more()
        {
            vm.filter.page++;
            getPets();
        }
    }
})();
