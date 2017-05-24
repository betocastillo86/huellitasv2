(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('NewPetController', NewPetController);

    NewPetController.$inject = ['$scope'];

    function NewPetController($scope) {
        var vm = this;
        
        activate();

        function activate()
        {
            $scope.$parent.root.seo.title = app.Settings.resources['Seo.NewPet.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.NewPet.Description'];
        }
    }
})();
