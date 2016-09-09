Huellitas.module('PetApp', function (PetApp, App, Backbone, Marionette, $, _) {
    PetApp.Router = Marionette.AppRouter.extend({
        appRoutes: {
            'pets': 'list',
            'pets/:id/edit' :'edit'
        }
    });

    API = {
        list: function () {
            PetApp.List.Controller.list();
        },
        edit: function (id, model) {
            PetApp.Edit.Controller.edit(id, model);
        }
    }

    App.vent.on('pets:item:clicked', function (model) {
        App.navigate('/pets/' + model.id +'/edit')
        API.edit(model.id, model);
    });

    App.addInitializer = function () {
        new PetApp.Router({ controller: API });
    }
});