(function () {
    angular.module('huellitas', [
        // Angular modules
        'ngRoute',
        'ngStorage',
        'ngSanitize',
        'ngAnimate',

        // Custom modules
        'huellitasComponents',
        'huellitasServices',

        // 3rd Party Modules
        'underscore',
        'angucomplete-alt',
    ]);
})();