(function () {
    'use strict';

    angular
        .module('huellitasServices', []);

    start();

    function start() {

        String.prototype.getIntervalTime = function () {
            if (this !== '') {
                var date = new Date(this);
                var difference = (window.currentDate - date) / 1000;

                if (difference <= 5) {
                    return 'En este momento';
                }

                if (difference <= 20) {
                    return 'Hace segundos';
                }

                if (difference <= 60) {
                    return 'Hace 1 minuto';
                }

                if (difference < 3600) {
                    return 'Hace '.concat(parseInt(difference / 60), ' minutos');
                }

                if (difference <= 5400) {
                    return 'Hace 1 hora';
                }

                if (difference < 84600) {
                    return 'Hace '.concat(parseInt(difference / 3600), ' horas');
                }
                else {
                    return 'Hace '.concat(parseInt(difference / 86400), ' dias');
                }
            } else {
                return '';
            }
        };
    }
})();