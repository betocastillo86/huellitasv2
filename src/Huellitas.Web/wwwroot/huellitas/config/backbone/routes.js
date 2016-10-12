Huellitas.RouteList = {
	common: {
		insert: '/create',
		update: '/edit',
		remove: '/delete'
	},
	pets: {
		rootUrl: '/pets',
		getAll: function (params) {
			return this.rootUrl + (params ? '?' + $.param(params) : '');
		},
		getById: function (id) {
			return this.rootUrl + id;
		},
		insert: function () {
			return this.rootUrl + common.insert;
		},
		update: function (id) {
			return this.rootUrl + id + '/' + common.update;
		}
	}
};