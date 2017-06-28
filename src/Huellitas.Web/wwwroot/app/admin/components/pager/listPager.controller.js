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
        vm.showPage = showPage;

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

        function showPage(page)
        {
            if (getTotalPages() > 6) {
                if ($scope.main.pager.page < 3 || $scope.main.pager.page > getTotalPages() - 3) {
                    return page > getTotalPages() - 4 || page < 3;
                }
                else
                {
                    return page > $scope.main.pager.page - 4 && page < $scope.main.pager.page + 3;
                }
            }
            else
            {
                return true;
            }
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