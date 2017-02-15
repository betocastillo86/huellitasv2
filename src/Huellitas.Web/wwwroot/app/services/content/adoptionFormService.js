(function () {
    angular.module('app')
        .factory('adoptionFormService', adoptionFormService);

    adoptionFormService.$inject = ['$http'];

    function adoptionFormService($http) {

        return {
            getAll: getAll,
            getById : getById
        };

        function getAll(filter)
        {
            return $http.get('/api/adoptionforms', { params: filter });
        }

        function getById(id)
        {
            return $http.get('/api/adoptionforms/' + id);
        }
    }

})();