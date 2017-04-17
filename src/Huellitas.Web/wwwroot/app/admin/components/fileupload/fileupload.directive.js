(function () {
    angular.module('huellitasAdmin')
        .directive('fileuploadHuellitas', fileuploadHuellitas);

    fileuploadHuellitas.$inject = ['fileService'];

    function fileuploadHuellitas(fileService) {
        return {
            restrict: 'EA',
            link: link,
            scope: {
                onadded: '=',
                callbackParam :'@'
            }
        };

        function link(scope, element, attrs)
        {
            angular.element(element).on('change', sendFile);
            var isMultiple = angular.element(element)[0].attributes.multiple !== undefined;

            function sendFile(e)
            {
                var fileUpload = element[0];
                for (var i = 0; i < fileUpload.files.length; i++) {
                    fileService.post(fileUpload.files[i])
                     .then(postCompleted);
                }
            }

            function postCompleted(response)
            {
                //if it has a callback method for added then call it
                if (scope.onadded)
                {
                    scope.onadded(response.data, scope.callbackParam);
                }
            }
        }
    }

})();