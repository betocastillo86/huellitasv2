(function () {
    angular.module('app')
        .factory('adoptionFormService', adoptionFormService);

    adoptionFormService.$inject = ['$http'];

    function adoptionFormService($http) {

        return {
            getAll: getAll
        };

        function getAll(filter)
        {
            return $http.get('/api/adoptionforms', { params: filter });
        }
    }

})();