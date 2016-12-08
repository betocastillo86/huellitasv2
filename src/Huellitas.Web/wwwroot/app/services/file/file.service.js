(function () {
    angular.module('app')
    .factory('fileService', fileService);

    fileService.$inject = ['$http'];

    function fileService($http)
    {
        return {
            post : post
        };

        function post(file, name)
        {
            var fd = new FormData();
            fd.append('files', file);
            fd.append('name', name);
            return $http.post('/api/files', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
        }
    }
})();