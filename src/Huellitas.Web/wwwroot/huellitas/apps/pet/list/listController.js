Huellitas.module('PetApp.List', function (List, App, Backbone, Marionette, $, _) {
    List.Controller = {
        list: function (filter) {
            App.vent.trigger('menu:choose', 'Animals');

            var pets = App.request('pet:entities', filter);

            var that = this;
            App.execute('when:fetched', [pets], function () {
                that.layout = that.getLayoutView();

                that.layout.on('show', function () {
                    that.titleRegion();
                    that.filterRegion(filter);
                    that.listRegion(pets);
                }, that);

                App.mainRegion.show(that.layout);
            });
        },
        filter: function (filter) {
            App.navigate(Huellitas.RouteList.pets.getAll(filter), { trigger: false });
            //Backbone.history.navigate(Huellitas.RouteList.pets.getAll(filter), { trigger: true });
            var pets = App.request('pet:entities', filter);
            this.listRegion(pets);
        },
        titleRegion: function () {
            var titleView = this.getTitleView();
            return this.layout.titleRegion.show(titleView);
        },
        filterRegion: function (initialFilter) {
            var shelters = App.request('shelter:entities');
            var filterView = new this.getFilterView({ filter: initialFilter, shelters: shelters });
            filterView.on('filter:pet:changed', this.filter, this);
            return this.layout.filterRegion.show(filterView);
        },
        listRegion: function (models) {
            var listView = new this.getListView(models);

            listView.on('childview:pet:item:clicked', function (child, args) {
                App.vent.trigger('pet:item:clicked', args.model);
            });

            listView.on('childview:pet:item:delete:clicked', function (child, args) {
                var model = args.model;
                if (confirm('¿Está seguro de eliminar el registro?')) {
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
        getFilterView: function (args) {
            return new List.Filter({ model: new App.Entities.Filter(args.filter), shelters: args.shelters });
        },
        getListView: function (models) {
            return new List.List({ collection: models });
        }
    };
});