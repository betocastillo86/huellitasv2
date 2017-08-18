
(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('AdoptTermsController', AdoptTermsController);

    AdoptTermsController.$inject = ['$routeParams', '$scope'];

    function AdoptTermsController($routeParams, $scope) {
        var vm = this;

        vm.friendlyName = $routeParams.friendlyName;

        activate();

        function activate() {
            $scope.$parent.root.seo.title = app.Settings.resources['Seo.AdoptTerms.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.AdoptTerms.Description'];
            $scope.$parent.root.hideFooter = true;
        }
    }
})();
