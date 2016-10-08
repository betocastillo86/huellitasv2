Huellitas.module('LeftApp', function (LeftApp, App, Backbone, Marionette, $, _) {
    this.startWithParent = false;

    var API = {
        menu: function () {
            return LeftApp.Menu.Controller.show();
        }
    };

    LeftApp.on('start', function () {
        API.menu();
    });
});