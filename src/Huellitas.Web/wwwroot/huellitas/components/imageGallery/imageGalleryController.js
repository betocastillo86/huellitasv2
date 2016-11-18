Huellitas.module('Components.ImageGallery', function (ImageGallery, App, Backbone, Marionette, $, _) {
    ImageGallery.Controller = Marionette.Controller.extend({
        getListLayout: function (options) {
            options = this.getDefaultOptionsLayout(options);

            this.layout = new ImageGallery.Layout(options);

            ////Su tiene una coleccion se actualiza automáticamente
            ////if (options.collection)
            ////    this.layout.listenTo(options.collection, 'change', this.reloadList);

            this.layout.on('show', function () {
                this.listRegion(options);
                this.addRegion(options);
                this.uploadFileRegion(options);
            }, this);

            //this.layout.render();
            return this.layout;
        },
        listRegion: function (options) {
            var view = this.getListView(options);

            view.on('childview:item:delete', function (childView) {
                //Valida con la vista superior si lo puede eliminar y si no es nuevo, hasta que no se destruya el objeto no lo borra
                if (view.onFileDelete({ model: childView.model })) {
                    if (childView.model && !childView.model.isNew()) {
                        childView.model.consoleAll();
                        childView.model.destroy();
                        childView.model.once('destroy', function () { this.deleteFile(view, childView); }, this);
                    }
                    else {
                        this.deleteFile(view.childView);
                    }
                }
            }, this);

            view.on('childview:item:edit', function (childView) {
                var that = this;
                var uploadViewEdit = App.request('fileupload:view', { url: options.url, multiple: options.multiple });
                uploadViewEdit.on('file:sent', function (file) {
                    childView.model.set('fileName', file.fileName);

                    if (!childView.model.isNew()) {
                        
                        childView.model.save();
                    }
                    
                }, that);
                uploadViewEdit.open();
            });

            return this.layout.listRegion.show(view);
        },
        deleteFile: function (listView, childView) {
            this.listView.removeChildView(childView);
            this.layout.trigger('file:deleted');
        },
        uploadFileRegion: function (options) {
            if (!options.url) {
                throw new Error("La Url es obligatoria");
            }
            this.uploadView = App.request('fileupload:view', { url: options.url, multiple: options.multiple });
            this.uploadView.on('file:sent', this.onUploadFileSent, this);
            this.uploadView.on('file:error', this.onUploadFileError, this);
            return this.layout.fileUploadRegion.show(this.uploadView);
        },
        addRegion: function () {
            var view = this.getAddView();
            view.on('additem:click', this.openUpload, this);
            return this.layout.addRegion.show(view);
        },
        openUpload: function () {
            this.uploadView.open();
        },
        getDefaultOptionsLayout: function (options) {
            return _.defaults(options, {
                title: 'Imagenes de Galeria'
            });
        },
        getListView: function (options) {
            options = this.getDefaultOptionsList(options);
            this.listView = new ImageGallery.List(options);
            return this.listView;
        },
        //reloadList : function(){
        //    this.listView.reloadList();
        //},
        getDefaultOptionsList: function (options) {
            return _.defaults(options, {
                imageField: 'fileName',
                idField: 'id',
                titleField: 'name',
                canDelete: true,
                canEdit: true,
                canAdd: true,
                onFileDelete: function () { return true; }
            });
        },
        getAddView: function () {
            this.addView = new ImageGallery.AddItem();
            return this.addView;
        },
        onUploadFileSent: function (data) {
            this.layout.trigger('file:sent', { data: data, model: this.layout.model });
        },
        onUploadFileError: function (data) {
            this.layout.trigger('file:error', { data: data, model: this.layout.model });
        }
    });


    App.reqres.setHandler('imagegallery:view', function (options) {
        var controller = new ImageGallery.Controller();
        return controller.getListLayout(options);
    });
});