Huellitas.module('HeaderApp.Show', function (Show, App, Backbone, Marionette, $, _) {
    Show.Header = App.Views.ItemView.extend({
        template : 'header/show/header'
    });
});