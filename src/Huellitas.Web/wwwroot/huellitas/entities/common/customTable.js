Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.CustomTableRow = App.Entities.Model.extend({

    });

    Entities.CustomTableRowCollection = App.Entities.Collection.extend({
        model: Entities.CustomTableRow
    });

    var API = {
        getRowsByTable: function (table) {
            var collection = new Entities.CustomTableRowCollection();
            collection.url = '/api/customtables/' + table + '/rows';
            collection.fetch();
            return collection;
        }
    };
    
    App.reqres.setHandler('customtable:row:entities', function (table) {
        return API.getRowsByTable(table);
    });


    var cachedTables = {};
    App.reqres.setHandler('customtable:row:entities:cached', function (table) {
        if (!cachedTables[table]) {
            var collection = App.request('customtable:row:entities', table);
            cachedTables[table] = collection;
        }
        return cachedTables[table];
    });


});