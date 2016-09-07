Huellitas.module('LeftApp', function (LeftApp, App, Backbone, Marionette, $, _) {
    this.startWithParent = false;

    var API = {
        list: function () {
            return LeftApp.List.Controller.list();
        }
    };

    LeftApp.on('start', function () {
        API.list();
    });
});