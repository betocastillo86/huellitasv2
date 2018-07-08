(function () {
    angular.module('huellitasAdmin')
        .controller('WelcomeController', WelcomeController);

    WelcomeController.$inject = [
        '$scope',
        'modalService',
        'crawlingService'];

    function WelcomeController(
        $scope,
        modalService,
        crawlingService) {

        console.log('Carga el controlador');
        var vm = this;
        vm.name = 'Gabriel Castillo Incompleto;';
        vm.model = {};
        vm.crawl = crawl;

        function crawl()
        {
            crawlingService.crawlEntireSite(vm.model.prefixCrawl);
        }

        return vm;
    }
})();