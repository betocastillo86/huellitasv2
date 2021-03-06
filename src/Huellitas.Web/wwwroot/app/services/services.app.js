﻿(function () {
    'use strict';

    angular
        .module('huellitasServices', []);

    start();

    function start() {

        String.prototype.currentTimeZone = -5;

        String.prototype.getIntervalTime = function () {
            if (this !== '') {

                var timeZone = currentTimeZone();

                var date = moment(new Date(this)).add('hour', timeZone).toDate();
                var difference = (window.currentDate - date) / 1000;

                if (difference <= 5) {
                    return 'En este momento';
                }
                else if (difference <= 20) {
                    return 'Hace segundos';
                }
                else if (difference <= 60) {
                    return 'Hace 1 minuto';
                }
                else if (difference < 3600) {
                    return 'Hace '.concat(parseInt(difference / 60), ' minutos');
                }
                else if (difference <= 5400) {
                    return 'Hace 1 hora';
                }
                else if (difference < 84600) {
                    return 'Hace '.concat(parseInt(difference / 3600), ' horas');
                }
                else if (difference < 604800) {
                    return 'Hace '.concat(parseInt(difference / 86400), ' dias');
                }
                else {
                    return moment(date).format("YYYY/MM/DD"); 
                }
            } else {
                return '';
            }       

            function currentTimeZone() {
                return String.prototype.currentTimeZone;
            }
        };

        String.prototype.queryStringToJson = function () {
            var pairs = this.slice(1).split('&');

            var result = {};
            pairs.forEach(function (pair) {
                pair = pair.split('=');
                result[pair[0]] = decodeURIComponent(pair[1] || '');
            });

            return JSON.parse(JSON.stringify(result));
        };

        String.prototype.format = function () {
            var args = arguments;
            return this.replace(/{(\d+)}/g, function (match, number) {
                return typeof args[number] != 'undefined'
                    ? args[number]
                    : match
                    ;
            });
        };
    }
})();