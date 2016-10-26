Huellitas.module('Entities', function (Entities, App, Backbone, Marionette, $, _) {
	Entities.StatusType = App.Entities.Model.extend({
		urlRoot: '/api/statustypes'
	});

	Entities.StatusTypeCollection = App.Entities.Collection.extend({
		url: '/api/statustypes',
		model: Entities.StatusType
	});

	var API = {
		getAll: function () {
			var collection = new Entities.StatusTypeCollection();
			collection.fetch();
			return collection;
		}
	};

	App.reqres.setHandler('statusType:entities', API.getAll);

	var cachedCollection;
	App.reqres.setHandler('statusType:entities:cached', function () {
		if (!cachedCollection)
		{
			cachedCollection = App.request('statusType:entities');
		}
		return cachedCollection;
	})
});