
this.Huellitas.module('Views', function (Views, App, Backbone, Marionette, $, _) {
    var _remove = Marionette.View.prototype.remove;
    _.extend(Marionette.View.prototype, {
        remove: function () {

            var args = 1 <= arguments.length ? slice.call(arguments, 0) : [];
            var that = this;

            console.log('removing', this);

            if (this.model && this.model['isDestroyed'] && this.model.isDestroyed()) {
                var wrapper = this.$el.toogleWrapper({
                    className: 'opacity',
                    backgroundColor: 'red'
                });

                wrapper.fadeOut(400, function () {
                    wrapper.remove();
                });

                this.$el.fadeOut(400, function () {
                    _remove.apply(that, args);
                });

                
            }
            else {              
                return _remove.apply(that, args);
            }
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
        },
        setInstancePropertiesFor: function () {
            var args = arguments || [];
            var props = _.pick(this.options, args);
            for (var i = 0; i < args.length; i++) {
                var arg = args[i];
                this[arg] = props[arg];
            }
        },
        addOpacityWrapper: function (init) {
            this.$el.toogleWrapper({ className: 'opacity' }, init);
        }
    });

    //App.Views.View = Marionette.View.extend({
    //
    //});
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