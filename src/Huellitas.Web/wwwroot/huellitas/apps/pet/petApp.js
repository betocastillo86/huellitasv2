Huellitas.module('PetApp', function (PetApp, App, Backbone, Marionette, $, _) {
    PetApp.Router = Marionette.AppRouter.extend({
        appRoutes: {
            'pets' : 'list'
        }
    });

    API = {
        list: function () {
            PetApp.List.Controller.list();
        }
    }

    App.addInitializer = function () {
        new PetApp.Router({ controller: API });
    }
});