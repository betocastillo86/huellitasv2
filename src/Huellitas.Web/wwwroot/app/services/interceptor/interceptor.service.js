(function () {
    angular.module('huellitasServices')
        .factory('interceptorService', interceptorService);

    interceptorService.$inject = ['$q','$window'];

    function interceptorService($q, $window)
    {
        return {
            'response': response
        };

        function response(responseData)
        {
            if (responseData.headers && responseData.headers()['x-currentdate']) {
                $window.currentDate = new Date(responseData.headers()['x-currentdate'])
            }
            else {
                $window.currentDate = new Date();
            }

            return responseData || $q.when(responseData);
        }
    }
})();