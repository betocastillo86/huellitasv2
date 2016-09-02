Huellitas.module('HeaderApp.Show', function (Show, App, Backbone, Marionette, $, _) {
    Show.Controller = {
        show: function () {
            var showView = this.getShowView();
            return App.headerRegion.show(showView);
        },
        getShowView: function () {
            return new Show.Header();
        }
    };
});