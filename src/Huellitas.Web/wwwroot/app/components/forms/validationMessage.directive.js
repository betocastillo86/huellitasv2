﻿(function () {
    angular.module('app')
        .directive('validationMessage', validationMessage);

    function validationMessage() {
        var directive = {
            restrict: 'E',
            template: '<ul class="parsley-errors-list filled" ng-if="form.$submitted && !field.$valid"><li class="parsley-required">{{getMessage(field)}}</li></ul>',
            scope: {
                form: '=',
                field: '=',
                maxlength: '@ngMaxlength',
                min: '@ngMin',
                max: '@ngMax',
                name: '@'
            },
            link: link
        };

        return directive;

        function link(scope, element, attrs) {
            scope.getMessage = getMessage;

            function getMessage(field) {
                var fieldName = scope.name ? 'Campo '+scope.name : 'Campo';

                if (!field.$valid && field.$error) {
                    if (field.$error['required']) {
                        return fieldName + ' es obligatorio';
                    }
                    else if (field.$error['email']) {
                        return fieldName + ' no es un correo valido';
                    }
                    else if (field.$error['min']) {
                        if (scope.min) {
                            return fieldName + ' no puede ser menor a ' + scope.min;
                        }
                        else {
                            return 'Valor mínimo invalido';
                        }
                    }
                    else if (field.$error['max']) {
                        if (scope.max) {
                            return fieldName + ' no puede ser mayor a ' + scope.max;
                        }
                        else {
                            return 'Valor mínimo invalido';
                        }
                    }
                    else if (field.$error['maxlength']) {
                        if (scope.maxlength) {
                            return fieldName + ' excede los ' + scope.maxlength + ' caracteres.';
                        }
                        else {
                            return fieldName + ' excede el máximo de caracteres';
                        }
                    }
                    else {
                        return fieldName + ' invalido';
                    }
                }
            }
        }
    }

})();