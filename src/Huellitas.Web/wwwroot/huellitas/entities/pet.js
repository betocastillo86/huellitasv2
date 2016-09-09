Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.Pet = App.Entities.Model.extend({
        urlRoot: '/api/pets'
    });

    Entities.PetCollection = App.Entities.Collection.extend({
        model: Entities.Pet,
        url: '/api/pets'
    });

    API = {
        getAll: function () {
            var pets = new Entities.PetCollection();
            pets.fetch({ reset: true });
            return pets;
        },
        get: function (id) {
            var pet = new Entities.Pet({ id: id });
            pet.fetch();
            return pet;
        }
    }

    App.reqres.setHandler('pets:entities', API.getAll);
    App.reqres.setHandler('pets:entity', API.get);
});