Huellitas.module('HeaderApp', function (HeaderApp, App, Backbone, Marionette, $, _) {
    this.startWithParent = false

    var API = {
        show: function () {
            return HeaderApp.Show.Controller.show();
        }
    }

    HeaderApp.on('start', function () {
        API.show();
    });
});