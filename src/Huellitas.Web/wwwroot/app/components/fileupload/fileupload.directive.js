(function () {
    angular.module('huellitasComponents')
        .directive('fileuploadHuellitas', fileuploadHuellitas);

    fileuploadHuellitas.$inject = ['fileService', 'helperService'];

    function fileuploadHuellitas(fileService, helperService) {
        return {
            restrict: 'EA',
            link: link,
            scope: {
                onadded: '=',
                onprogress: '=',
                callbackParam: '@',
                defaultname: '@'
            }
        };

        function link(scope, element, attrs) {
            angular.element(element).on('change', sendFile);
            var isMultiple = angular.element(element)[0].attributes.multiple !== undefined;

            attrs.$observe('defaultname', updateDefaultName);

            //// Contiene el progreso de cada archivo
            var progressArray = [];
            //// Contiene el indice de los archivos ya cargados
            var iFileSent = 0;

            function sendFile(e) {
                var fileUpload = element[0];

                //progressArray = new Array();

                var errorSize = false;

                for (var i = 0; i < fileUpload.files.length; i++) {

                    if (fileUpload.files[i].size <= app.Settings.security.maxRequestFileUploadMB * 1024 * 1024) {
                        console.log('Agrega archivo', iFileSent);
                        fileService.post(fileUpload.files[i], scope.defaultname, onProgress, iFileSent)
                            .then(postCompleted)
                            .catch(helperService.handleException);
                        iFileSent++;
                    }
                    else
                    {
                        errorSize = true;
                    }
                }

                if (errorSize) {
                    var message = '';
                    if (fileUpload.files.length == 1) {
                        message = 'El archivo no puede exceder las ' + app.Settings.security.maxRequestFileUploadMB + 'MB. Subir archivos de menor peso.';
                    }
                    else if (iFileSent == 0)
                    {
                        message = 'Los archivos no pueden exceder las ' + app.Settings.security.maxRequestFileUploadMB + 'MB. Subir archivos de menor peso.';
                    }
                    else{
                        message = 'Hay archivos que exceden las ' + app.Settings.security.maxRequestFileUploadMB + 'MB. Subir archivos de menor peso.';
                    }


                    helperService.handleException({ data: { error: { message: message } } });
                }
            }

            function onProgress(percentage, indexFile)
            {
                progressArray[indexFile] = percentage;
                if (scope.onprogress)
                {
                    scope.onprogress(progressArray);
                }
            }

            function updateDefaultName(newValue) {
                scope.defaultname = newValue;
            }

            function postCompleted(response, percentage, indexFile) {

                var finishedLoad = _.reject(progressArray, function (el) {
                    return el == 101;
                });

                if (!finishedLoad.length)
                {
                    console.log("Se resetea array", progressArray);
                    progressArray = [];
                    iFileSent = 0;
                }

                if (scope.onprogress) {
                    scope.onprogress(progressArray);
                }

                //if it has a callback method for added then call it
                if (scope.onadded) {
                    scope.onadded(response, scope.callbackParam);
                }
            }
        }
    }

})();