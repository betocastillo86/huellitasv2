Huellitas.module('PetApp', function (PetApp, App, Backbone, Marionette, $, _) {
    PetApp.Router = Marionette.AppRouter.extend({
        appRoutes: {
            'pets': 'list',
            'pets/:id/edit': 'edit',
            'pets/create': 'create'
        }
    });

    API = {
        list: function () {
            PetApp.List.Controller.list();
        },
        edit: function (id, model) {
            PetApp.Edit.Controller.edit(id, model);
        },
        create: function () {
            PetApp.Edit.Controller.create();
        }
    }

    App.vent.on('pet:new:clicked', function () {
        App.navigate('/pets/create', { trigger: false, replace: true })
        //return API.create();
    });

    App.vent.on('pet:item:clicked', function (model) {
        App.navigate('/pets/' + model.id + '/edit', { trigger: false, replace: true })
        //API.edit(model.id, model);
    });

    App.vent.on('pet:cancelled', function (model) {
        App.navigate('/pets');
    });

    App.addInitializer = function () {
        new PetApp.Router({ controller: API });
    }
});