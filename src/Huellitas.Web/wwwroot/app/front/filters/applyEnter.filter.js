(function () {
    'use strict';

    angular
        .module('huellitas')
        .filter('applyEnter', applyEnter);

    function applyEnter() {

        return function (text) {

            console.log('Entra a revisar');

            if (text) {
                return text.replace(/\n/g, '<br>');
            }
            else {
                return '';
            }
        };
    }
})();

