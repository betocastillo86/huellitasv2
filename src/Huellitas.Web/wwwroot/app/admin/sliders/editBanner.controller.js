(function () {
    angular.module('huellitasAdmin')
        .controller('EditBannerController', EditBannerController);

    EditBannerController.$inject = ['$routeParams', '$location', 'bannerService', 'modalService', 'helperService'];

    function EditBannerController($routeParams, $location, bannerService, modalService, helperService)
    {
        var vm = this;
        vm.model = {};
        vm.id = $routeParams.id;
        vm.continueAfterSaving = false;
        vm.isSending = false;

        vm.save = save;
        vm.saveAndContinue = saveAndContinue;
        vm.changeActive = changeActive;
        vm.imageAdded = imageAdded;

        activate();

        return vm;

        function activate()
        {
            if (vm.id)
            {
                getBanner();
            }
        }

        function getBanner()
        {
            bannerService.getById(vm.id)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.model = response;
            }
        }

        function save(isValid)
        {
            if (isValid && !vm.isSending) {

                vm.isSending = true;

                if (vm.model.id > 0) {
                    bannerService.put(vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }
                else {
                    bannerService.post(vm.model)
                        .then(saveCompleted)
                        .catch(saveError);
                }
            }

            function saveCompleted(response) {

                vm.isSending = false;

                response = response;
                var message = 'El banner fue actualizado correctamente';
                var isNew = !vm.model.id;

                if (isNew) {
                    message = 'El banner se ha creado correctamente';
                    vm.model.id = response.id;
                }

                modalService.show({
                    message: message
                })
                    .then(function (modal) {
                        modal.closed.then(function () {
                            if (vm.continueAfterSaving) {
                                //if it is new and want to continue updates the location
                                if (isNew) {
                                    $location.path('/banners/' + vm.model.id + '/edit');
                                }
                            }
                            else {
                                $location.path('/banners');
                            }

                            vm.continueAfterSaving = false;
                        });
                    });
            }

            function saveError(response) {
                vm.isSending = false;
                helperService.handleException(response);
            }
        }

        function imageAdded(file, previousFile)
        {
            vm.model.fileId = file.id;
            vm.model.fileUrl = file.thumbnail;
        }

        function saveAndContinue()
        {
            vm.continueAfterSaving = true;
        }

        function changeActive(active)
        {
            vm.model.active = active;
        }
    }
})();