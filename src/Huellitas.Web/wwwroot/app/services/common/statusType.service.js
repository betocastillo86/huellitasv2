(function () {

    angular.module('huellitasServices')
    .factory('statusTypeService', statusTypeService);

    statusTypeService.$inject = ['$http'];

    function statusTypeService($http)
    {
        return {
            getAll : getAll
        };

        function getAll()
        {
            return $http.get('/api/statustypes')
            .then(getAllCompleted)
            .catch(getAllError);

            function getAllCompleted(result)
            {
                return result.data;
            }

            function getAllError()
            {
                console.log('El error');
            }
        }
    }

})();