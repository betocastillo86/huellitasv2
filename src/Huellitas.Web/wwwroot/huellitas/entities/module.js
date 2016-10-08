Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.Module = Entities.Model.extend({
        urlRoot: '/api/modules'
    });

    Entities.ModuleCollection = Entities.Collection.extend({
        model: Entities.Module,
        url: '/api/modules'
    });

    var API = {
        getAll: function () {
            var collection = new Entities.ModuleCollection();
            collection.fetch();
            return collection;
        }
    };

    App.reqres.setHandler('module:entities', API.getAll);

});