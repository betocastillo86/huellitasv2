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

            var progressArray = [];
            
            function sendFile(e) {
                var fileUpload = element[0];
                progressArray = new Array();
                for (var i = 0; i < fileUpload.files.length; i++) {
                    fileService.post(fileUpload.files[i], scope.defaultname, onProgress, i)
                        .then(postCompleted)
                        .catch(helperService.handleException);
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

            function postCompleted(response) {

                progressArray = _.rest(progressArray, 1);
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