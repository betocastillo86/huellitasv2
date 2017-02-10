(function () {

    angular.module('app')
        .factory('adoptionFormStatusService', adoptionFormStatusService);

    adoptionFormStatusService.$inject = ['$http'];

    function adoptionFormStatusService($http)
    {
        return{        
            getAll : getAll
        };

        function getAll()
        {
            return $http.get('/api/adoptionformanswerstatus');
        }
    }
})();