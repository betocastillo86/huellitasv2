Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
    Entities.Button = App.Entities.Model.extend({

    });

    Entities.ButtonsCollection = App.Entities.Collection.extend({
        model : Entities.Button
    });

    var API = {
        getFormButtons: function (buttons, model) {
            buttons = this.getDefaultButtons(buttons, model);

            var array = [];
            if (buttons.cancel !== false)
                array.push({ type: 'cancel', className: 'btn btn-default', text: buttons.cancel });
            if(buttons.primary !== false)
                array.push({ type: 'primary', className: 'btn btn-success', text: buttons.primary });

            if (buttons.placement == 'right')
                array.reverse();

            var buttonCollection = new Entities.ButtonsCollection(array);
            buttonCollection.placement = buttons.placement;
            return buttonCollection;
        },
        getDefaultButtons: function (buttons, model) {
            return _.defaults(buttons, {
               primary: model.isNew() ? 'Crear' : 'Actualizar',
               cancel: 'Cancelar',
               placement: 'left'
           });
        }
    }

    App.reqres.setHandler('form:buttons:entities', function (buttons, model) {
        buttons = buttons || {};
        return API.getFormButtons(buttons, model);
    });
});