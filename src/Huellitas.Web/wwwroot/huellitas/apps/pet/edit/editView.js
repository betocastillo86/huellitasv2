Huellitas.module('PetApp.Edit', function (Edit, App, Backbone, Marionette, $, _) {
    Edit.Layout = App.Views.Layout.extend({
        template: 'pet/edit/editLayout',
        regions: {
            formRegion: '#form-region',
            titleRegion: '#title-region',
            galleryRegion:'#gallery-region'
        }
    });

    Edit.Pet = App.Views.ItemView.extend({
        template: 'pet/edit/editPet',
        modelEvents: {
            'change:shelter': 'shelterChanged'
        },
        events: {
            'click .moreOptions': 'showMoreOptions'
            //'click #divLoadFile': 'uploadFile'
        },
        bindings: {
            '.txtName': 'name',
            '.ddlStatusType': {
                observe: 'status',
                selectOptions: {
                    collection: 'this.statusTypes',
                    labelPath: 'name',
                    valuePath: 'id',
                    defaultOption: {
                        label: '-',
                        value: undefined
                    }
                }
            },
            '.ddlSubtype': {
                observe: 'subtype',
                selectOptions: {
                    collection: 'this.subtypes',
                    labelPath: 'value',
                    valuePath: 'id',
                    defaultOption: {
                        label: '-',
                        value: undefined
                    }
                },
                onGet: function (value) {
                    var value = this.model.get('subtype');
                    return value ? value.value : undefined;
                },
                onSet: function (value) {
                    return !isNaN(value) ? { value: parseInt(value) } : undefined;
                }
            },
            '.ddlSize': {
                observe: 'size',
                selectOptions: {
                    collection: 'this.sizes',
                    labelPath: 'value',
                    valuePath: 'id',
                    defaultOption: {
                        label: '-',
                        value: undefined
                    }
                },
                onGet: function (value) {
                    var size = this.model.get('size');
                    return size ? size.value : undefined;
                },
                onSet: function (value) {
                    return !isNaN(value) ? { value: parseInt(value) } : undefined;
                }
            },
            '.ddlGenre': {
                observe: 'genre',
                selectOptions: {
                    collection: 'this.genres',
                    labelPath: 'value',
                    valuePath: 'id',
                    defaultOption: {
                        label: '-',
                        value: undefined
                    }
                },
                onGet: function (value) {
                    var value = this.model.get('genre');
                    return value ? value.value : undefined;
                },
                onSet: function (value) {
                    return !isNaN(value) ? { value: parseInt(value) } : undefined;
                }
            },
            '.ddlShelter': {
                observe: 'shelter',
                selectOptions: {
                    collection: 'this.shelters',
                    labelPath: 'name',
                    valuePath: 'id',
                    defaultOption: {
                        label: '-',
                        value: undefined
                    }
                },
                onGet: function (value) {
                    var value = this.model.get('shelter');
                    return value ? value.id : undefined;
                },
                onSet: function (value) {
                    return !isNaN(value) ? { id: parseInt(value) } : undefined;
                }
            },
        },
        initialize: function (args) {
            this.statusTypes = args.statusTypes;
            this.genres = args.genres;
            this.sizes = args.sizes;
            this.subtypes = args.subtypes;
            this.shelters = args.shelters;
            //this.fileView = App.request('fileupload:view', { url: '/api/fileupload', multiple:true});
            //this.galleryView = args.gallery;
            //this.model.consoleAll();
        },
        //uploadFile: function () {
        //    this.fileView.open();
        //},
        loadAutoCompleteLocation: function () {
            this.autocompleteView = App.request('autocomplete:text',
                '.txtLocation',
                this.model,
                {
                    url: '/api/locations',
                    selector: '.txtLocation',
                    observe: 'location',
                    isObjectType: true
                }
            );
        },
        loadAutoCompleteUser: function () {
            this.autocompleteView = App.request('autocomplete:text',
                '.txtUser',
                this.model,
                {
                    url: '/api/users',
                    selector: '.txtUser',
                    observe: 'user',
                    isObjectType: true
                }
            );
        },
        shelterChanged: function () {
            this.$('.rowLocation').css('display', this.model.get('shelter') ? 'none' : 'block');
        },
        showMoreOptions: function () {
            this.isShowingMoreOptions = !this.isShowingMoreOptions;
            this.$('.divMoreOptions').css('display', this.isShowingMoreOptions ? 'block' : 'none');
        },
        onFormSubmit: function () {
            return true;
        },
        onFormRender: function () {
            this.loadAutoCompleteLocation();
            this.loadAutoCompleteUser();
        }
    });

    Edit.Title = App.Views.ItemView.extend({
        template: 'pet/edit/editTitle',
        modelEvents: {
            'updated': 'render'
        },
        serializeData: function () {
            return {
                name: this.model.toJSON().name,
                isNew: this.model.isNew()
            };
        }
    });
});