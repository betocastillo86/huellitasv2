Huellitas.module('PetApp.Edit', function (Edit, App, Backbone, Marionette, $, _) {
    Edit.Controller = {
        create: function () {
            var that = this;
            var model = App.request('pet:new:entity');
            model.on('created', function () {
                App.vent.trigger('pet:item:clicked', model);
            });

            this.layout = this.getLayoutView(model);
            this.layout.on('show', function () {
                that.formRegion(model);
                that.titleRegion(model);
                that.galleryRegion(model);
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
                that.galleryRegion(model);
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
        galleryRegion: function (model) {
            var view = this.getGalleryView(model);
            view.on('file:sent', this.onFileSent, this);
            view.on('file:error', this.onFileError, this);
            view.on('file:delete', this.onFileDelete, this);
            this.layout.galleryRegion.show(view);
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
        },
        onFileSent: function (fileInfo) {
            //Si el modelo es nuevo lo agrega a la coleccion directamente, sino lo envía al servidor
            var model = fileInfo.model;
            if (model.isNew()) {
                this.fileCollection.add({ name: fileInfo.data.fileName, fileName: '/img/content/000000/1_imagen1.jpg' });
                model.set('images', this.fileCollection.toJSON());
            }
            else {
                var newFile = App.request('file:new:entity');
                newFile.set({ contentId : model.get('id'), name: fileInfo.data.fileName, fileName: fileInfo.data.fileName });
                newFile.once('sync', this.fileSaved, this);
                newFile.once('error', this.onFileError, this);
                newFile.addToContent();
            }
        },
        onFileError: function (data) {
            alert('Error cargando el archivo');
        },
        onFileDelete: function () {
            return confirm('¿Está seguro de eliminar este archivo?');
        },
        fileSaved: function (model) {
            this.fileCollection.add(model);
        },
        getGalleryView: function (model) {
            if (model.isNew())
            {
                this.fileCollection = App.request('file:new:entities');
            }
            else {
                this.fileCollection = App.request('file:entities:byContent', model.get('id'));
            }
            
            return App.request('imagegallery:view', {
                title: 'Mi titulo',
                model: model,
                collection: this.fileCollection,
                url: '/api/uploadfiles/',
                multiple: true,
                onFileDelete : this.onFileDelete
            });
        },
    };
});