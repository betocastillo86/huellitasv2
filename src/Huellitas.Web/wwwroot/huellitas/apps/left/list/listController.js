Huellitas.module('LeftApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Controller = {
        list: function () {
            var listView = this.getListView();
            return App.leftRegion.show(listView);
        },
        getListView: function () {
            return new List.Left();
        }
    };
});