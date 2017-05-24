(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('FaqController', FaqController);

    FaqController.$inject = ['$timeout'];

    function FaqController($timeout) {
        var vm = this;

        activate();

        function activate() {
            $timeout(fullpage, 500);   
        }

        function fullpage()
        {
            $('#fullpage').fullpage({
                sectionsColor: ['#FFF', '#7CD2E1', '#B8EA81', '#FC9282', '#304A6F', '#3C75C2'],
                navigation: true,
                navigationPosition: 'right',
                navigationTooltips: ['Adopta', 'Tiempo', 'Dinero', 'Hogar', 'Responsable', 'Tu mascota']
            });
        }
    }
})();
