Huellitas.module('Components.ImageGallery', function (ImageGallery, App, Backbone, Marionette, $, _) {
    ImageGallery.Layout = App.Views.Layout.extend({
        template: 'imageGallery/galleryLayout',
        regions: {
            listRegion: '.list-region',
            addRegion: '.add-region',
            fileUploadRegion: '.fileupload-region'
        },
        initialize: function (options) {
            this.title = options.title;
        },
        serializeData: function () {
            return {
                title: this.title
            };
        }
    });

    ImageGallery.Item = App.Views.ItemView.extend({
        template: 'imageGallery/galleryItem',
        triggers: {
            'click .fa-times'   : 'item:delete',
            'click .fa-pencil'  : 'item:edit'
        },
        modelEvents: {
            'change:fileName' : 'changeImageSrc'
        },
        changeImageSrc: function (model, value) {
            this.$('.mainImage').attr('src', value);
        },
        serializeData: function () {
            return {
                image: this.model.get(this.imageField),
                id: this.model.get(this.idField),
                title: this.model.get(this.titleField),
                canDelete: this.canDelete,
                canEdit: this.canEdit
            };
        }
    });

    ImageGallery.List = App.Views.CollectionView.extend({
        childView: ImageGallery.Item,
        initialize: function () {
            this.setInstancePropertiesFor('imageField', 'idField', 'titleField', 'canDelete', 'canEdit', 'canAdd', 'onFileDelete');
            //this.on('itemview:before:render', this.onBeforeItemAdded, this);
        },
        onBeforeAddChild: function (view) {
            view.imageField = this.imageField;
            view.idField = this.idField;
            view.titleField = this.titleField;
            view.canDelete = this.canDelete;
            view.canEdit = this.canEdit;
        }
    });

    ImageGallery.AddItem = App.Views.ItemView.extend({
        template: 'imageGallery/galleryAddItem',
        triggers: {
            'click': 'additem:click'
        }
    });
});