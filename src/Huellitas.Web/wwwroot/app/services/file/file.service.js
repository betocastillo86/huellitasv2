(function () {
    angular.module('huellitasServices')
    .factory('fileService', fileService);

    fileService.$inject = ['$http'];

    function fileService($http) {
        return {
            post: post,
            deleteContentFile: deleteContentFile,
            postContentFile: postContentFile
        };

        function post(file, name) {
            var fd = new FormData();
            fd.append('files', file);
            fd.append('name', name);
            return $http.post('/api/files', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
        }

        function deleteContentFile(contentId, fileId) {
            return $http.delete('/api/contents/' + contentId + '/files/' + fileId);
        }

        function postContentFile(contentId, file) {
            return $http.post('/api/contents/' + contentId + '/files', file);
        }
    }
})();