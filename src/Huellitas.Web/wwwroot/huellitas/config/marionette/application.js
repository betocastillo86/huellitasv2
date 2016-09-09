(function (Backbone) {
    return _.extend(Backbone.Marionette.Application.prototype, {
        navigate: function (route, options) {
            route: route || {};
            //if (route.chatAt(0) === '/')
            //    route = '#' + route;
            route = '#' + route;
            return Backbone.history.navigate(route, options);
        },
        getCurrentRoute: function () {
            var fragment = Backbone.history.fragment;
            return _.isEmpty(fragment) ? null : fragment;
        },
        startHistory: function () {
            if (Backbone.history)
                Backbone.history.start();
        }
    });
})(Backbone);