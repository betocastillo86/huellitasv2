(function () {
    angular.module('app')
        .directive('fileuploadHuellitas', fileuploadHuellitas);

    fileuploadHuellitas.$inject = ['fileService'];

    function fileuploadHuellitas(fileService) {
        return {
            restrict: 'EA',
            link: link,
            scope: false
        };

        function link(scope, element, attrs)
        {
            var model = scope.$eval(attrs.obj);

            angular.element(element).on('change', sendFile);

            function sendFile(e)
            {
                fileService.post(element[0].files[0])
                .then(postCompleted);
            }

            function postCompleted(response)
            {
                model.id = response.id;
                model.thumbnail = response.thumbnail;
            }
        }
    }

})();