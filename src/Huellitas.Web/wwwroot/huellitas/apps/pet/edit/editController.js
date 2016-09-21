﻿Huellitas.module('PetApp.Edit', function (Edit, App, Backbone, Marionette, $, _) {
    Edit.Controller = {
        create: function () {
            var that = this;
            var model = App.request('pet:new:entity');
            model.on('created', function () {
                App.vent.trigger('pet:item:clicked', model);
            });

            model.consoleAll();
            
            this.layout = this.getLayoutView(model);
            this.layout.on('show', function () {
                that.formRegion(model);
                that.titleRegion(model);
            });
            App.mainRegion.show(this.layout);
        },
        edit: function (id, model) {
            if (!model)
                model = App.request('pet:entity', id);

            var that = this;
            this.layout = that.getLayoutView(model);
            this.layout.on('show', function () {
                that.formRegion(model);
                that.titleRegion(model);
            });

            App.execute('when:fetched', [model], function () {
                App.mainRegion.show(that.layout);
            });
            
        },
        formRegion: function (model) {
            var editView = this.getEditView(model);
            editView.on('form:cancel', function () {
                App.vent.trigger('pet:cancelled', model);
            });
            var formView = App.request('form:wrapper', editView, {footer:true});
            this.layout.formRegion.show(formView);
        },
        titleRegion: function (model) {
            var titleView = this.getTitleView(model);
            this.layout.titleRegion.show(titleView);
        },
        getLayoutView: function (model) {
            return new Edit.Layout({model:model});
        },
        getEditView: function (model) {
            return new Edit.Pet({ model :model});
        },
        getTitleView: function (model) {
            return new Edit.Title({model:model});
        }
    }
});