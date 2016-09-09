Huellitas.module('PetApp.Edit', function (Edit, App, Backbone, Marionette, $, _) {
    Edit.Pet = App.Views.ItemView.extend({
        template: 'pet/edit/editPet',
        modelEvents: {
            'sync' :'render'
        }
    });
});