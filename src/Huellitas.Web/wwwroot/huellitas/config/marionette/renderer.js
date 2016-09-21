(function (Marionette) {
    return _.extend(Marionette.Renderer, {
        //root: 'huellitas/apps/',
        //lookups:['huellitas/apps/','huellitas/components'],
        templates :[],
        render: function (template, data) {
            var path = this.getTemplate(template,data);
            if (path)
                return path(data);
            else
                throw 'No existe el template ' + template;
        },
        getTemplate: function (template, data) {
            return HTempl[template];
        }
    });
})(Marionette);