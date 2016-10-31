Huellitas.module('Components.Form', function (Form, App, Backbone, Marionette, $, _) {
    Form.Controller = Marionette.Controller.extend({
        initialize: function (args) {
            args = args || {};
            this.contentView = args.view;

            this.formLayout = this.getFormLayout(args.config);
            //this.formLayout.on('show', this.showFormContentRegion, this);

            this.listenTo(this.formLayout, 'show', this.showFormContentRegion);
            this.listenTo(this.formLayout, 'destroy', this.destroy);
            this.listenTo(this.formLayout, 'form:submit', this.formSubmit);
            this.listenTo(this.formLayout, 'form:cancel', this.formCancel);
            this.listenTo(this.formLayout.formContentRegion, 'show', this.formRender);
        },
        onDestroy: function () {
            console.log('onDestroy', this);
        },
        showFormContentRegion: function () {
            this.formLayout.formContentRegion.show(this.contentView);
            
            if (this.contentView.bindings)
                this.contentView.stickit();

            if (this.contentView.model.validation)
                Backbone.Validation.bind(this.contentView);                
        },
        getFormLayout: function (args) {
            args = args || {};
            var config = this.getDefaultConfig(_.result(this.contentView, 'form'))
            _.extend(config, args);
            var buttons = this.getButtons(config.buttons);
            return new Form.FormWrapper({
                config: config,
                model: this.contentView.model,
                buttons: buttons,
                bindings: this.contentView.bindings
            });
        },
        formSubmit: function () {
            if (this.contentView.triggerMethod('form:submit') && this.validateModel())
            {
                var model = this.contentView.model;
                var collection = this.contentView.collection;
                this.processFormSubmit(model, collection);
            }
        },
        formRender: function () {
            this.contentView.triggerMethod('form:render');
        },
        formCancel: function () {
            this.contentView.triggerMethod('form:cancel');
        },
        validateModel: function () {
            var model = this.contentView.model;
            model.validate();
            return model.isValid();
        },
        processFormSubmit: function (model, collection) {
            model.save({}, {collection :collection});
        },
        getDefaultConfig: function (config) {
            config = config || {};
            return _.defaults(config, {
                footer: true,
                focusFirstInput: true,
                errors: true,
                syncing:true
                //buttons: this.getDefaultButtons(config.buttons)
            });
        },
        getButtons: function (buttons) {
            if (buttons !== false)
                return App.request('form:buttons:entities', buttons || {}, this.contentView.model);
        },
        //getDefaultButtons: function (buttons) {
        //    buttons = buttons ||{};
        //    _.defaults(buttons, {
        //        primary: 'Enviar',
        //        cancel: 'Cancelar',
        //        placement: 'left'
        //    });
        //}
    });

    App.reqres.setHandler('form:wrapper', function (contentView, options) {
        if (!contentView.model)
            throw new Error('Not model found in form view');

        var formController = new Form.Controller({ view: contentView, config: options });
        return formController.formLayout;
    });
});