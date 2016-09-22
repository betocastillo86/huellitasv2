Huellitas.module('PetApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Controller = {
        list: function () {
            var pets = App.request('pet:entities');

            var that = this;
            App.execute('when:fetched', [pets], function () {
                that.layout = that.getLayoutView();

                that.layout.on('show', function () {
                    that.titleRegion();
                    that.filterRegion();
                    that.listRegion(pets);
                }, that);

                App.mainRegion.show(that.layout);
            });
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

            listView.on('childview:pet:item:clicked', function (child, args) {
                App.vent.trigger('pet:item:clicked', args.model);
            });

            listView.on('childview:pet:item:delete:clicked', function (child, args) {
                var model = args.model;
                if (confirm('¿Está seguro de eliminar el registro?'))
                {
                    model.destroy();
                    //App.vent.trigger('pet:item:delete:clicked', model);
                }
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