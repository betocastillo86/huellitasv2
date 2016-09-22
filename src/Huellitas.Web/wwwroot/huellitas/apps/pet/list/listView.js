Huellitas.module('PetApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Layout = App.Views.Layout.extend({
        template: 'pet/list/listLayout',
        regions: {
            titleRegion: '#title-region',
            filterRegion: '#filter-region',
            listRegion: '#list-region'
        }
    });

    List.Title = App.Views.ItemView.extend({
        template: 'pet/list/_title',
        events: {
            'click #btn-create-pet': 'createPet'
        },
        createPet: function () {
            App.vent.trigger('pet:new:clicked');
        }
    });

    List.Filter = App.Views.ItemView.extend({
        template: 'pet/list/_filter'
    });

    List.PetItem = App.Views.ItemView.extend({
        template: 'pet/list/_petItem',
        tagName: 'tr',
        className: 'pointer',
        events: {
            'click': 'select'
        },
        triggers: {
            'click'             : 'pet:item:clicked',
            'click .btn-delete' : 'pet:item:delete:clicked'
        }
    });

    List.Empty = App.Views.ItemView.extend({
        template: 'pet/list/_empty',
        tagName: 'tr'
    });

    List.List = App.Views.CompositeView.extend({
        template: 'pet/list/_list',
        childView: List.PetItem,
        childViewContainer: 'tbody',
        emptyView: List.Empty
    });
});