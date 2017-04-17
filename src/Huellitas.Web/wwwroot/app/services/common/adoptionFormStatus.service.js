(function () {

    angular.module('huellitasServices')
        .factory('adoptionFormStatusService', adoptionFormStatusService);

    adoptionFormStatusService.$inject = ['httpService'];

    function adoptionFormStatusService(http)
    {
        return{        
            getAll : getAll
        };

        function getAll()
        {
            return http.get('/api/adoptionformanswerstatus');
        }
    }
})();