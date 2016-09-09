﻿Huellitas.module('PetApp.List', function (List, App, Backbone, Marionette, $, _) {

    List.Layout = App.Views.Layout.extend({
        template: 'pet/list/listLayout',
        regions: {
            titleRegion: '#title-region',
            filterRegion: '#filter-region',
            listRegion : '#list-region'
        }
    });

    List.Title = App.Views.ItemView.extend({
        template: 'pet/list/_title'
    });

    List.Filter = App.Views.ItemView.extend({
        template:'pet/list/_filter'
    });

    List.PetItem = App.Views.ItemView.extend({
        template: 'pet/list/_petItem',
        tagName: 'tr',
        className: 'pointer',
        events: {
            'click' : 'select'
        },
        select: function () {
            this.trigger('pets:item:clicked', this.model);
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
        emptyView : List.Empty
    });

});