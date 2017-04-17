(function () {
    angular.module('huellitasAdmin')
        .directive('datepickerHuellitas', DatepickerHuellitas);

    function DatepickerHuellitas()
    {
        return {
            scope: false,
            link: link,
            restrict: 'A'
        };

        function link(scope, element, attrs)
        {
            var picker = new Pikaday({
                field: element[0],
                format: 'YYYY/MM/DD',
                i18n: {
                    previousMonth: 'Mes anterior',
                    nextMonth: 'Mes siguiente',
                    months: ['Enero', 'Febrero', 'Marzo', 'Abrl', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    weekdays: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                    weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
                }
            });
        }
    }
})();