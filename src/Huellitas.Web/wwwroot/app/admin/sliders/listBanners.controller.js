
(function () {
    'use strict';

    angular
        .module('huellitasAdmin')
        .controller('ListBannersController', ListBannersController);

    ListBannersController.$inject = ['bannerService', 'helperService'];


    function ListBannersController(bannerService, helperService) {
        var vm = this;

        vm.banners = [];
        vm.filter = {
            pageSize: app.Settings.general.pageSize,
            page: 0
        };

        vm.changePage = changePage;
        vm.getBanners = getBanners;

        activate();

        return vm;

        function activate() {
            getBanners();
        }

        function getBanners() {
            bannerService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response) {
                vm.banners = response.results;
                vm.pager = response.meta;
                vm.pager['pageSize'] = vm.filter.pageSize;
                vm.pager['page'] = vm.filter.page;
                return vm.notifications;
            }
        }

        function changePage(page) {
            vm.filter.page = page;
            return getBanners();
        }
    }
})();
