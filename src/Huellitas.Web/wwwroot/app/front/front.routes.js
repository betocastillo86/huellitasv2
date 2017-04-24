(function () {
    angular.module('huellitas')
        .config(huellitasRoutes);

    huellitasRoutes.$inject = ['$routeProvider'];

    function huellitasRoutes($routeProvider)
    {
        $routeProvider
            .when('/', {
                templateUrl: '/app/front/home/home.html',
                controller: 'HomeController',
                controllerAs: 'main'
            });
    }
})();