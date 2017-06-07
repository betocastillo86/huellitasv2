
(function () {
    'use strict';

    angular
        .module('huellitas')
        .directive('listComments', listComments);

    function listComments() {
        return {
            /*scope: {
                contentid: '='
            },*/
            scope: false,
            templateUrl: '/app/front/components/contents/listComments.html',
            controller: 'ListCommentsController',
            controllerAs: 'listComments',
            bindToController: true,
            restrict: 'A'
        };
    }
})();

