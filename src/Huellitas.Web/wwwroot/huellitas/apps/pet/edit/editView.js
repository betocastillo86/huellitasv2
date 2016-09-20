Huellitas.module('PetApp.Edit', function (Edit, App, Backbone, Marionette, $, _) {
    Edit.Layout = App.Views.Layout.extend({
        template: 'pet/edit/editLayout',
        regions: {
            formRegion: '#form-region',
            titleRegion:'#title-region'
        }
    });

    Edit.Pet = App.Views.ItemView.extend({
        template: 'pet/edit/editPet',
        bindings: {
            '.txtName' : 'name'
        },
        onFormSubmit: function () {
            return true;
        }
    });

    Edit.Title = App.Views.ItemView.extend({
        template: 'pet/edit/editTitle',
        modelEvents: {
            'updated': 'render'
        },
        serializeData: function(){
            return {
                name : this.model.toJSON().name,
                isNew: this.model.isNew()
            };
        }
    });
});