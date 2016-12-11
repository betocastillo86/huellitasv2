(function () {
    angular.module('app')
        .controller('GalleryController', GalleryController);

    GalleryController.$inject = ['$scope', 'fileService'];

    function GalleryController($scope, fileService)
    {
        var vm = this;
        vm.model = {};
        vm.model.images = $scope.images;
        vm.model.title = $scope.title;
        vm.removeImageCallback = $scope.ondelete;
        vm.removeImage = removeImage;
        vm.imageAdded = imageAdded;
        vm.getImageByIndex = getImageByIndex;

        function removeImage(file)
        {
            if (confirm('¿Está seguro de eliminar esta imagen?'))
            {
                //if it has a callback after removing an image call it, otherwise deletes the file on the server
                if (!vm.removeImageCallback) {
                    fileService.deleteFile(file);
                }
                else {
                    vm.removeImageCallback(file);
                }

                var index = vm.model.images.indexOf(file);
                vm.model.images.splice(index, 1);
            }
        }

        function getImageByIndex(index)
        {
            return vm.model.images[index];
        }

        function imageAdded(file, previousFile)
        {
            //Valites if it has a previous file to replace it
            if (previousFile) {
                previousFile = JSON.parse(previousFile);
                var index = _.findIndex(vm.model.images, function (el) { return el.id == previousFile.id });
                vm.model.images[index] = file;
            }
            else {
                vm.model.images = vm.model.images || [];
                vm.model.images.push(file);
            }

            //calls the method after adding it
            $scope.onadded(file, previousFile);
        }
    }
})();