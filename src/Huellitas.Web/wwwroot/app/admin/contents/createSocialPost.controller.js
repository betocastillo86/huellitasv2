(function () {
    'use strict';

    angular
        .module('huellitasAdmin')
        .controller('CreateSocialPostController', CreateSocialPostController);

    CreateSocialPostController.$inject = ['$scope', 'fileService', 'helperService'];

    function CreateSocialPostController($scope, fileService, helperService) {
        var vm = this;

        vm.colors = [
            { name: 'Rosado', code: 'Pink', color: '#FC9282' },
            { name: 'Morado', code: 'Violet', color: '#B96BFE' },
            { name: 'Verde', code: 'Green', color: '#B8EA81' },
            { name: 'Azul Claro', code: 'Blue', color: '#7CD2E1' },
            { name: 'Azul Oscuro', code: 'DarkBlue', color: '#3C75C2', selected:true }
        ];

        vm.networks = [
            { name: 'Facebook', selected: true, css:'fa-facebook' },
            { name: 'Instagram', css: 'fa-instagram'}
        ];

        vm.changeColor = changeColor;
        vm.changeNetwork = changeNetwork;
        vm.close = close;
        vm.createImage = createImage;
        vm.contentId = $scope.params.contentId;
        vm.generatedImage = undefined;

        activate();

        function activate()
        {

        }

        function changeColor(color)
        {
            for (var i = 0; i < vm.colors.length; i++) {
                vm.colors[i].selected = vm.colors[i].code == color.code;
            }
        }

        function changeNetwork(network) {
            for (var i = 0; i < vm.networks.length; i++) {
                vm.networks[i].selected = vm.networks[i].name == network.name;
            }
        }

        function createImage()
        {
            var network = _.findWhere(vm.networks, { selected: true });
            var color = _.findWhere(vm.colors, { selected: true });

            fileService.postSocialNetwork(vm.contentId, { color: color.code, socialNetwork: network.name })
                .then(postCompleted)
                .catch(helperService.handleException);

            function postCompleted(response)
            {
                vm.generatedImage = response.thumbnail + '?' + moment();
            }
        }

        function close() {
            $scope.close({ accept: true });
        }
    }
})();
