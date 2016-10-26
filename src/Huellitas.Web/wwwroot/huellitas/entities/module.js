Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.Module = App.Entities.Model.extend({
        urlRoot: '/api/modules'
    });

    Entities.ModuleCollection = App.Entities.Collection.extend({
        model: Entities.Module,
        url: '/api/modules',
        selectByKey: function (key) {
            this.choose(this.findWhere({key:key}));
        },
        choose: function (newChoosed) {
            var model = this.findWhere({ choosen: true });
            if(model)
                model.set('choosen', false);
            newChoosed.set('choosen', true);
        }
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