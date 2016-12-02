(function () {
    angular.module('app')
        .factory('shelterService', shelterService);

    shelterService.$inject = ['$http'];

    function shelterService($http) {
        return {
            getAll: getAll
        };

        function getAll() {
            return $http.get('/api/shelters')
            .then(getAllComplete)
            .catch(getAllError);

            function getAllComplete(response) {
                return response.data;
            }

            function getAllError() {
                console.log('Un error');
            }
        }

    }

})();