Huellitas.module('LeftApp.Menu', function (Menu, App, Backbone, Marionette, $, _) {
    Menu.Controller = {
        show: function () {
            this.layout = this.getLayoutView();

            this.layout.on('show', function () {
                this.showMainRegion();
                this.showUserRegion();
            }, this);

            App.leftRegion.show(this.layout);
        },
        showMainRegion: function () {
            var modules = App.request('module:entities');
            var menuView = this.getMenuView(modules);
            return this.layout.mainRegion.show(menuView);
        },
        showUserRegion: function () {
            var userView = this.getUserView();
            return this.layout.userRegion.show(userView);
        },
        getLayoutView: function () {
            return new Menu.Layout();
        },
        getMenuView: function (modules) {
            return new Menu.MenuView({ collection: modules });
        },
        getUserView: function () {
            return new Menu.UserView();
        }
    };
});