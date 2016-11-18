Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.Pet = App.Entities.Model.extend({
        urlRoot: '/api/pets',
        validation: {
            'name': {
                required: true
            },
            'body': {
                required: true
            },
            'location': {
                required: function (value, field, model) {
                    return !model.shelter || !model.shelter.id;
                }
            },
            'subtype': {
                required: true
            },
            'genre': {
                required: true
            },
            'size': {
                required: true
            },
            'months': {
                required: true,
                min: 1
            },
            'status': {
                required:true
            }
        },
        labels: {
            'body': 'Descripción',
            'name': 'Name',
            'location': 'Ubicación',
            'subtype': 'Tipo',
            'genre': 'Genero',
            'size': 'Tamaño',
            'months': 'Edad'

        }
    });

    Entities.PetCollection = App.Entities.Collection.extend({
        model: Entities.Pet,
        url: '/api/pets'
    });

    var API = {
        getAll: function (filter) {
            var pets = new Entities.PetCollection();
            pets.fetch({ reset: true, data: filter });
            return pets;
        },
        get: function (id) {
            var pet = new Entities.Pet({ id: id });
            pet.fetch();
            return pet;
        },
        getNew: function () {
            return new Entities.Pet();
        }
    }

    App.reqres.setHandler('pet:entities', function (filter) {
        return API.getAll(filter);
    });
    App.reqres.setHandler('pet:entity', API.get);
    App.reqres.setHandler('pet:new:entity', API.getNew);
});