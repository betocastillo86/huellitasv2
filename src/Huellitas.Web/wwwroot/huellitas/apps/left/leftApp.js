Huellitas.module('LeftApp', function (LeftApp, App, Backbone, Marionette, $, _) {
    this.startWithParent = false;

    var API = {
        menu: function (menuModules) {
            return LeftApp.Menu.Controller.show(menuModules);
        }
    };

    LeftApp.on('start', function (menuModules) {
        API.menu(menuModules);
    });
});