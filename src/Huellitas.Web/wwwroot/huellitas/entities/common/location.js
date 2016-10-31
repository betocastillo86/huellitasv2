Huellitas.module('Entities', function (Entities, App, Marionette, Backbone, $, _) {
    Entities.Location = App.Entities.Model.extend({
        urlRoot: '/api/locations'
    });

    Entities.LocationCollection = App.Entities.Collection.extend({
        url: '/api/locations',
        model: Entities.Location
    });

    var API = {
        getByParent: function (parentId) {
            var collection = new Entities.LocationCollection();
            collection.fetch({ parentId: parentId });
            return collection;
        }
    };

    App.reqres.setHandler('location:entities:byParent', function (parentId) {
        return API.getByParent(parentId);
    });

});