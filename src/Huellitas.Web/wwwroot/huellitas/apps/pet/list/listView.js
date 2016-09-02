Huellitas.module('PetApp.List', function (List, App, Backbone, Marionette, $, _) {

    List.Layout = App.Views.Layout.extend({
        template: 'pet/list/list_layout',
        regions: {
            titleRegion: '#title-region',
            filterRegion : '#filter-region'
        }
    });

    List.Title = App.Views.ItemView.extend({
        template: 'pet/list/_title'
    });

    List.Filter = App.Views.ItemView.extend({
        template:'pet/list/_filter'
    });
});