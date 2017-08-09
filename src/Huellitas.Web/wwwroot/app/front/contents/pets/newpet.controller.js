(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('NewPetController', NewPetController);

    NewPetController.$inject = ['$scope', 'routingService'];

    function NewPetController($scope, routingService) {
        var vm = this;
        vm.isShowingVideo = false;
        vm.showVideo = showVideo;
        
        activate();

        function activate()
        {
            $scope.$parent.root.seo.title = app.Settings.resources['Seo.NewPet.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.NewPet.Description'];
            $scope.$parent.root.seo.image = routingService.getFullRouteOfFile('img/front/compartir-fb-publicar.png');
        }

        function showVideo()
        {
            vm.isShowingVideo = true;
        }
    }
})();
