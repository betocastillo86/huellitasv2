
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

    angular
        .module('huellitas')
        .controller('ListCommentsController', ListCommentsController);

    ListCommentsController.$inject = ['$scope', '$attrs', 'helperService', 'commentService'];

    function ListCommentsController($scope, $attrs, helperService, commentService)
    {
        var vm = this;
        vm.comments = [];
        vm.hasNextPage = false;
        vm.newComment = undefined;
        vm.newChild = undefined;
        vm.filter = {
            pageSize: 5,
            page: 0,
            orderBy: 'recent',
            withChildren: true
        };

        vm.saveComment = saveComment;
        vm.enableResponse = enableResponse;
        vm.saveChild = saveChild;
        vm.showMoreChildren = showMoreChildren;
        vm.showMore = showMore;

        activate();

        function activate()
        {
            vm.filter.contentId = $attrs.contentid ? $scope.$eval($attrs.contentid) : undefined;

            if (!vm.filter.contentId)
            {
                throw 'No se ingreso el filtro de contenido';
            }
            
            getComments();
        }

        function getComments()
        {
            commentService.getAll(vm.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                vm.hasNextPage = response.meta.hasNextPage;

                if (vm.comments.length) {
                    vm.comments = vm.comments.concat(response.results);
                }
                else
                {
                    vm.comments = response.results;
                }
            }
        }

        function saveComment()
        {
            if (vm.form.$valid && !vm.isBusy)
            {
                var comment = {
                    value: vm.newComment,
                    contentId: vm.filter.contentId
                };

                commentService.post(comment)
                    .then(postCompleted)
                    .catch(postError);

                function postCompleted(response)
                {
                    vm.filter.page = 0;
                    vm.comments = [];
                    vm.newComment = undefined;
                    vm.form.$submitted = false;
                    getComments();
                }

                function postError()
                {
                    debugger;
                }
            }
        }

        function enableResponse(comment)
        {
            for (var i = 0; i < vm.comments.length; i++) {
                vm.comments[i].isAnswering = vm.comments[i].id == comment.id;
            }
        }

        function saveChild(parentComment)
        {
            if (vm.formChildren.$valid && !vm.isBusy) {
                var comment = {
                    value: vm.newChild,
                    parentCommentId: parentComment.id
                };

                commentService.post(comment)
                    .then(postCompleted)
                    .catch(postError);

                function postCompleted(response) {
                    vm.filter.page = 0;
                    vm.newChild = undefined;
                    parentComment.firstComments = [];
                    parentComment.filter = undefined;
                    parentComment.isAnswering = false;
                    vm.formChildren.$submitted = false;
                    showMoreChildren(parentComment);
                }

                function postError() {
                    debugger;
                }
            }
        }

        function showMoreChildren(comment)
        {
            if (comment.filter) {
                comment.filter.page++;
            }
            else
            {
                comment.filter = {
                    page: 0,
                    pageSize: 5,
                    parentId : comment.id
                };
            }
            commentService.getAll(comment.filter)
                .then(getCompleted)
                .catch(helperService.handleException);

            function getCompleted(response)
            {
                comment.firstComments = (comment.filter.page > 0 ?  comment.firstComments : []).concat(response.results);
                comment.allCommentsLoaded = !response.meta.hasNextPage;
            }
        }

        function showMore()
        {
            vm.filter.page++;
            getComments();
        }
    }

})();

