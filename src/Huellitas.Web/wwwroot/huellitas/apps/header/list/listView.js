Huellitas.module('HeaderApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Header = App.Views.ItemView.extend({
        template : 'header/list/header'
    });
});