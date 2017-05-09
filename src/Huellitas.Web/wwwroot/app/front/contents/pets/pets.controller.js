(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('PetsController', PetsController);

    PetsController.$inject = ['$location', 'routingService'];

    function PetsController($location, routingService) {
        var vm = this;

        vm.filter = {
            pageSize: 9,
            page: 0,
            status: 'Published',
            orderBy: 'Featured',
            size: $location.search().size ? parseInt($location.search().size) : undefined,
            genre: $location.search().genre ? parseInt($location.search().genre) : undefined,
            age: $location.search().age,
            subtype: $location.search().subtype ? parseInt($location.search().subtype) : undefined,
            keyword: $location.search().keyword
        };

        vm.genres = app.Settings.genres;
        vm.sizes = app.Settings.sizes;
        vm.subtypes = app.Settings.subtypes;
        vm.ages = [{ id: '0-12', value: 'Menos de un año' }, { id: '13-35', value: 'De 1 a 2 años' }, { id: '36-59', value: 'De 3 a 4 años' }, { id: '60-', value: 'Más de 5 años' }];

        vm.pagingEnabled = true;

        vm.search = search;
        vm.changeSubtype = changeSubtype;
        vm.isSubtypeChecked = isSubtypeChecked;

        activate();

        function activate()
        {

        }

        function search()
        {
            $location.path(routingService.getRoute('pets')).search({
                size: vm.filter.size,
                genre: vm.filter.genre,
                age: vm.filter.age,
                subtype: vm.filter.subtype,
                keyword: vm.filter.keyword
            });
        }

        function changeSubtype(index)
        {
            vm.filter.subtype = vm.subtypes[index].id;
        }

        function isSubtypeChecked(index)
        {
            return vm.filter.subtype == vm.subtypes[index].id;
        }
    }
})();
