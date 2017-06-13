(function () {
    'use strict';

    angular
        .module('huellitas')
        .controller('FaqController', FaqController);

    FaqController.$inject = ['$scope', '$timeout'];

    function FaqController($scope, $timeout) {
        var vm = this;

        activate();

        function activate() {
            $scope.$parent.root.seo.title = app.Settings.resources['Seo.Faq.Title'];
            $scope.$parent.root.seo.description = app.Settings.resources['Seo.Faq.Description'];

            $scope.$on('$locationChangeStart', function (event) {
                console.log('Destruye el fullpage');
                $.fn.fullpage.destroy('all');
            });
            
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
