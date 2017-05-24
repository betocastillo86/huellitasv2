(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('NotFoundController', NotFoundController);

    NotFoundController.$inject = ['$scope'];

    function NotFoundController($scope) {
        var vm = this;

        activate();

        function activate()
        {
            $scope.$parent.root.seo.title = 'Página no encontrada';
            $scope.$parent.root.seo.description = 'Página no encontrada';
        }
    }
})();