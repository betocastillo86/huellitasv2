(function () {
    angular.module('huellitasAdmin')
        .controller('GalleryController', GalleryController);

    GalleryController.$inject = ['$scope', '$attrs', 'fileService', 'modalService', 'helperService'];

    function GalleryController($scope, $attrs, fileService, modalService, helperService)
    {
        var vm = this;
        vm.model = {};
        vm.model.images = $scope.images;
        vm.saveonreorder = $scope.saveonreorder;
        vm.model.title = $scope.title;
        vm.model.socialpost = $scope.socialpost === '1';
        vm.model.width = $scope.width || '100%';
        vm.model.height = $scope.height || '100%';
        vm.contentid = $scope.contentid;
        vm.progressFiles = [];

        vm.defaultName = $scope.defaultname;
        vm.removeImageCallback = $scope.ondelete;
        vm.removeImage = removeImage;
        vm.imageAdded = imageAdded;
        vm.getImageByIndex = getImageByIndex;
        vm.reorder = reorder;
        vm.onProgress = onProgress;
        vm.createSocialPost = createSocialPost;
        vm.resizeImage = resizeImage;

        return activate();

        function activate()
        {
            $attrs.$observe('defaultname', updateDefaultName);
        }

        function updateDefaultName(newValue)
        {
            vm.defaultName = newValue;
        }

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

        function onProgress(progressFiles)
        {
            console.log(progressFiles);
            vm.progressFiles = progressFiles;
        }

        function reorder(files)
        {
            vm.model.images = files;
        }

        function createSocialPost(image)
        {
            modalService.show({
                controller: 'CreateSocialPostController',
                template: '/app/admin/contents/createSocialPost.html?' + app.Settings.general.configJavascriptCacheKey,
                params: {
                    contentId: vm.contentid,
                    fileId: image.id
                }
            });
        }

        function resizeImage(image)
        {
            if (confirm('¿Seguro desea generar la imagen nuevamente?'))
            {
                fileService.patchContentFile(vm.contentid, image.id, [{ path: '/resize', op: 'replace', value: 'land' }])
                    .then(resizeCompleted)
                    .catch(helperService.handleException);
            }

            function resizeCompleted()
            {
                modalService.show({ message: 'Imagen redimensionada correctamente' });
                image.thumbnail = image.thumbnail + '?rand=' + new Date().getDate();
            }
        }
    }
})();