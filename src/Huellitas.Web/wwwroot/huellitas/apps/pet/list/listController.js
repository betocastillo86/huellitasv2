Huellitas.module('PetApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Controller = {
        list: function () {
            this.layout = this.getLayoutView();

            this.layout.on('show', function(){
                this.titleRegion();
                this.filterRegion();
            }, this);

            return App.mainRegion.show(this.layout);
        },
        titleRegion: function () {
            var titleView = this.getTitleView();
            return this.layout.titleRegion.show(titleView);
        },
        filterRegion: function () {
            var filterView = new this.getFilterView();
            return this.layout.filterRegion.show(filterView);
        },
        getLayoutView: function () {
            return new List.Layout();
        },
        getTitleView: function () {
            return new List.Title();
        },
        getFilterView: function () {
            return new List.Filter();
        }
    }
});