(function () {

    angular.module('huellitasServices')
    .factory('statusTypeService', statusTypeService);

    statusTypeService.$inject = ['httpService'];

    function statusTypeService(http)
    {
        return {
            getAll : getAll
        };

        function getAll()
        {
            return http.get('/api/statustypes');
        }
    }

})();