(function () {
    angular.module('huellitasComponents')
        .directive('datepickerHuellitas', DatepickerHuellitas);

    //DatepickerHuellitas.$inject = ['$rootScope'];

    function DatepickerHuellitas()
    {
        return {
            scope: false,
            link: link,
            restrict: 'A'
        };

        function link(scope, element, attrs)
        {
            var maxdate = undefined;
            if (attrs.maxdate)
            {
                maxdate = moment().toDate(attrs.maxdate);
            }

            var picker = new Pikaday({
                field: element[0],
                format: 'YYYY/MM/DD',
                i18n: {
                    previousMonth: 'Mes anterior',
                    nextMonth: 'Mes siguiente',
                    months: ['Enero', 'Febrero', 'Marzo', 'Abrl', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                    weekdays: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                    weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab']
                },
                maxDate: maxdate
            });
        }
    }
})();