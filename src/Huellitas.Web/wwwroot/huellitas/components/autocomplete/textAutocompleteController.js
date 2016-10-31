Huellitas.module('Components.Autocomplete', function (Autocomplete, App, Backbone, Marionette, $, _) {
    Autocomplete.Controller = Marionette.Controller.extend({
        getAutocompleteView: function (args) {
            return new Autocomplete.TextView(args);
        }
    });

    App.reqres.setHandler('autocomplete:text', function (el, model, options) {
        var controller = new Autocomplete.Controller();
        _.extend(options, { el: el, model: model });
        return controller.getAutocompleteView(options);
    });
});