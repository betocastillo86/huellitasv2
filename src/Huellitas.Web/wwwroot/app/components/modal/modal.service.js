(function () {
    angular.module('app')
        .factory('modalService', modalService);

    modalService.$inject = ['$q', '$templateRequest', '$rootScope', '$compile', '$controller', '$document'];

    //based on https://github.com/dwmkerr/angular-modal-service
    function modalService($q, $templateRequest, $rootScope, $compile, $controller, $document) {
        var vm = this;
        vm.show = show;
        vm.showError = showError;

        var defaultOptions = {
            modalType: 'default',
            scope: $rootScope,
            controllerAs: 'modal',
            title: 'Mensaje'
        };

        return vm;



        //searches the template by the modal type
        function getTemplate(modalType) {
            var deferred = $q.defer();

            var templateUrl = '/app/components/modal/modal-' + modalType + '.html';

            $templateRequest(templateUrl, true)
                .then(templateCompleted, templateError);

            return deferred.promise;

            function templateCompleted(template) {
                deferred.resolve(template);
            }

            function templateError(error) {
                deferred.reject(error);
            }
        }


        function show(options) {

            var deferred = $q.defer();

            //loads the defaults options
            options = _.defaults(options, defaultOptions);

            getTemplate(options.modalType)
            .then(templateLoaded)
            .catch(templateError);

            return deferred.promise;


            function templateLoaded(template) {

                //creates an scope
                var scope = options.scope.$new();

                //if the location changes then closes the modal
                var rootScopeOnClose = $rootScope.$on('$locationChangeSuccess', close);

                //compiles the themplate
                var templateCompiled = $compile(template);
                var modalElement = templateCompiled(scope);

                scope.close = close;
                scope.message = options.message;
                scope.title = options.title;

                //injected properties of the new controller
                var inputs = {
                    $scope: scope
                };

                var controllerObjBefore = scope[options.controllerAs];
                var controller = $controller(getControllerName(options.modalType), inputs, false, options.controllerAs);

                if (options.controllerAs && controllerObjBefore) {
                    angular.extend(controller, controllerObjBefore);
                }

                var body = angular.element($document[0].body);
                appendElement(body, modalElement);

                var closeDeferred = $q.defer();
                var closedDeferred = $q.defer();

                var modal = {
                    controller: controller,
                    scope: scope,
                    element: modalElement,
                    close: closeDeferred.promise,
                    closed: closedDeferred.promise
                };

                modal.element.on('hidden.bs.modal', function () {
                    close({previousClosed : true});
                })
                //calls the modal before resolving promise
                modal.element.modal();

                deferred.resolve(modal);

                function close(result) {
                    closeDeferred.resolve(result);

                    if (!result.previousClosed)
                    {
                        modal.element.modal('toggle');
                    }
                    
                    scope.$destroy();
                    closedDeferred.resolve(result);
                    // remove event watcher
                    rootScopeOnClose && rootScopeOnClose();
                }

                function appendElement(parent, child) {
                    var children = parent.children();
                    if (children.length > 0) {
                        return children[children.length - 1].append(child);
                    }
                    else {
                        return parent.append(child);
                    }
                }

                function getControllerName(modalType) {
                    modalType = modalType.toLowerCase();
                    switch (modalType) {
                        case 'default':
                        default:
                            return 'ModalDefaultController';
                    }
                }
            }

            function templateError() {
                console.log('template error');
            }
        }

        function showError(options)
        {
            var options = options || {};
            options.modalType = 'error';
            options.title = options.title || 'Error';
            
            //if it has an error shows the especific message
            if (options.error)
            {
                var message = '';
                var error = options.error;
                if (error.code === 'BadArgument')
                {
                    //validates the details to attatch to message
                    if (error.details)
                    {
                        message = _.pluck(error.details, 'message').join('<br>');
                    }
                    else
                    {
                        message = error.message || 'Algunos datos son invalidos';
                    }
                }
                else if(error.code === 'InvalidForeignKey')
                {
                    message = 'El campo ' + error.target + ' que intenta relacionar no existe';
                }

                options.message = message;
            }

            return show(options);
        }
    }
})();