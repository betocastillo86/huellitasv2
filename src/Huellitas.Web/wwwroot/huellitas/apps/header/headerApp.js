Huellitas.module('HeaderApp', function (HeaderApp, App, Backbone, Marionette, $, _) {
    this.startWithParent = false

    var API = {
        list: function () {
            return HeaderApp.List.Controller.list();
        }
    }

    HeaderApp.on('start', function () {
        API.list();
    });
});