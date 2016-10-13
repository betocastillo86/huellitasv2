Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.Shelter = App.Entities.Model.extend({
        urlRoot: '/api/shelters'
    });

    Entities.ShelterCollection = App.Entities.Collection.extend({
        model: Entities.Shelter,
        url: '/api/shelters'
    });

    var API = {
        getAll: function () {
            var shelters = new Entities.ShelterCollection();
            shelters.fetch();
            return shelters;
        }
    };

    App.reqres.setHandler('shelter:entities', API.getAll);
});