Huellitas.module('Components.Autocomplete', function (Autocomplete, App, Backbone, Marionette, $, _) {
    Autocomplete.TextView = App.Views.ItemView.extend({
        url: undefined,
        selector: undefined,
        isObjectType: false,
        observe: undefined,
        observeText: undefined,
        queryArg: 'q',
        initialize: function (args) {
            this.setInstancePropertiesFor('url', 'selector', 'isObjectType', 'observe', 'observeText', 'queryArg');
            this.loadAutoComplete();
            this.bindElements();
        },
        loadAutoComplete: function () {
            var that = this;
            AutoComplete({
                Url: this.url,
                QueryArg: this.queryArg,
                _Post: function (response) {
                    return _.map(JSON.parse(response), function (el) { return { Label: el.name, Value: el.id } });
                },
                _Select: function (item) {
                    var id = parseInt(item.attributes['data-autocomplete-value'].value);
                    var name = item.innerHTML;

                    if (that.isObjectType) {
                        var selected = that.model.get(that.observe) || {};
                        selected.id = id;
                        selected.name = name;
                        that.model.set(that.observe, selected);
                    }
                    else {
                        that.model.set(that.observe, id);
                        that.model.set(that.observeText, id);
                    }

                    that.bindElements();
                },
                _Position:function() {
                    this.DOMResults.setAttribute("class", "autocomplete");
                },
                _Blur: function (event) {
                    if (event.target.value.trim() == '')
                    {
                        that.model.unset(that.observe);
                    }
                    console.log('blur');
                },
                HttpHeaders: {
                    'Content-Type': 'application/json'
                }
            }, this.selector);
        },
        bindElements: function () {
            if (this.isObjectType) {
                this.$el.val(this.model.get(this.observe).name);
            }
            else {
                this.$el.val(this.model.get(this.observeText));
            }
        }
    });
});