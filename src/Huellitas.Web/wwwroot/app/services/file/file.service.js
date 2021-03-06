﻿(function () {
    angular.module('huellitasServices')
        .factory('fileService', fileService);

    fileService.$inject = ['$http', '$q', 'httpService'];

    function fileService($http, $q, http) {
        return {
            post: post,
            deleteContentFile: deleteContentFile,
            postContentFile: postContentFile,
            sortContentFile: sortContentFile,
            postSocialNetwork: postSocialNetwork,
            patchContentFile: patchContentFile 
        };

        function post(file, name, callback, indexFile) {
            var defered = $q.defer();
            var promise = defered.promise;
            
            var fd = new FormData();
            fd.append('files', file);
            fd.append('name', name ? name : file.name);

            
                $http({
                    method: 'POST',
                    url: '/api/files',
                    headers: { 'Content-Type': undefined },
                    uploadEventHandlers: {
                        progress: function (object) {
                            try {
                                //console.log("index " + indexFile + " loaded " + object.loaded + " total " + object.total);
                                var prog = Math.ceil((object.loaded / object.total) * 100);
                                if (callback) {
                                    callback.call(this, prog, indexFile);
                                }
                            } catch (e) {
                            }
                        }
                    },
                    data: fd,
                    transformRequest: angular.identity
                })
                    .then(postCompleted.bind(null, defered), postError.bind(null, defered));

            function postCompleted(defered, response) {

                if (callback) {
                    callback.call(this, 101, indexFile);
                }

                defered.resolve(response.data);
            }

            function postError(defered, response) {
                defered.reject(response);
            }

            return promise;

            //return http.post('/api/files', fd, {
            //    transformRequest: angular.identity,
            //    headers: { 'Content-Type': undefined }
            //});
        }

        function deleteContentFile(contentId, fileId) {
            return http.delete('/api/contents/' + contentId + '/files/' + fileId);
        }

        function postContentFile(contentId, file) {
            return http.post('/api/contents/' + contentId + '/files', file);
        }

        function sortContentFile(contentId, fileIdFrom, fileIdTo) {
            return http.post('/api/contents/' + contentId + '/files/' + fileIdFrom + '/sort/' + fileIdTo);
        }

        function postSocialNetwork(contentId, model)
        {
            return http.post('/api/contents/' + contentId + '/socialpost', model);
        }

        function patchContentFile(contentId, fileId, jsonPatch)
        {
            return http.patch('/api/contents/' + contentId + '/files/' + fileId, jsonPatch);
        }
    }
})();