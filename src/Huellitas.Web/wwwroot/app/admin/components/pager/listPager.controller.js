(function () {

    angular.module('huellitasAdmin')
        .controller('ListPagerController', ListPagerController);

    ListPagerController.$inject = ['$scope'];

    function ListPagerController($scope) {
        var vm = this;
        vm.model = $scope.main.pager;
        vm.getArrayPages = getArrayPages;
        vm.getTotalPages = getTotalPages;
        vm.activeClass = activeClass;
        vm.classPrevious = classPrevious;
        vm.classNext = classNext;
        vm.previousPage = previousPage;
        vm.nextPage = nextPage;
        vm.changePage = changePage;

        activate();

        function activate() {

        }

        function getArrayPages() {
            if ($scope.main.pager.totalCount !== undefined) {
                return new Array(getTotalPages());
            }
        }

        function getTotalPages() {
            return Math.ceil($scope.main.pager.totalCount / $scope.main.pager.pageSize);
        }

        function activeClass(currentPage) {
            return currentPage == $scope.main.pager.page ? 'active' : '';
        }

        function classPrevious() {
            return $scope.main.pager.page == 0 ? 'disabled' : '';
        }

        function classNext() {
            return $scope.main.pager.page + 1 == getTotalPages() ? 'disabled' : '';
        }

        function previousPage() {
            if ($scope.main.pager.page > 0) {
                changePage($scope.main.pager.page - 1);
            }
        }

        function nextPage() {
            if ($scope.main.pager.page + 1 < getTotalPages()) {
                changePage($scope.main.pager.page + 1);
            }
        }

        function changePage(page) {
            $scope.main.changePage(page);
        }
    }
})();