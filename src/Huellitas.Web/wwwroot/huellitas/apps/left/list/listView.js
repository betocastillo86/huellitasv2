Huellitas.module('LeftApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Left = App.Views.ItemView.extend({
        template : 'left/list/left'
    });
});