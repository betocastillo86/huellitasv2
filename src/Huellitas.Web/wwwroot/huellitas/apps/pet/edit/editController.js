Huellitas.module('PetApp.Edit', function (Edit, App, Backbone, Marionette, $, _) {
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
            var formView = App.request('form:wrapper', editView, { footer: true });
            this.layout.formRegion.show(formView);
        },
        titleRegion: function (model) {
            var titleView = this.getTitleView(model);
            this.layout.titleRegion.show(titleView);
        },
        getLayoutView: function (model) {
            return new Edit.Layout({ model: model });
        },
        getEditView: function (model) {
            var statusTypes = App.request('statusType:entities:cached');
            var genres = App.request('customtable:row:entities:cached', Huellitas.settings.customTable.genre);
            var sizes = App.request('customtable:row:entities:cached', Huellitas.settings.customTable.size);
            var subtypes = App.request('customtable:row:entities:cached', Huellitas.settings.customTable.subtype);
            var shelters = App.request('shelter:entities');
            return new Edit.Pet({ model: model, statusTypes: statusTypes, genres: genres, sizes: sizes, subtypes: subtypes, shelters: shelters });
        },
        getTitleView: function (model) {
            return new Edit.Title({ model: model });
        }
    };
});