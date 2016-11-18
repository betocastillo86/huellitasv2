Huellitas.module('Components.Form', function (Form, App, Backbone, Marionette, $, _) {
    Form.FormWrapper = App.Views.Layout.extend({
        template: 'form/form',
        tagName: 'form',
        className: 'form-horizontal form-label-left',
        initialize: function () {
            this.setInstancePropertiesFor('config', 'buttons', 'bindings', 'goToFocus');
        },
        modelEvents: {
            'validated:invalid'     : 'changeErrors',
            'validated:valid'       : 'removeErrors',
            'sync:start'            : 'syncStart',
            'sync:stop'             : 'syncStop'
        },
        triggers: {
            'submit': 'form:submit',
            'click button[data-form-button="cancel"]': 'form:cancel'
        },
        attributes: function () {
            return { 'data-type': this.getFormDataType() };
        },
        regions: {
            formContentRegion: '#form-content-region'
        },
        serializeData: function () {
            return {
                footer: this.config.footer,
                buttons: this.buttons !== undefined ? this.buttons.toJSON() : undefined
            };
        },
        onShow: function () {
            var that = this;
            if (this.config.focusFirstInput)
                _.defer(function () { that.focusFirstInput(); });
        },
        changeErrors: function (model, errors) {

            if (this.bindings) {
                var fieldsToMark = {};
                var that = this;
                this.removeErrors();
                _.each(this.bindings, function (element, index) {

                    var inputEl = that.$(index);
                    //Si es un objeto busca la propiedad en el campo observe
                    if (_.isObject(element)) {
                        //fieldsToMark[element['observe']] = element['controlToMark'] ? that.$(element['controlToMark']) : inputEl.closest('.item');

                        var markError = undefined;
                        if (element['controlToMark']) {
                            markError = { inputEl: that.$(element['controlToMark']), containerEl: that.$(element['controlToMark']).closest('.item') };
                        }
                        else {
                            markError= { inputEl: inputEl, containerEl: inputEl.closest('.item') };
                        }

                        fieldsToMark[element['observe']] = markError;
                    }
                    else {
                        fieldsToMark[element] = { inputEl: inputEl, containerEl: inputEl.closest('.item') };
                    }
                });

                this.markErrorsOnForm(errors, fieldsToMark);
            }

        },
        markErrorsOnForm: function (errors, fieldsToMark) {
            var that = this;
            _.each(errors, function (errorMessage, index) {
                //recorre los errores y marca solo los que tienen objeto DOM
                var el = fieldsToMark[index];
                if (el) {
                    var message = $('<div class="alert"><div>').text(errorMessage);
                    el.inputEl.parent().after(message);
                    el.containerEl.addClass("bad");
                }
            });

            if (this.config.goToFocus)
            {
                this.scrollFocusObject('.bad:first', -15);
            }
        },
        scrollFocusObject: function (selector, addPixels) {
            //Valida que el selector exista
            var obj = this.$(selector);
            if (!obj.length)
                return;
            if (addPixels == undefined)
                addPixels = 0;

            var position = 0;
            if (obj.offset() != undefined)
                position = obj.offset().top;
            $('html, body').animate({
                scrollTop: position + addPixels
            }, 500);
        },
        syncStart: function (model) {
            if (this.config.syncing)
                this.addOpacityWrapper(true);
        },
        syncStop: function (model) {
            if (this.config.syncing)
                this.addOpacityWrapper(false);
        },

        removeErrors: function () {
            this.$(".bad").removeClass("bad");
            this.$('div.alert').remove();
        },
        focusFirstInput: function () {
            return this.$(':input:visible:enabled:first').focus();
        },
        getFormDataType: function () {
            return this.model.isNew() ? 'new' : 'edit';
        }
    });
});