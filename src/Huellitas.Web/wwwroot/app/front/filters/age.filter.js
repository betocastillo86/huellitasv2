
(function () {
    'use strict';

    angular
        .module('huellitas')
        .filter('age', age);

    function age() {

        return ageFilter;

        function ageFilter(months)
        {
            if (!months)
            {
                return;
            }

            if (months < 12) {
                return months + ' mes' + (months > 1 ? 'es' : '');
            }
            else
            {
                var years = Math.floor(months / 12);
                var otherMonths = months % 12;

                if (otherMonths) {
                    return years + ' año' + (years > 1 ? 's' : '') + ' y ' + otherMonths + ' mes' + (otherMonths > 1 ? 'es' : '');
                }
                else
                {
                    return years + ' año' + (years > 1 ? 's' : '');
                }
            }

        }
    }
})();
