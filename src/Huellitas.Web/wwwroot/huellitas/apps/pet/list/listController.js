Huellitas.module('PetApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Controller = {
        list: function () {

            var pets = App.request('pets:entities');

            this.layout = this.getLayoutView();

            this.layout.on('show', function(){
                this.titleRegion();
                this.filterRegion();
                this.listRegion(pets);
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
        listRegion: function (models) {
            var listView = new this.getListView(models);

            listView.on('childview:pets:item:clicked', function (child, model) {
                App.vent.trigger('pets:item:clicked', model);
            });

            return this.layout.listRegion.show(listView);
        },
        getLayoutView: function () {
            return new List.Layout();
        },
        getTitleView: function () {
            return new List.Title();
        },
        getFilterView: function () {
            return new List.Filter();
        },
        getListView: function (models) {
            return new List.List({collection:models});
        }
    }
});