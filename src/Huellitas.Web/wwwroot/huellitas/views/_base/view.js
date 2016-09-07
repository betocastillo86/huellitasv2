
this.Huellitas.module('Views', function (Views, App, Backbone, Marionette, $, _) {
    var _remove = Marionette.View.prototype.remove;
    _.extend(Marionette.View.prototype, {
        remove: function () {
            var args = 1 <= arguments.length ? slice.call(arguments, 0) : [];
            return _remove.apply(this, args);
        },
        templateHelpers: {
            linkTo: function (name, url, options) {
                options = options || {};
                _.defaults(options, { external: false });
    
                if (!options.external) {
                    url = '#' + url;
                }
                return '<a href="#{url}">#{this.escape(name)}</a>';
    
            }
        }
    });
    //App.Views.View = Marionette.View.extend({
    //    remove: function () {
    //        var args = 1 <= arguments.length ? slice.call(arguments, 0) : [];
    //        return _remove.apply(this, args);
    //    },
    //    templateHelpers: {
    //        linkTo: function (name, url, options) {
    //            options = options || {};
    //            _.defaults(options, { external: false });

    //            if (!options.external) {
    //                url = '#' + url;
    //            }
    //            return '<a href="#{url}">#{this.escape(name)}</a>';

    //        }
    //    }
    //});

});