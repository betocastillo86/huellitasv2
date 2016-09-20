Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {

    var _save = Backbone.Model.prototype.save;

    Entities.Model = Backbone.Model.extend({
        _save : _save,
        save: function (data, options) {
            var isNew = this.isNew();
            options = options || {};
            _.defaults(options, {
                //wait: true,
                success: _.bind(this.saved, this, isNew, options.collection)
            });
            this._save(data, options);
        },
        saved: function (isNew, collection) {
            
            if (isNew) {
                if (collection)
                {
                    collection.add(this);
                    collection.trigger('model:created', this);
                }

                this.trigger('created', this);
            }
            else {
                collection = collection || this.collection;
                if(collection)
                    collection.trigger('model:updated', this);
                this.trigger('updated', this);
            }
        },
        consoleAll: function () {
            this.on('all', function (ev) { console.log(ev); });
        }
    });
});