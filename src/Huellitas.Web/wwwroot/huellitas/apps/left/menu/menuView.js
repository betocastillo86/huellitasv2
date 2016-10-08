Huellitas.module('LeftApp.Menu', function (Menu, App, Backbone, Marionette, $, _) {

    Menu.Layout = App.Views.Layout.extend({
        template: 'left/menu/menuLayout',
        regions: {
            userRegion: '#menu-user-region',
            mainRegion: '#menu-main-region'
        }
    });

    Menu.UserView = App.Views.ItemView.extend({
        template: 'left/menu/menuUserView'
    });

    Menu.MenuItemView = App.Views.ItemView.extend({
        tagName: 'li',
        template: 'left/menu/menuItemView'
    });

    Menu.MenuView = App.Views.CompositeView.extend({
        template: 'left/menu/menuView',
        childView: Menu.MenuItemView,
        childViewContainer: 'ul'
    });

    
});