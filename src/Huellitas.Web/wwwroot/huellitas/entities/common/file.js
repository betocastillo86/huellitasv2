Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.File = App.Entities.Model.extend({
        urlRoot: '/api/files',
        addToContent: function () {
            return API.addToContent(this);
        }
    });

    Entities.FileCollection = App.Entities.Collection.extend({
        url: '/api/files',
        model: Entities.File
    });

    var API = {
        getByContent: function (contentId) {
            var collection = new Entities.FileCollection();
            collection.url = '/api/contents/' + contentId + '/files';
            collection.fetch();
            return collection;
        },
        addToContent: function (model) {
            var contentId = model.get('contentId');
            model.url = '/api/contents/' + contentId + '/files';
            model.save();
            return model;
        },
        getNew: function () {
            return new Entities.File();
        },
        getNewCollection: function () {
            return new Entities.FileCollection();
        }
    };

    App.reqres.setHandler('file:entities:byContent', function (contentId) {
        if (!contentId)
            throw new Error('ContentId es obligatorio');

        return API.getByContent(contentId);
    });

    App.reqres.setHandler('file:new:entities', function () {
        return API.getNewCollection();
    });

    App.reqres.setHandler('file:new:entity', function () {
        return API.getNew();
    });
});