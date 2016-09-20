Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    App.commands.setHandler('when:fetched', function (entities, callback) {
        var xhrs = _.chain([entities]).flatten().pluck('_fetch').value();
        $.when.apply($, xhrs).done(function(){
            callback();
        });
    });
});