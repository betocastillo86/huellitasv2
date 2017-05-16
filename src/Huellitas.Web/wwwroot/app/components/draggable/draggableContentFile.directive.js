
(function () {
    'use strict';

    angular
        .module('huellitasComponents')
        .directive('draggableContentFile', draggableContentFile);

    draggableContentFile.$inject = ['$window', 'fileService'];

    function draggableContentFile($window, fileService) {

        return {
            link: link,
            restrict: 'A',
            scope: {
                contentid: '=',
                id: '=',
                callback: '=',
                saveonchange: '@',
                files: '='
            }
        };

        function link(scope, element, attrs) {

            element.on('dragstart', function (ev) {
                $window.dragObject = {};
                $window.dragObject.id = scope.id;
            });

            element.on('dragend', function (ev) {
                $window.dragObject = {};
            });

            element.on('dragover', function (ev) {
                ev.preventDefault();

                if (element[0].className.indexOf('over') === -1 && $window.dragObject &&
                    $window.dragObject.id !== scope.id) {
                    element.addClass('over');
                }
            });

            element.on('dragleave', function (ev) {
                ev.preventDefault();
                angular.element(document.getElementsByClassName('over')).removeClass('over');
            });

            element.on('drop', function (ev) {
                ev.preventDefault();
                angular.element(document.getElementsByClassName('over')).removeClass('over');

                var toId = scope.id;

                if (toId)
                {
                    var fromId = $window.dragObject.id;

                    //var fromElement = _.findWhere(scope.$parent.main.model.files, { id: parseInt(fromId) });
                    //var toElement = _.findWhere(scope.$parent.main.model.files, { id: parseInt(toId) });
                    var fromElement = _.findWhere(scope.files, { id: parseInt(fromId) });
                    var toElement = _.findWhere(scope.files, { id: parseInt(toId) });

                    //var indexTo = scope.$parent.main.model.files.indexOf(toElement);
                    //var indexFrom = scope.$parent.main.model.files.indexOf(fromElement);
                    var indexTo = scope.files.indexOf(toElement);
                    var indexFrom = scope.files.indexOf(fromElement);

                    if (scope.saveonchange === "true")
                    {
                        fileService.sortContentFile(scope.contentid, parseInt(fromId), parseInt(toId))
                            .then(move.bind(null, indexFrom, indexTo, fromElement))
                            .catch(errorMoving);
                    }
                    else
                    {
                        scope.$apply(move.bind(null, indexFrom, indexTo, fromElement));
                    }

                    function errorMoving()
                    {
                        console.log('Error moviendo');
                    }
                }

            });

            function move(indexFrom, indexTo, contentFrom) {
                
                //scope.$parent.main.model.files.splice(indexFrom, 1);
                //scope.$parent.main.model.files.splice(indexTo, 0, contentFrom);
                scope.files.splice(indexFrom, 1);
                scope.files.splice(indexTo, 0, contentFrom);
                
                if (scope.callback) {
                    //scope.callback(scope.$parent.main.model.files);
                    scope.callback(scope.files);
                }
            }
        }
    }
})();

