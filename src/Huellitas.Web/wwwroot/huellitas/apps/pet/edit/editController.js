Huellitas.module('PetApp.Edit', function (Edit, App, Backbone, Marionette, $, _) {
    Edit.Controller = {
        edit: function (id, model) {
            if (!model)
                model = App.request('pets:entity', id);
            var editView = this.getEditView(model);
            App.mainRegion.show(editView);
        },
        getEditView: function (model) {
            return new Edit.Pet({ model :model});
        }
    }
});