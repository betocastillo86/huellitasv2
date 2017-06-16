(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('ShareController', ShareController);

    ShareController.$inject = ['$scope', '$window'];

    function ShareController($scope, $window) {
        var vm = this;

        vm.url = $scope.params.url;

        vm.share = share;
        
        activate();

        function activate() {
        }

        function share(network)
        {
            var width = 650;
            var height = 500;
            var top = $window.screenY + ($window.outerHeight - height) / 2.5;
            var left = $window.screenX + ($window.outerWidth - width) / 2;

            var shareUrl = '';

            switch (network) {
                case 'twitter':
                    shareUrl = 'https://twitter.com/intent/tweet?url=' + vm.url;
                    break;
                case 'whatsapp':
                    shareUrl = 'whatsapp://send?text=' + vm.url;
                    break;
                case 'facebook':
                default:
                    shareUrl = 'https://www.facebook.com/sharer/sharer.php?u=' + vm.url;
                    break;
            }

            $window.open(shareUrl, 'shared', 'width='.concat(width, ',height=', height, ',top=', top, ',left=', left));
        }
    }
})();
