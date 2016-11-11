Huellitas.module('Components.FileUpload', function (FileUpload, App, Backbone, Marionette, $, _) {
	FileUpload.Controller = Marionette.Controller.extend({
		getFileUploadView: function (options) {
			var defaultOptions = this.getDefaultConfig(options);
			this.uploadView = new FileUpload.UploadView(options);
			this.listenTo(this.uploadView, 'file:selected', this.sendPostFile);
			return this.uploadView;
		},
		getDefaultConfig: function (config) {
			config = config || {};
			return _.defaults(config, {
				maxSizeMb: 5,
				fileType: 'image',
				multiple: false,
				showLoading: true,
				asFormat: undefined
			});
		},
		sendPostFile: function (result) {
			var that = this;

			var data = new FormData();
			data.append('file', result.file);

			$.ajax({
				url: this.uploadView.url + '?' + $.param(result.options),
				data: data,
				cache: false,
				contentType: false,
				processData: false,
				type: 'POST',
				success: function (data) {
				    //that.uploadView.triggerMethod('file:sent', data);
				    that.uploadView.trigger('file:sent', data);
					//result.view.displayAjaxLoading(false);
				},
				error: function (data) {
				    that.uploadView.trigger('file:error', data.responseJSON);
					//that.uploadView.triggerMethod('file:error', data.responseJSON);
					//result.view.displayAjaxLoading(false);
				}
			});
		}
	});

	App.reqres.setHandler('fileupload:view', function (options) {
		var controller = new FileUpload.Controller();
		return controller.getFileUploadView(options);
	});
});